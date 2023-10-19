using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using NSwag.Annotations;
using System.Security.Cryptography;
using System.Web.Security;
using IslandOfHealing.Models.Function;
using System.Text.RegularExpressions;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("SignUp", Description = "註冊")]
    public class SignUpController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 註冊功能
        /// </summary>
        /// <param name="loginSignUp">註冊輸入之帳號密碼</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/signup")]
        public IHttpActionResult SignUp(ViewModel.LoginSignUp loginSignUp)
        {
            string account = loginSignUp.Account;
            string password = loginSignUp.Password;
                
            // 檢查是否有相同帳號，沒有相同的帳號user=null
            var user = db.Users.FirstOrDefault(u => u.Account == account);

            //密碼至少包含英文大寫、英文小寫、數字各一
            Boolean passwordCheck = Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$");

            if (!ModelState.IsValid || !passwordCheck)//判斷帳密是否符合格式
            {
                // 註冊帳號密碼不符合格式
                var result = "註冊格式不符";

                return BadRequest(result);
            }
            else if (user != null)//判斷帳號是否已經存在SQL
            {
                // 註冊帳號已被使用
                var result = "帳號已被使用，註冊失敗";

                return BadRequest(result);
            }
            else
            {
                //產生鹽
                string salt = Utility.CreateSalt(16);

                //產生雜湊後要存進資料庫的密碼
                var hashPwd = Utility.CreatePasswordHash(password, salt);

                //寫法1
                var newUser = new User();
                newUser.Account = account;
                newUser.Password = hashPwd;
                newUser.Salt = salt;
                newUser.NickName = "島民";
                newUser.ImgUrl = "userdefaultimgurl.jpg";
                newUser.InitDate = DateTime.Now;
                newUser.MyPlan = "free";
                newUser.Role = "user";
                newUser.Guid = Guid.NewGuid().ToString();
                newUser.UseAI = 0; 
                    
                //寫法2
                //var newUser = new User
                //{
                //    Account = account,
                //    Password = hashPwd,
                //    Salt = salt,
                //    InitDate = DateTime.Now
                //};

                //註冊成功塞資料進SQL
                db.Users.Add(newUser);
                db.SaveChanges();

                // 註冊成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "註冊成功",
                    Account = account
                };
                return Ok(result);
            }
        }
    }
}
