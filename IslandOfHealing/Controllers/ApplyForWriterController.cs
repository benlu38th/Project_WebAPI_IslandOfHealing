using IslandOfHealing.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using NSwag.Annotations;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("Users", Description = "會員設定")]
    public class ApplyForWriterController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 申請成為作家(未申請、審核失敗變成已申請)
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/applyforwriter")]
        [JwtAuthFilter]
        public IHttpActionResult ApplyForWriter()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if(userInfo == null)
            {
                return BadRequest("使用者不存在");
            }
            else if (userInfo.Role == "administrator")
            {
                return BadRequest("管理者不得申請為作家");
            }
            else if(userInfo.WriterProgress == WriterProgress.未申請 || userInfo.WriterProgress == WriterProgress.申請失敗)
            {
                //將更新的申請作家狀態存入
                userInfo.WriterProgress = WriterProgress.已申請;

                db.SaveChanges();

                //返回結果
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "已成功遞交申請"
                };

                return Ok(result);
            }
            else
            {
                return BadRequest("使用者目前申請作家中或已經申請作家成功");
            }
        }
    }
}
