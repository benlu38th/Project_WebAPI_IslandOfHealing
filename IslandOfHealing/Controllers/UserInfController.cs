using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;
using System.Web.Http.Controllers;
using System.Web;
using System.IO;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System.Threading.Tasks;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("Users", Description = "會員設定")]
    public class UserInfController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 新增使用者大頭照
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/upload/userphoto")]
        [JwtAuthFilter]
        public async Task<IHttpActionResult> UploadWriterImgURL()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            // 檢查請求是否包含 multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // 檢查資料夾是否存在，若無則建立
            string root = HttpContext.Current.Server.MapPath("~/upload/userimgurl");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            try
            {
                // 讀取 MIME 資料
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                // 取得檔案副檔名，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                string fileNameData = provider.Contents.FirstOrDefault().Headers.ContentDisposition.FileName.Trim('\"');
                string fileType = fileNameData.Remove(0, fileNameData.LastIndexOf('.')); // .jpg

                // 定義檔案名稱
                string fileName = userInfo.Account + "writerimgurl" + DateTime.Now.ToString("yyyyMMddHHmmss") + fileType;

                // 儲存圖片，單檔用.FirstOrDefault()直接取出，多檔需用迴圈
                var fileBytes = await provider.Contents.FirstOrDefault().ReadAsByteArrayAsync();
                var outputPath = Path.Combine(root, fileName);
                using (var output = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    await output.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                // 使用 SixLabors.ImageSharp 調整圖片尺寸 (正方形大頭貼)
                var image = SixLabors.ImageSharp.Image.Load<Rgba32>(outputPath);
                image.Mutate(x => x.Resize(160, 160)); // 輸入(120, 0)會保持比例出現黑邊
                image.Save(outputPath);

                //更新使用者欄位
                userInfo.ImgUrl = fileName;

                //儲存變更大頭照到資料庫
                db.SaveChanges();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "大頭照更新成功"
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); // 400
            }
        }


        /// <summary>
        /// 更新使用者資訊(暱稱、頭銜、生日、自介~)
        /// </summary>
        /// <param name="updateUserInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/updateuserinfo")]
        [JwtAuthFilter]
        public IHttpActionResult Update(ViewModel.UpdateUserInfo updateUserInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("格式不符");
            }
            else
            {
                // 解密後會回傳 Json 格式的物件 (即加密前的資料)
                var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

                int id = (int)jwtObject["Id"];
                var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

                //更新使用者欄位
                userInfo.NickName = updateUserInfo.NickName;
                userInfo.Birthday = updateUserInfo.Birthday;
                userInfo.JobTitle = updateUserInfo.JobTitle;
                userInfo.Bio = updateUserInfo.Bio;

                //儲存變更到資料庫
                db.SaveChanges();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "會員資料更新成功",
                    Nickname = updateUserInfo.NickName,
                    Birthday = updateUserInfo.Birthday,
                    Jobtitle = updateUserInfo.JobTitle,
                    Bio = updateUserInfo.Bio
                };

                return Ok(result);
            }

        }

        /// <summary>
        /// 取得會員資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/userinfo")]
        [JwtAuthFilter]
        public IHttpActionResult Get()
        {

            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];
            var userInf = db.Users.Where(u => u.Id == id).FirstOrDefault();

            // 取得成功回傳
            if (userInf != null)
            {
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得會員資料成功",
                    Data = new
                    {
                        User = new
                        {
                            Uid = userInf.Id,
                            NickName = userInf.NickName,
                            Email = userInf.Account,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + userInf.ImgUrl,
                            Role = userInf.Role,
                            Birthday = userInf.Birthday,
                            MyPlan = userInf.MyPlan,
                            Jobtitle = userInf.JobTitle,
                            Bio = userInf.Bio
                        }
                    }
                };

                return Ok(result);
            }
            else
            {
                // 註冊帳號已被使用
                var result = "id不存在";

                return BadRequest(result);
            }

        }
    }
}
