using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;
using NSwag.Annotations;
using IslandOfHealing.Models.Function;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("LogIn", Description = "登入")]
    public class LogInController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 登入功能
        /// </summary>
        /// <param name="loginSignUp">登入輸入之帳號密碼</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/login")]
        [SwaggerResponse(typeof(ViewModel.LoginSignUp))] // 顯示回傳資料的註解
        public IHttpActionResult LogIn(ViewModel.LoginSignUp loginSignUp)
        {

            string account = loginSignUp.Account;//前端輸入的帳號
            string password = loginSignUp.Password;//前端輸入的密碼

            // 檢查是否有相同帳號
            var user = db.Users.Where(m => m.Account == account).FirstOrDefault();

            if(user != null)
            {
                // 取出該帳號的鹽
                string userSalt = user.Salt;

                password = Utility.CreatePasswordHash(password, userSalt);

                //判斷會員是否過期
                //myplan不等於free才需要判斷
                if(user.MyPlan != "free")
                {
                    //使用者id
                    int userId = user.Id;

                    //找出使用者全部已付款的訂單
                    var orderInfo = user.Orders.Where(o => o.UserId == userId && o.Paid == true).ToList();

                    //創建布林直userIsMember，用來判斷使用者是否是為會員，預設false
                    bool userIsMember = false;

                    //遍歷使用者全部已付款的訂單，只要找到一個到期日>現在，代表使用者仍是會員，將userIsMember = true，
                    foreach (var eachOrder in orderInfo)
                    {
                        if (eachOrder.EndDate >= DateTime.Now)
                        {
                            userIsMember = true;
                            break;
                        }
                    }

                    //使用者不是會員
                    if(userIsMember == false)
                    {
                        user.MyPlan = "free";
                        db.SaveChanges();
                        user = db.Users.Where(m => m.Account == account).FirstOrDefault();
                    }
                }    
            }

            if (!ModelState.IsValid)
            {
                // 帳號密碼未按照格式
                var result = "帳號密碼不符合格式";

                return BadRequest(result);
            }
            else if (user == null)
            {
                // 無此帳號
                var result = "帳號或密碼錯誤，登入失敗";

                return BadRequest(result);
            }
            else if (password != user.Password)
            {
                // 密碼錯誤
                var result = "帳號或密碼錯誤，登入失敗";

                return BadRequest(result);
            }
            else 
            {
                // GenerateToken() 生成新 JwtToken 用法
                JwtAuthUtil jwtAuthUtil = new JwtAuthUtil();
                string jwtToken = jwtAuthUtil.GenerateToken(user.Id);

                // 登入成功
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "登入成功",
                    Token = jwtToken,// 登入成功時，回傳登入成功順便夾帶 JwtToken
                    Data = new
                    {
                        User = new
                        {
                            Uid = user.Id,
                            NickName = user.NickName,
                            Email = user.Account,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + user.ImgUrl,
                            Role = user.Role,
                            MyPlan = user.MyPlan,
                            user.RenewMembership
                        }
                    }
                };
                return Ok(result);
            }
        }
    }
}
