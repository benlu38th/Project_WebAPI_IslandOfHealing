using IslandOfHealing.Security;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Models.Function;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("Users", Description = "會員設定")]
    public class UserFunctionController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 刪除訊息
        /// </summary>
        /// <param name="notificationid">訊息編號</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/notification/delete")]
        [JwtAuthFilter]
        public IHttpActionResult NotificationDelete(int notificationid)
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
                var notificationInfo = db.Notifications.Where(n => n.Id == notificationid && n.UserId == id).FirstOrDefault();

                if (notificationInfo == null)
                {
                    return BadRequest("訊息不存在或該訊息非屬於使用者");
                }
                else
                {
                    db.Notifications.Remove(notificationInfo);
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "刪除訊息成功",
                        NotificationId = notificationInfo.Id
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 已讀訊息
        /// </summary>
        /// <param name="notificationid">訊息編號</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/readnotification")]
        [JwtAuthFilter]
        public IHttpActionResult ReadNotification(int notificationid)
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
                var notificationInfo = db.Notifications.Where(n => n.Id == notificationid && n.UserId == id).FirstOrDefault();

                if (notificationInfo == null)
                {
                    return BadRequest("訊息不存在或該訊息非屬於使用者");
                }
                else
                {
                    notificationInfo.IsRead = true;
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "已讀訊息成功",
                        NotificationId = notificationInfo.Id
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 取得使用者過去一個月的訊息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/notifications/get")]
        [JwtAuthFilter]
        public IHttpActionResult NotificationsGet()
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
                var DateOneMonthAgo = DateTime.Now.AddMonths(-1);

                var notification = db.Notifications.Where(n => n.UserId == id && n.InitDate > DateOneMonthAgo).OrderByDescending(n => n.InitDate).ToList();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得過去一個月訊息成功",
                    UserId = userInfo.Id,
                    UserNickName = userInfo.NickName,
                    UnReadNum = notification.Where(n=>n.IsRead == false).Count(),
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

                return Ok(result);
            }
        }

        /// <summary>
        /// 取得申請作家進度
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/applyforwriterprogress/get")]
        [JwtAuthFilter]
        public IHttpActionResult GetApplyForWriterProgress()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo == null)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得申請作家進度成功",
                    WriterProgress = userInfo.WriterProgress.ToString()
                };

                return Ok(result);

            }
        }

        /// <summary>
        /// 續訂會員、取消續訂會員
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/cancelrenewmembership")]
        [JwtAuthFilter]
        public IHttpActionResult CancelRenewMembership()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            if (userInfo == null)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                if (userInfo.RenewMembership == true)//目前續訂=true
                {
                    userInfo.RenewMembership = false;

                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取消續訂會員成功",
                        RenewMembership = false
                    };

                    return Ok(result);
                }
                else //目前續訂=false
                {
                    userInfo.RenewMembership = true;

                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "續訂會員成功",
                        RenewMembership = true
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 追蹤作家
        /// </summary>
        /// <param name="writerid">作家id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/writer/follow/{writerid}")]
        [JwtAuthFilter]
        public IHttpActionResult CollectWriter(int writerid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出追蹤作家資料，不存在返回null
            var CollectWriterInfo = db.FollowtWriters.Where(c => c.FollowerId == id && c.WriterWhoBeFollowedId == writerid).FirstOrDefault();

            //判斷追蹤者是否存在
            var follwerExist = db.Users.Any(a => a.Id == id);

            //判斷被追蹤者id是否存在且是作家身分
            var writerExist = db.Users.Any(a => a.Id == writerid && a.Role == "writer");

            if (!follwerExist)//追蹤者不存在
            {
                return BadRequest("追蹤者不存在");
            }
            else//追蹤者存在
            {
                if (!writerExist)//被追蹤者(作家)不存在或被追蹤者不是作家
                {
                    return BadRequest("被追蹤者不存在或被追蹤者不是作家");
                }
                else//被追蹤者存在且被追蹤者是作家
                {
                    if (CollectWriterInfo == null)//未建立資料
                    {
                        //寫法1
                        var newCollectWriters = new FollowWriters();
                        newCollectWriters.FollowerId = id;
                        newCollectWriters.WriterWhoBeFollowedId = writerid;
                        newCollectWriters.InitDate = DateTime.Now;

                        db.FollowtWriters.Add(newCollectWriters);
                        db.SaveChanges();

                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "追蹤作家成功"
                        };

                        return Ok(result);
                    }
                    else//已建立資料
                    {
                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "已追蹤過此作家"
                        };

                        return Ok(result);
                    }
                }
            }
        }

        /// <summary>
        /// 取消追蹤作家
        /// </summary>
        /// <param name="writerid">被追蹤者(作家)id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/writer/cancelfollow/{writerid}")]
        [JwtAuthFilter]
        public IHttpActionResult CancelCollectWriter(int writerid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出追蹤作家資料，不存在返回null
            var CollectWriterInfo = db.FollowtWriters.Where(c => c.FollowerId == id && c.WriterWhoBeFollowedId == writerid).FirstOrDefault();

            //判斷追蹤者是否存在
            var follwerExist = db.Users.Any(a => a.Id == id);

            //判斷被追蹤者id是否存在且是作家身分
            var writerExist = db.Users.Any(a => a.Id == writerid && a.Role == "writer");

            if (!follwerExist)//追蹤者不存在
            {
                return BadRequest("追蹤者不存在");
            }
            else//追蹤者存在
            {
                if (!writerExist)//被追蹤者(作家)不存在或被追蹤者不是作家
                {
                    return BadRequest("被追蹤者不存在或被追蹤者不是作家");
                }
                else//被追蹤者存在且被追蹤者是作家
                {
                    if (CollectWriterInfo == null)//未建立資料
                    {
                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "未追蹤過此作家"
                        };

                        return Ok(result);
                    }
                    else//已建立資料
                    {
                        // 從資料庫中刪除追蹤作家資料
                        db.FollowtWriters.Remove(CollectWriterInfo);
                        db.SaveChanges();
                        var result = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "已刪除追蹤此作家"
                        };

                        return Ok(result);
                    }
                }
            }
        }

        /// <summary>
        /// 瀏覽個人追蹤作家清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/followedwriter/get")]
        [JwtAuthFilter]
        public IHttpActionResult FollowedWriterGet()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出追蹤作家資料，不存在返回null
            var userExist = db.Users.Any(a => a.Id == id);

            if (!userExist)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                var followWritersInfo = db.FollowtWriters.Where(f => f.FollowerId == id);

                if (followWritersInfo.FirstOrDefault() == null) //使用者未追蹤任何作家
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "未追蹤任何作家"
                    };

                    return Ok(result);
                }
                else //使用者有追蹤作家
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得追蹤作家成功",
                        Data = new List<object>()
                    };

                    foreach (var eachFollowedWriter in followWritersInfo.ToList())
                    {
                        var Data = new
                        {
                            WriterId = eachFollowedWriter.MyUser.Id,
                            NickName = eachFollowedWriter.MyUser.NickName,
                            Bio = eachFollowedWriter.MyUser.Bio,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + eachFollowedWriter.MyUser.ImgUrl,
                            JobTitle = eachFollowedWriter.MyUser.JobTitle,
                            IsFollowing = true
                        };

                        result.Data.Add(Data);
                    }

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 瀏覽個人收藏文章清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/collectedarticles/get")]
        [JwtAuthFilter]
        public IHttpActionResult CollectedArticlesGet()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出追蹤作家資料，不存在返回null
            var userExist = db.Users.Any(a => a.Id == id);

            if (!userExist)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                var collectInfo = db.CollectLikes.Where(f => f.UserId == id && f.Collect == true);

                if (!collectInfo.Any()) //未收藏任何文章
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "未收藏任何文章"
                    };

                    return Ok(result);
                }
                else//有收藏文章
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得收藏文章成功",
                        //作法1
                        Data = collectInfo.ToList().Select(c => new
                        {
                            WriterId = c.MyArticle.MyUser.Id,
                            WriterNickName = c.MyArticle.MyUser.NickName,
                            WriterImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + c.MyArticle.MyUser.ImgUrl,
                            ArticleId = c.ArticleId,
                            ArticleImgUrl = Utility.ReturnArticleCover(c.MyArticle.CoverUrl),
                            Title = c.MyArticle.Title,
                            Summary = c.MyArticle.Summary,
                            Initdate = c.MyArticle.InitDate,
                            IsCollected = true
                        })
                        ////作法2
                        //Data = new List<object>()
                    };

                    ////作法2
                    //foreach (var eachCollectedArticle in collectInfo)
                    //{
                    //    var articleData = new
                    //    {
                    //        WriterId = eachCollectedArticle.MyArticle.MyUser.Id,
                    //        WriterImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + eachCollectedArticle.MyArticle.MyUser.ImgUrl,
                    //        ArticleId = eachCollectedArticle.ArticleId,
                    //        ArticleImgUrl = Utility.ReturnArticleCover(c.MyArticle.CoverUrl),
                    //        Title = eachCollectedArticle.MyArticle.Title,
                    //        Summary = eachCollectedArticle.MyArticle.Summary,
                    //        Initdate = eachCollectedArticle.MyArticle.InitDate
                    //    };

                    //    result.Data.Add(articleData);
                    //}

                    return Ok(result);
                }
            }
        }

    }
}

