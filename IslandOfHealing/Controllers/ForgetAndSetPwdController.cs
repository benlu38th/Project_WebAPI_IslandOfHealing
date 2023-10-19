using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using System.Net.Mail;
using System.Security.Cryptography;
using IslandOfHealing.Models.Function;
using System.Text.RegularExpressions;
using IslandOfHealing.Security;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("Users", Description = "會員設定")]
    public class ForgetAndSetPwdController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 更新密碼
        /// </summary>
        /// <param name="setPwd">帳號、密碼、密碼確認</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/setpwd")]
        [SwaggerResponse(typeof(ViewModel.LoginSignUp))]
        public IHttpActionResult Put(ViewModel.SetPwd setPwd)
        {
            string newPwd = setPwd.Password;
            string newPwdConfirn = setPwd.ConfirmPassword;

            //密碼至少包含英文大寫、英文小寫、數字各一
            Boolean passwordCheck = Regex.IsMatch(newPwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$");

            if (newPwd != newPwdConfirn)
            {
                //密碼與密碼確認不相同
                var result = "密碼與密碼確認不相同";

                return BadRequest(result);
            }
            else if (!ModelState.IsValid || !passwordCheck)
            {
                // 帳號或重設的密碼不符合格式
                var result = "重設的密碼格式不符";

                return BadRequest(result);
            }
            else
            {
                // 解密後會回傳 Json 格式的物件 (即加密前的資料)
                var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

                int id = (int)jwtObject["Id"];
                var user = db.Users.Where(u => u.Id == id).FirstOrDefault();

                //產生鹽
                string salt = Utility.CreateSalt(16);

                //產生雜湊後要存進資料庫的密碼
                var hashPwd = Utility.CreatePasswordHash(newPwd, salt);

                //更新使用者(密碼、鹽)欄位
                user.Password = hashPwd;
                user.Salt = salt;

                //儲存變更到資料庫
                db.SaveChanges();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "修改成功",
                };

                return Ok(result);
            }
        }

        /// <summary>
        /// 忘記密碼，輸入email傳送重設連結
        /// </summary>
        /// <param name="email">電子信箱</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/forgetpwd")]
        public IHttpActionResult Post(string email)
        {
            try
            {
                // 根據account從資料庫中找到對應的使用者，如果找不到回傳null
                var user = db.Users.FirstOrDefault(u => u.Account == email);

                if (user!=null)
                {
                    string guid = Guid.NewGuid().ToString();

                    //更新使用者的Guid
                    user.Guid = guid;

                    //更新密碼時限
                    user.PasswordTime = DateTime.Now.AddMinutes(10);

                    //儲存變更到資料庫
                    db.SaveChanges();

                    // 生成重置密碼的連結
                    string resetLink = Utility.GenerateResetPasswordLink(email, guid);

                    // 發送重置密码的電子信箱
                    Utility.SendResetPasswordEmail(email, resetLink);

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "Email已發送，請檢查信箱"
                    };
                    return Ok(result);
                }
                else
                {
                    var result = "Email未被註冊";
                    return BadRequest(result);
                }

            }
            catch (Exception ex)
            {

                var result = "發送電子郵件失敗。錯誤訊息：" + ex.Message;

                return BadRequest(result);
            }
        }

        /// <summary>
        /// 激活平台提供忘記密碼使用者的新密碼
        /// </summary>
        /// <param name="email">使用者Account</param>
        /// <param name="guid">使用者GUID</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/resetpwdactive")]
        public IHttpActionResult Post(string email, string guid)
        {
            // 根據account及guid從資料庫中找到對應的使用者，如果找不到回傳null
            var user = db.Users.FirstOrDefault(u => u.Account == email && u.Guid == guid );

            if (user != null)
            {
                if (user.PasswordTime.Value >= DateTime.Now)
                {
                    //新密碼
                    string newPwd = "A" + guid.Substring(guid.Length - 12);

                    //產生鹽
                    string salt = Utility.CreateSalt(16);

                    //產生雜湊後要存進資料庫的密碼
                    var hashPwd = Utility.CreatePasswordHash(newPwd, salt);

                    //更新使用者(密碼、鹽、Guid)欄位
                    user.Password = hashPwd;
                    user.Salt = salt;
                    user.Guid = Guid.NewGuid().ToString();

                    //儲存變更到資料庫
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "新密碼已更新，請盡快登入，並重設密碼"
                    };
                    return Ok(result);
                }
                else
                {
                    var result = "超過密碼更新時限";
                    return BadRequest(result);
                }
            }
            else
            {
                var result = "新密碼更新失敗";
                return BadRequest(result);
            }
        }

    }

}
