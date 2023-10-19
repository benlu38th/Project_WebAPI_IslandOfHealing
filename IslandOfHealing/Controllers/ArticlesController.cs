using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;
using NSwag.Annotations;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("PostArticle", Description = "發表文章")]
    public class ArticlesController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 新增文章/草稿圖片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/articlecontentimgurl/create")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UpdateArticleImg()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            var userExist = db.Users.Any(u => u.Id == userid && u.Role == "writer");

            if (userExist)//使用者存在且使用者是作家
            {
                // 檢查請求是否包含 multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                // 檢查資料夾是否存在，若無則建立
                string root = HttpContext.Current.Server.MapPath("~/upload/articlecontentimg");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                try
                {
                    // 讀取 MIME 資料
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    List<string> imglList = new List<string>();

                    //遍歷 provider.Contents 中的每個 content，處理多個圖片檔案
                    foreach (var content in provider.Contents)
                    {
                        // 取得檔案副檔名
                        string fileNameData = content.Headers.ContentDisposition.FileName.Trim('\"');
                        string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                        // 定義檔案名稱
                        string fileName = "articlecontentimg" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileType;

                        // 儲存圖片
                        var fileBytes = await content.ReadAsByteArrayAsync();
                        var outputPath = Path.Combine(root, fileName);
                        using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                        }

                        // 載入原始圖片，直接存入伺服器(未裁切)
                        using (var image = Image.Load<Rgba32>(outputPath))
                        {
                            // 儲存裁切後的圖片
                            image.Save(outputPath);
                        }

                        //更新文章封面欄位
                        imglList.Add(fileName);
                    }

                    //創建文章圖片資料庫
                    var newPost = new ArticleContentImg();

                    string imgUrl = string.Join(";", imglList);
                    newPost.ImgUrl = imgUrl;

                    newPost.InitDate = DateTime.Now;

                    //圖片資料新增進SQL
                    db.ArticleContentImgs.Add(newPost);

                    // 儲存文章圖片到資料庫
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "文章內圖片上傳成功",
                        ArticleContentImgData = imglList.Select(img => "https://islandofhealing.rocket-coding.com/upload/articlecontentimg/" + img)
                    };

                    return Ok(result);
                }
                catch (DbEntityValidationException ex)
                {
                    // Handle entity validation errors
                    var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    return BadRequest(exceptionMessage);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message); // 400
                }
            }
            else
            {
                return BadRequest("使用者不存在或不是作家");
            }
        }

        /// <summary>
        /// 新增文章分類
        /// </summary>
        /// <param name="category">文章分類</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/articlecategory/create")]
        [JwtAuthFilter]
        public IHttpActionResult AddarticleCategory(string category)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id及Role
            int id = (int)jwtObject["Id"];
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            var userRole = user.Role;

            //使用者角色是administrator，才能新增文章分類
            if (userRole == "administrator")
            {
                //新增文章類別的資料
                var newCategory = new ArticlesCategory();
                newCategory.Name = category;
                newCategory.InitDate = DateTime.Now;

                // 文章類別新增的資料塞進SQL
                db.ArticlesCategories.Add(newCategory);
                db.SaveChanges();

                // 新增文章類別成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "新增文章類別成功"
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("角色不是平台方，新增文章失敗");
            }
        }

        /// <summary>
        /// 新增文章/草稿
        /// </summary>
        /// <param name="inputCreateArticle">文章標題、文章內文、標籤、閱讀權限、文章分類、內容摘要、文章進度</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/article/create")]
        [JwtAuthFilter]
        public IHttpActionResult ArticleCreate(ViewModel.CreateArticle inputCreateArticle)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得作家(使用者)Id及Role
            int id = (int)jwtObject["Id"];
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            var userRole = user.Role;

            if (userRole == "writer")
            {
                //新增文章的資料
                var newArticle = new Article();
                newArticle.Content = inputCreateArticle.Content;
                newArticle.ArticlesCategoryId = inputCreateArticle.ArticlesClassId;
                newArticle.InitDate = DateTime.Now;
                newArticle.UserId = id;
                newArticle.Title = inputCreateArticle.Title;
                newArticle.Pay = inputCreateArticle.Pay;
                newArticle.Summary = inputCreateArticle.Summary;
                newArticle.Tags = string.Join(";", inputCreateArticle.Tags);
                newArticle.Progress = inputCreateArticle.Progress;

                // 文章新增成功塞資料進SQL
                db.Articles.Add(newArticle);
                db.SaveChanges();

                // 文章新增成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "新增文章成功",
                    ArticleId = newArticle.Id //該新生成文章的Id
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("角色不是作家，無法新增文章");
            }
        }

        /// <summary>
        /// 更新文章進度
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <param name="inputArticleProgress">文章進度</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/article/updateprogress/{articleid}")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UpdateProgress(int articleid, ViewModel.ArticleProgress inputArticleProgress)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取出使用者id
            int userid = (int)jwtObject["Id"];

            //取出使用者角色(經過[JwtAuthFilter]驗證，user必定存在)
            var userInfo = db.Users.Where(u => u.Id == userid).FirstOrDefault();
            string userRole = userInfo.Role;

            //取出文章
            var articleInfo = db.Articles.Where(a => a.Id == articleid).FirstOrDefault();

            if (articleInfo == null)
            {
                return BadRequest("文章不存在，無法更新文章進度");
            }
            else
            {
                if (userRole == "administrator")
                {
                    //將前端傳入的更新狀態存入
                    articleInfo.Progress = inputArticleProgress.Progress;

                    //儲存變更到資料庫
                    db.SaveChanges();

                    // 文章刪除成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "文章進度更新成功"
                    };
                    return Ok(result);
                }
                else if (userRole == "writer")
                {
                    bool sameAuthor = articleInfo.UserId == userid;
                    if (!sameAuthor)
                    {
                        return BadRequest("作者只能更新自己文章的進度");
                    }
                    else
                    {
                        //將前端傳入的更新狀態存入
                        articleInfo.Progress = inputArticleProgress.Progress;

                        //儲存變更到資料庫
                        db.SaveChanges();

                        // 文章刪除成功回傳
                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "文章進度更新成功"
                        };
                        return Ok(result);
                    }
                }
                else
                {
                    return BadRequest("只有管理者和文章作者自己才能更新文章進度");
                }
            }
        }

        /// <summary>
        /// 修改文章封面照
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/article/updatearticleimg/{articleid}")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UpdateArticleImg(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            var articleInfo = db.Articles.Where(a => a.Id == articleid).FirstOrDefault();
            var articleUserInfo = db.Articles.Any(a => a.Id == articleid && a.UserId == userid);

            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // 檢查資料夾是否存在，若無則建立
            string root = HttpContext.Current.Server.MapPath("~/upload/articlecover");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            if (articleInfo == null)
            {
                return BadRequest("該文章不存在，無法修改文章封面");
            }
            else if (!articleUserInfo)
            {
                return BadRequest("只有文章作者才能修改文章封面");
            }
            else
            {
                try
                {
                    // 讀取 MIME 資料
                    var provider = new MultipartMemoryStreamProvider();
                    await Request.Content.ReadAsMultipartAsync(provider);

                    // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                    string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                    string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                    // 定義檔案名稱
                    string fileName = articleid + "articlecover" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

                    // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                    var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                    var outputPath = Path.Combine(root, fileName);
                    using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                    }

                    // 載入原始圖片，裁切圖片 (820x312 正方形)
                    using (var image = Image.Load<Rgba32>(outputPath))
                    {
                        // 設定裁切尺寸
                        int cropWidth = 820;
                        int cropHeight = 312;

                        // 裁切圖片
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(cropWidth, cropHeight),
                            Mode = ResizeMode.Crop
                        }));

                        // 儲存裁切後的圖片
                        image.Save(outputPath);
                    }

                    //更新文章封面欄位
                    articleInfo.CoverUrl = fileName;

                    //儲存變更文章封面到資料庫
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "文章封面更新成功"
                    };

                    return Ok(result);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message); // 400
                }
            }
        }

        /// <summary>
        /// 刪除文章
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/article/delete/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult DeleteArticle(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取出使用者id
            int userid = (int)jwtObject["Id"];

            //取出使用者角色(經過[JwtAuthFilter]驗證，user必定存在)
            var userInfo = db.Users.Where(u => u.Id == userid).FirstOrDefault();
            string userRole = userInfo.Role;

            //取出文章
            var articleInfo = db.Articles.Where(abc => abc.Id == articleid).FirstOrDefault();

            if (articleInfo == null)
            {
                return BadRequest("文章不存在，無法刪除");
            }
            else
            {
                if (userRole == "administrator")
                {
                    // 從資料庫中刪除文章
                    db.Articles.Remove(articleInfo);
                    db.SaveChanges();

                    // 文章刪除成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "文章刪除成功"
                    };
                    return Ok(result);
                }
                else if (userRole == "writer")
                {
                    bool sameAuthor = articleInfo.UserId == userid;
                    if (!sameAuthor)
                    {
                        return BadRequest("作者只能刪除自己的文章");
                    }
                    else
                    {
                        // 從資料庫中刪除文章
                        db.Articles.Remove(articleInfo);
                        db.SaveChanges();

                        // 文章刪除成功回傳
                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "文章刪除成功"
                        };
                        return Ok(result);
                    }
                }
                else
                {
                    return BadRequest("只有管理者和文章作者自己才能刪除文章");
                }
            }
        }

        /// <summary>
        /// 更新文章(不包含封面照)
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <param name="inputCreateArticle">欲更新的文章內容</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/article/update/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult UpdateArticle(int articleid, ViewModel.CreateArticle inputCreateArticle)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            var articleExist = db.Articles.Any(a => a.Id == articleid);
            var articleInfo = db.Articles.Where(a => a.Id == articleid && a.UserId == userid).FirstOrDefault();

            if (!articleExist)
            {
                return BadRequest("該文章不存在，無法修改文章");
            }
            else if (articleInfo == null)
            {
                return BadRequest("只有文章作者才能修改文章");
            }
            else
            {
                articleInfo.Content = inputCreateArticle.Content;
                articleInfo.ArticlesCategoryId = inputCreateArticle.ArticlesClassId;
                articleInfo.Title = inputCreateArticle.Title;
                articleInfo.Pay = inputCreateArticle.Pay;
                articleInfo.Summary = inputCreateArticle.Summary;
                articleInfo.Tags = string.Join(";", inputCreateArticle.Tags);
                articleInfo.Progress = inputCreateArticle.Progress;

                // 文章修改成功塞資料進SQL
                db.SaveChanges();

                // 文章新增成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "修改文章成功"
                };
                return Ok(result);
            }
        }
    }
}
