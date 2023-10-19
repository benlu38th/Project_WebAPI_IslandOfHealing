using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;
using System.Web;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;
using System.Data.Entity.Validation;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("ForumSystem", Description = "論壇")]
    public class ConversationController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 瀏覽使用者自己的話題列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/conversations")]
        [JwtAuthFilter]
        public IHttpActionResult GetUserConversations()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //判斷使用者是否存在
            var userExist = db.Users.Any(u => u.Id == id);

            // 取得資料表中的所有屬於使用者的話題資料
            var conversationsInfo = db.Conversations.Where(c => c.UserId == id).ToList();



            if (!userExist) //使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else //使用者存在
            {
                //回傳資料
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得使用者自己的話題列表成功",
                    Data = new List<object>()
                };

                //處理回傳的Data資料
                foreach (var eachConversation in conversationsInfo)
                {
                    var Data = new
                    {
                        Id = eachConversation.Id,
                        Title = eachConversation.Title,
                        Initdate = eachConversation.InitDate,
                        Anonymous = eachConversation.Anonymous,
                        CategoryId = eachConversation.ConversationCategoryId,
                        Category = eachConversation.MyConversationsCategory.Name,
                        CommentsNum = eachConversation.ConversationComments.Count()
                    };

                    result.Data.Add(Data);
                }

                return Ok(result);
            }
        }

        /// <summary>
        /// 取得全部話題分類(XX相談室)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/conversationcategory/get")]
        [JwtAuthFilter]
        public IHttpActionResult ConversationCategoryGet()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得取得話題分類(XX相談室)");
            }
            else//使用者是"administrator"
            {
                var conversationCategoriesInfo = db.ConversationsCategories;

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得話題分類(XX相談室)成功",
                    ConversationCategories = conversationCategoriesInfo
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// 新增話題分類(XX相談室)
        /// </summary>
        /// <param name="intputConversationsCategory">分類名稱、分類介紹</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/conversationcategory/create")]
        [JwtAuthFilter]
        public IHttpActionResult ConversationCategoryCreate(ViewModel.IntputConversationsCategory intputConversationsCategory)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得增加論壇分類");
            }
            else//使用者是"administrator"
            {
                //讀入前端輸入的資料
                var conversationsCategory = new ConversationsCategory();
                conversationsCategory.Name = intputConversationsCategory.Name;
                conversationsCategory.Description = intputConversationsCategory.Description;
                conversationsCategory.InitDate = DateTime.Now;

                //新增分類之資料庫
                db.ConversationsCategories.Add(conversationsCategory);
                db.SaveChanges();

                //建立回傳前端內容
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "新增話題分類(告解室)成功"
                };

                return Ok(result);
            }
        }

        /// <summary>
        /// 新增話題
        /// </summary>
        /// <param name="inputCreateConversation">話題標題、話題內文、標籤、是否匿名、話題分類、內容摘要</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/conversation/create")]
        [JwtAuthFilter]
        public IHttpActionResult ArticleCreate(ViewModel.InputCreateConversation inputCreateConversation)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得作家(使用者)Id及Role
            int id = (int)jwtObject["Id"];
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo != null)
            {
                //新增話題的資料
                var newConversation = new Conversation();
                newConversation.Content = inputCreateConversation.Content;
                newConversation.ConversationCategoryId = inputCreateConversation.ConversationsCategoryId;
                newConversation.InitDate = DateTime.Now;
                newConversation.UserId = id;
                newConversation.Title = inputCreateConversation.Title;
                newConversation.Anonymous = inputCreateConversation.Anonymous;
                newConversation.Summary = inputCreateConversation.Summary;
                newConversation.Tags = string.Join(";", inputCreateConversation.Tags);

                // 話題新增成功塞資料進SQL
                db.Conversations.Add(newConversation);
                db.SaveChanges();

                // 文章新增成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "新增話題成功",
                    ConversationId = newConversation.Id //該新生成話題的Id
                };
                return Ok(result);
            }
            else
            {
                return BadRequest("使用者不存在，無法新增話題");
            }
        }

        /// <summary>
        /// 修改話題封面照
        /// </summary>
        /// <param name="conversationid">話題id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/conversation/updateconversationimg/{conversationid}")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UpdateArticleImg(int conversationid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            //取得話題資訊
            var conversationInfo = db.Conversations.Where(a => a.Id == conversationid).FirstOrDefault();

            //取得話題作者是否存在
            var conversationUserInfo = db.Conversations.Any(a => a.Id == conversationid && a.UserId == userid);

            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // 檢查資料夾是否存在，若無則建立
            string root = HttpContext.Current.Server.MapPath("~/upload/conversationcover");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            if (conversationInfo == null)
            {
                return BadRequest("該話題不存在，無法修改話題封面");
            }
            else if (!conversationUserInfo)
            {
                return BadRequest("只有話題作者才能修改話題封面");
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
                    string fileName = conversationid + "conversationcover" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

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
                    conversationInfo.CoverUrl = fileName;

                    //儲存變更文章封面到資料庫
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "話題封面更新成功"
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
        /// 新增話題圖片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/conversationcontentimgurl/create")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UpdateConversationImg()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            var userExist = db.Users.Any(u => u.Id == userid);

            if (userExist)//使用者存在
            {
                // 檢查請求是否包含 multipart/form-data.
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                // 檢查資料夾是否存在，若無則建立
                string root = HttpContext.Current.Server.MapPath("~/upload/conversationcontentimg");
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
                        string fileName = "conversationcontentimg" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileType;

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

                        //更新話題圖片欄位
                        imglList.Add(fileName);
                    }

                    //創建話題圖片資料庫
                    var newPost = new ConversationContentImg();

                    string imgUrl = string.Join(";", imglList);
                    newPost.ImgUrl = imgUrl;

                    newPost.InitDate = DateTime.Now;

                    //圖片資料新增進SQL
                    db.ConversationContentImgs.Add(newPost);

                    // 儲存話題圖片到資料庫
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "話題圖片上傳成功",
                        ConversationContentImgData = imglList.Select(img => "https://islandofhealing.rocket-coding.com/upload/conversationcontentimg/" + img)
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
            }
            else
            {
                return BadRequest("使用者不存在");
            }
        }

        /// <summary>
        /// 刪除話題
        /// </summary>
        /// <param name="conversationid">話題id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/conversation/delete/{conversationid}")]
        [JwtAuthFilter]
        public IHttpActionResult DeleteConversation(int conversationid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            //話題是否屬於使用者
            var conversationBelongUser = db.Conversations.Any(c => c.Id == conversationid && c.UserId == userid);

            //取得話題資訊
            var conversationInfo = db.Conversations.Where(c => c.Id == conversationid).FirstOrDefault();

            if (conversationInfo == null)//話題不存在
            {
                return BadRequest("話題不存在");
            }
            else//話題存在
            {
                if (!conversationBelongUser)//話題不屬於使用者
                {
                    return BadRequest("只有話題的發布者才能刪除話題");
                }
                else
                {
                    // 從資料庫中刪除話題
                    db.Conversations.Remove(conversationInfo);
                    db.SaveChanges();

                    // 話題刪除成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "話題刪除成功"
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 更新話題(不包含封面照)
        /// </summary>
        /// <param name="conversationid">話題編號id</param>
        /// <param name="inputCreateConversation">話題標題、話題內容、標籤、匿名、話題分類(XX相談室)、內容摘要</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/conversation/update/{conversationid}")]
        [JwtAuthFilter]
        public IHttpActionResult UpdateConversation(int conversationid, ViewModel.InputCreateConversation inputCreateConversation)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
            int userid = (int)jwtObject["Id"];

            //判斷話題是否存在
            var conversationExist = db.Conversations.Any(c => c.Id == conversationid);

            //取得話題資料
            var conversationInfo = db.Conversations.Where(c => c.Id == conversationid && c.UserId == userid).FirstOrDefault();

            if (!conversationExist)//話題不存在
            {
                return BadRequest("該話題不存在，無法修改話題");
            }
            else//話題存在
            {
                if (conversationInfo == null)
                {
                    return BadRequest("只有話題的發布者才能修改文章");
                }
                else
                {
                    conversationInfo.Content = inputCreateConversation.Content;
                    conversationInfo.ConversationCategoryId = inputCreateConversation.ConversationsCategoryId;
                    conversationInfo.Title = inputCreateConversation.Title;
                    conversationInfo.Anonymous = inputCreateConversation.Anonymous;
                    conversationInfo.Summary = inputCreateConversation.Summary;
                    conversationInfo.Tags = string.Join(";", inputCreateConversation.Tags);

                    // 話題修改成功塞資料進SQL
                    db.SaveChanges();

                    // 話題修改成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "修改話題成功"
                    };

                    return Ok(result);
                }
            }
        }
    }
}
