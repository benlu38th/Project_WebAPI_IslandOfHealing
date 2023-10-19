using IslandOfHealing.Security;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;

namespace IslandOfHealing.Controllers
{

    [OpenApiTag("AIChatroom", Description = "AI聊天室")]
    public class AIController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 取得使用者使用AI次數
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/useaitimes/get")]
        [JwtAuthFilter]
        public IHttpActionResult GetUseAITimes()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("使用者不存在");
            }
            else
            {
                int UserAITimes = userInfo.UseAI;

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得已問問題的次數成功",
                    UserAITimes
                };

                return Ok(result);
            }
        }

        /// <summary>
        /// 使用者使用AI次數+1
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/useaitimes/add")]
        [JwtAuthFilter]
        public IHttpActionResult AddUseAITimes()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("使用者不存在");
            }
            else
            {
                int UserAITimes = userInfo.UseAI;

                if (UserAITimes >= 5)
                {
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "使用AI次數已超過5次"
                    };

                    return Ok(result);
                }
                else
                {
                    userInfo.UseAI += 1;
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "使用AI次數+1",
                    };

                    return Ok(result);
                }

            }
        }

        /// <summary>
        /// 儲存聊天記錄
        /// </summary>
        /// <param name="inputAIMessage"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/aiquestionanswers/create")]
        [JwtAuthFilter]
        public IHttpActionResult AIQuestionAnswersCreate(ViewModel.InputAIMessage inputAIMessage)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("使用者不存在");
            }
            else
            {
                //判斷AI角色(列舉型別)是否存在
                if (!Enum.IsDefined(typeof(AI), inputAIMessage.AI))//AI角色不存在
                {
                    return BadRequest("AI角色不存在");
                }
                else//AI角色存在
                {
                    var newAIMessage = new AIMessage();
                    newAIMessage.UserId = id;
                    newAIMessage.UserNickName = userInfo.NickName;
                    newAIMessage.UserQuestion = inputAIMessage.UserQuestion;
                    newAIMessage.AI = inputAIMessage.AI;
                    newAIMessage.AIAnswer = inputAIMessage.AIAnswer;
                    newAIMessage.InitDate = DateTime.Now;

                    db.AIMessages.Add(newAIMessage);
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "儲存AI聊天紀錄成功"
                    };

                    return Ok(result);
                }
            }
        }
    }
}
