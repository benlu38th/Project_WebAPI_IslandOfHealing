using NSwag.Annotations;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using IslandOfHealing.Models;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("AdministratorSystem", Description = "後台系統")]
    public class HelloWorldController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// Pusher-發送使用者成為作家訊息
        /// </summary>
        /// <param name="writerid">成為作家的使用者id</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/pusher/bewriter")]
        public async Task<ActionResult> PusherBeWriter(int writerid)
        {
            var options = new PusherOptions
            {
                Cluster = "ap3",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1639672",
              "e8873cf4694b7f36fb0c",
              "fae30c09af3bbfc7a192",
              options);

            var channelNumbers = db.Users.Select(u => u.Id).ToList();

            var DateOneMonthAgo = DateTime.Now.AddMonths(-1);

            var channelName = $"my-channel-{writerid}";

            var userInfo = db.Users.Where(u => u.Id == writerid).FirstOrDefault();

            var notification = db.Notifications.Where(n => n.UserId == writerid && n.InitDate > DateOneMonthAgo).OrderByDescending(n => n.InitDate).ToList();

            var MessageResponse = new
            {
                StatusCode = (int)HttpStatusCode.OK,
                Status = "success",
                Message = "取得過去一個月訊息成功",
                UserId = writerid,
                UserNickName = userInfo.NickName,
                UnReadNum = notification.Where(n => n.IsRead == false).Count(),
                Notification = notification.Select(n => new
                {
                    n.Id,
                    n.MyNotificationSender.Name,
                    n.MyNotificationSender.ImgUrl,
                    n.NotificationContentId,
                    n.NotificationContent,
                    n.FollowedWriterNewArticleId,
                    n.FollowedWriterNewArticleTitle,
                    n.IsRead,
                    n.InitDate
                })
            };

            //foreach (var channelNumber in channelNumbers)
            //{
            //    var channelName = $"my-channel-{channelNumber}";

            //    var userInfo = db.Users.Where(u => u.Id == channelNumber).FirstOrDefault();

            //    var notification = db.Notifications.Where(n => n.UserId == channelNumber && n.InitDate > DateOneMonthAgo).OrderByDescending(n => n.InitDate).ToList();

            //    var MessageResponse = new
            //    {
            //        StatusCode = (int)HttpStatusCode.OK,
            //        Status = "success",
            //        Message = "取得過去一個月訊息成功",
            //        UserId = channelNumber,
            //        UserNickName = userInfo.NickName,
            //        UnReadNum = notification.Where(n => n.IsRead == false).Count(),
            //        Notification = notification.Select(n => new
            //        {
            //            n.Id,
            //            n.MyNotificationSender.Name,
            //            n.MyNotificationSender.ImgUrl,
            //            n.NotificationContentId,
            //            n.NotificationContent,
            //            n.FollowedWriterNewArticleId,
            //            n.FollowedWriterNewArticleTitle,
            //            n.IsRead,
            //            n.InitDate
            //        })
            //    };

            var result = await pusher.TriggerAsync(
                  channelName,
                  "my-event",
                  new { MessageResponse });

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Pusher-發送作家發布新文章審核成功訊息
        /// </summary>
        /// <param name="articleid">審核成功文章id</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/pusher/newarticleapproved")]
        public async Task<ActionResult> PusherNewArticleApproved(int articleid)
        {
            var options = new PusherOptions
            {
                Cluster = "ap3",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1639672",
              "e8873cf4694b7f36fb0c",
              "fae30c09af3bbfc7a192",
              options);

            //找出文章的作者id
            var articleWriterInfo = db.Articles.Where(a => a.Id == articleid).Select(a => a.MyUser).FirstOrDefault();

            var channelName = $"my-channel-{articleWriterInfo.Id}";

            var DateOneMonthAgo = DateTime.Now.AddMonths(-1);

            var notification = db.Notifications.Where(n => n.UserId == articleWriterInfo.Id && n.InitDate > DateOneMonthAgo).OrderByDescending(n => n.InitDate).ToList();

            var MessageResponse = new
            {
                StatusCode = (int)HttpStatusCode.OK,
                Status = "success",
                Message = "取得過去一個月訊息成功",
                UserId = articleWriterInfo.Id,
                UserNickName = articleWriterInfo.NickName,
                UnReadNum = notification.Where(n => n.IsRead == false).Count(),
                Notification = notification
            };

            var result = await pusher.TriggerAsync(
              channelName,
              "my-event",
              new { MessageResponse });

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Pusher-發送使用者追蹤作家發布新文章訊息
        /// </summary>
        /// <param name="articleid">新文章的id</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/pusher/followedwriternewarticle")]
        public async Task<ActionResult> PusherFollowedWriterNewArticle(int articleid)
        {
            var options = new PusherOptions
            {
                Cluster = "ap3",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1639672",
              "e8873cf4694b7f36fb0c",
              "fae30c09af3bbfc7a192",
              options);

            //找出所有的追蹤者
            var articleWriterFans = db.Articles.Where(a => a.Id == articleid).Select(a => a.MyUser.FollowWriters).ToList();

            var DateOneMonthAgo = DateTime.Now.AddMonths(-1);

            foreach (var channelNumber in articleWriterFans[0])
            {
                var userInfo = db.Users.Where(u => u.Id == channelNumber.FollowerId).FirstOrDefault();

                if(userInfo != null)
                {
                    var channelName = $"my-channel-{channelNumber.FollowerId}";

                    var notification = db.Notifications.Where(n => n.UserId == channelNumber.FollowerId && n.InitDate > DateOneMonthAgo).OrderByDescending(n => n.InitDate).ToList();

                    var UnReadNum = notification.Where(n => n.IsRead == false).Count();

                    var MessageResponse = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得過去一個月訊息成功",
                        UserId = userInfo.Id,
                        UserNickName = userInfo.NickName,
                        UnReadNum,
                        Notification = notification.Select(n => new
                        {
                            n.Id,
                            n.MyNotificationSender.Name,
                            n.MyNotificationSender.ImgUrl,
                            n.NotificationContentId,
                            n.NotificationContent,
                            n.FollowedWriterNewArticleId,
                            n.FollowedWriterNewArticleTitle,
                            n.IsRead,
                            n.InitDate
                        })
                    };

                    var result = await pusher.TriggerAsync(
                      channelName,
                      "my-event",
                      new { MessageResponse });
                }
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.OK);
        }
    }
}

