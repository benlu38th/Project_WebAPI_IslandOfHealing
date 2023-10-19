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
    [OpenApiTag("AdministratorSystem", Description = "後台系統")]
    public class NotificationController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 建立已追蹤作家發布新文章(僅一篇)的訊息
        /// </summary>
        /// <param name="articleid">新文章的id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/followedwriternewarticlenotification/create")]
        [JwtAuthFilter]
        public IHttpActionResult FollowedWriterNewArticleNotificationCreate(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "administrator").FirstOrDefault();

            var articleInfo = db.Articles.Where(a => a.Id == articleid && a.Progress == Progress.審核成功).FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("平台方才可以建立訊息");
            }
            else
            {
                if (articleInfo == null)
                {
                    return BadRequest("新文章不存在或新文章未審核成功");
                }
                else
                {
                    //找出所有的追蹤者
                    var articleWriterFans = db.Articles.Where(a => a.Id == articleid).Select(a => a.MyUser.FollowWriters).ToList();

                    //儲存訊息到SQL
                    foreach (var eachArticleWriterFans in articleWriterFans[0])
                    {
                        

                        var fanInfo = db.Users.Where(u => u.Id == eachArticleWriterFans.FollowerId).FirstOrDefault();

                        if(fanInfo != null)
                        {
                            var newNotification = new Notification();

                            newNotification.InitDate = DateTime.Now;
                            newNotification.UserId = eachArticleWriterFans.FollowerId;
                            newNotification.SenderId = 1;
                            newNotification.NotificationContentId = NotificationContentId.你追蹤的作家發表了新文章;
                            newNotification.NotificationContent = "哈囉~&nbsp;" + fanInfo.NickName + "&nbsp;你追蹤的作家&nbsp;" + articleInfo.MyUser.NickName + "&nbsp;已發布新文章";
                            newNotification.FollowedWriterNewArticleId = articleid;
                            newNotification.FollowedWriterNewArticleTitle = articleInfo.Title;
                            newNotification.IsRead = false;

                            db.Notifications.Add(newNotification);
                            db.SaveChanges();
                        }
                    }

                    var result = new
                    {

                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "建立使用者已追蹤作家發布新文章訊息成功"
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 建立使用者成為作家訊息
        /// </summary>
        /// <param name="writerid">申請成為作家的使用者id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/writernotification/create")]
        [JwtAuthFilter]
        public IHttpActionResult WriterNotificationCreate(int writerid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "administrator").FirstOrDefault();

            var writerInfo = db.Users.Where(u => u.Id == writerid && u.Role == "writer").FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("平台方才可以建立訊息");
            }
            else
            {
                if (writerInfo == null)
                {
                    return BadRequest("訊息接收者不存在或尚未成為作家");
                }
                else
                {
                    var newNotification = new Notification();

                    newNotification.InitDate = DateTime.Now;
                    newNotification.UserId = writerid;
                    newNotification.SenderId = 1;
                    newNotification.NotificationContentId = NotificationContentId.恭喜你成為作家;
                    newNotification.NotificationContent = "哈囉~&nbsp;" + writerInfo.NickName + "&nbsp;恭喜你成為作家！請記得重新登入，才能啟用作家功能，盡情創作噢！";

                    db.Notifications.Add(newNotification);
                    db.SaveChanges();

                    var result = new
                    {

                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "建立使用者成為作家訊息成功"
                    };

                    return Ok(result);
                }
            }

        }

        /// <summary>
        /// 建立作家發表文章審核成功訊息
        /// </summary>
        /// <param name="articleid">審核成功的文章id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/writernewarticlenotification/create")]
        [JwtAuthFilter]
        public IHttpActionResult WriterNewArticleNotificationCreate(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "administrator").FirstOrDefault();

            var articleInfo = db.Articles.Where(a => a.Id == articleid && a.Progress == Progress.審核成功).FirstOrDefault();

            var writerInfo = db.Articles.Where(a => a.Id == articleid).Select(a => a.MyUser).Where(a=>a.Role == "writer").FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("平台方才可以建立訊息");
            }
            else
            {
                if(articleInfo == null)
                {
                    return BadRequest("文章不存在或未審核成功");
                }
                else
                {
                    if (writerInfo == null)
                    {
                        return BadRequest("訊息接收者不存在或尚未成為作家");
                    }
                    else
                    {
                        var newNotification = new Notification();

                        newNotification.InitDate = DateTime.Now;
                        newNotification.UserId = writerInfo.Id;
                        newNotification.SenderId = 1;
                        newNotification.NotificationContentId = NotificationContentId.你發表的新文章審核成功;
                        newNotification.NotificationContent = "哈囉~&nbsp;" + writerInfo.NickName + "&nbsp;你發表的新文章審核成功!";
                        newNotification.FollowedWriterNewArticleId = articleid;
                        newNotification.FollowedWriterNewArticleTitle = articleInfo.Title;

                        db.Notifications.Add(newNotification);
                        db.SaveChanges();

                        var result = new
                        {

                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "建立作家發表文章審核成功訊息"
                        };

                        return Ok(result);
                    }
                }
            }
        }
    }
}

