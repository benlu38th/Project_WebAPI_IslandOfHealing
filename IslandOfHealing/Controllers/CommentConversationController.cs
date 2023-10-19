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
    [OpenApiTag("ForumSystem", Description = "論壇")]
    public class CommentConversationController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 新增話題留言
        /// </summary>
        /// <param name="inputConversationComment">話題留言、話題編號</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/conversationcomment/create")]
        [JwtAuthFilter]
        public IHttpActionResult AddConversationComment(ViewModel.ConversationComment inputConversationComment)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得作家(使用者)Id及Role
            int userId = (int)jwtObject["Id"];

            //判斷話題是否存在
            var conversationExist = db.Conversations.Any(c => c.Id == inputConversationComment.ConversationId);

            //判斷使用者是否存在
            var userExist = db.Users.Any(u => u.Id == userId);

            if (!conversationExist)//話題不存在
            {
                return BadRequest("話題不存在，無法留言");
            }
            else//話題存在
            {
                if (!userExist)//使用者不存在
                {
                    return BadRequest("使用者不存在，無法留言");
                }
                else//使用者存在
                {
                    //新增話題的資料
                    var newComment = new ConversationComment();
                    newComment.Comment = inputConversationComment.Comment;
                    newComment.ConversationId = inputConversationComment.ConversationId;
                    newComment.InitDate = DateTime.Now;
                    newComment.LatestDate = DateTime.Now;
                    newComment.UserId = userId;

                    // 話題留言新增成功塞資料進SQL
                    db.ConversationComments.Add(newComment);
                    db.SaveChanges();

                    // 話題留言新增成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "新增話題留言成功"
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 更新話題留言
        /// </summary>
        /// <param name="inputConversationCommentUpdate">話題留言Id、話題留言內容</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/conversationcomment/update")]
        [JwtAuthFilter]
        public IHttpActionResult ConversationCommentUpdate(ViewModel.ConversationCommentUpdate inputConversationCommentUpdate)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            //取得留言資料(條件：留言id=傳入的留言id；使用者id=傳入的使用者id)
            var commentInfo = db.ConversationComments.Where(c => c.Id == inputConversationCommentUpdate.CommentId && c.UserId == id).FirstOrDefault();

            if (userInfo == null)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                if (commentInfo == null)//這筆留言不屬於該使用者
                {
                    return BadRequest("該留言不屬於這個使用者");
                }
                else//這筆留言屬於該使用者
                {
                    // 從資料庫中更新留言
                    commentInfo.Comment = inputConversationCommentUpdate.Comment;
                    commentInfo.LatestDate = DateTime.Now;
                    db.SaveChanges();

                    // 留言更新成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "話題留言更新成功"
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 刪除話題留言
        /// </summary>
        /// <param name="commentid">話題留言id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/conversationcomment/delete/{commentid}")]
        [JwtAuthFilter]
        public IHttpActionResult ConversationCommentDelete(int commentid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int userId = (int)jwtObject["Id"];

            //判斷留言是否存在
            var commentExist = db.ConversationComments.Any(c => c.Id == commentid);

            if (!commentExist)//留言不存在
            {

                return BadRequest("留言不存在，無法刪除");
            }
            else//留言存在
            {
                //取得該使用者的留言資料，若返回null代表該留言不是該使用者所留的言
                var commentInfo = db.ConversationComments.Where(c => c.Id == commentid && c.UserId == userId).FirstOrDefault();

                if (commentInfo == null)//留言不屬於使用者
                {
                    return BadRequest("只有該留言的發布者才能刪除留言");
                }
                else//留言屬於使用者
                {
                    // 從資料庫中刪除留言
                    db.ConversationComments.Remove(commentInfo);
                    db.SaveChanges();

                    // 留言刪除成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "話題留言刪除成功"
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 瀏覽特定話題
        /// </summary>
        /// <param name="inputReadCategoryConversations">話題分類編號、現在在第幾頁</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/readcategoryconversations")]
        public IHttpActionResult ReadCategoryConversations(ViewModel.ReadCategoryConversations inputReadCategoryConversations)
        {

            //取得話題分類Id
            int categoryId = inputReadCategoryConversations.ConversationsCategoryId;

            //一頁幾篇文章
            int pageSize = 8;

            //現在在第幾頁
            int pageNumber = inputReadCategoryConversations.Page;

            //取得話題類別
            var category = db.ConversationsCategories.Where(c => c.Id == categoryId).FirstOrDefault();

            if (category == null)//話題類別不存在
            {
                return BadRequest("話題類別不存在");
            }
            else//話題類別存在
            {
                if (pageNumber <= 0 || pageSize <= 0)//頁數和一頁幾個不得小於等於0
                {
                    return BadRequest("現在第幾頁請輸入>0的正整數");
                }
                else
                {
                    //取得文章資料
                    var conversationsInfo = db.Conversations
                        .Where(c => c.ConversationCategoryId == categoryId)
                        .OrderByDescending(c => c.InitDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

                    //取得總項目
                    int totalItems = db.Conversations
                        .Where(a => a.ConversationCategoryId == categoryId).Count();

                    //取得總頁數
                    int TotalPages = ((totalItems % pageSize) != 0) ? (totalItems / pageSize) + 1 : (totalItems / pageSize);

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得特定分類的話題成功",
                        CategoryId = category.Id,
                        Category = category.Name,
                        CategoryDescription = category.Description,
                        NowPage = pageNumber,
                        TotalPages,
                        TotalConversations = totalItems,
                        ConversationsData = conversationsInfo.Select(c => new
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Summary = c.Summary,
                            Initdate = c.InitDate,
                            Anonymous = c.Anonymous,
                            PosterId = c.MyUser.Id,
                            PosterNickName = (c.Anonymous) ? "匿名" : c.MyUser.NickName,
                            PosterImgUrl = (c.Anonymous == true) ? "" : "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + c.MyUser.ImgUrl,
                            ConversationImgUrl =  (c.CoverUrl == "" || c.CoverUrl == null) ? "" : "https://islandofhealing.rocket-coding.com/upload/conversationcover/" + c.CoverUrl,
                            CommentsNum = c.ConversationComments.Count()
                        })
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 瀏覽一篇話題
        /// </summary>
        /// <param name="conversationid">話題id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/readconversation/{conversationid}")]
        public IHttpActionResult ReadOneConversation(int conversationid)
        {
            //取得話題資訊
            var conversationInfo = db.Conversations.Where(c => c.Id == conversationid).FirstOrDefault();

            // 取得該話題中的所有留言資料
            var commentInfo = db.ConversationComments.Where(c => c.ConversationId == conversationid).OrderBy(c => c.InitDate).ToList();

            if (conversationInfo == null)//話題不存在
            {
                //回傳前端"話題不存在"
                return BadRequest("話題不存在");
            }
            else//話題資訊存在
            {
                //建立回傳給前端的資料
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得一篇話題成功",
                    PosterData = new
                    {
                        Id = conversationInfo.UserId, //發布者id
                        NickName = conversationInfo.MyUser.NickName,
                        Bio = conversationInfo.MyUser.Bio,
                        ImgUrl = (conversationInfo.MyUser.ImgUrl == "" || conversationInfo.MyUser.ImgUrl == null) ? "" : "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + conversationInfo.MyUser.ImgUrl,
                    },
                    ConversationData = new
                    {
                        Id = conversationInfo.Id,
                        Title = conversationInfo.Title,
                        Content = conversationInfo.Content,
                        Initdate = conversationInfo.InitDate,
                        Tags = (conversationInfo.Tags.Split(';')[0] == "") ? new string[0] : conversationInfo.Tags.Split(';'),
                        Anonymous = conversationInfo.Anonymous,
                        Category = conversationInfo.MyConversationsCategory.Name,
                        ImgUrl = Utility.ReturnConversationCover(conversationInfo.CoverUrl),
                        Summary = conversationInfo.Summary
                    },
                    Comments = commentInfo.Select(c => new
                    {
                        CommentId = c.Id,
                        UserId = c.UserId,
                        NickName = db.Users.Where(u => u.Id == c.UserId).FirstOrDefault().NickName,
                        ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + db.Users.Where(u => u.Id == c.UserId).FirstOrDefault().ImgUrl,
                        Comment = c.Comment,
                        LatestDate = c.LatestDate
                    })
                };

                //// 取得該文章中的所有留言資料
                //var commentInfo = db.ArticleComments.Where(a => a.ArticleId == articleid);

                ////處理回傳的Comment資料
                //foreach (var eachCommentInfo in commentInfo)
                //{
                //    var userInfo = db.Users.Where(u => u.Id == eachCommentInfo.UserId).FirstOrDefault();

                //    if (userInfo != null)
                //    {
                //        var Data = new
                //        {
                //            CommentId = eachCommentInfo.Id,
                //            UserId = userInfo.Id,
                //            NickName = userInfo.NickName,
                //            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + userInfo.ImgUrl,
                //            Comment = eachCommentInfo.Comment,
                //            LatestDate = eachCommentInfo.LatestDate
                //        };

                //        result.Comment.Add(Data);
                //    }
                //}

                return Ok(result);
            }
        }


        /// <summary>
        /// 關鍵字搜尋全部話題
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <param name="nowpage">目前頁數</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/searchconversation/{keyword}/{nowpage}")]
        public IHttpActionResult SearchArticle(string keyword, int nowpage)
        {
            int pageSize = 6;  // 一頁幾個
            int pageNumber = nowpage;  //現在第幾頁

            //取得含關鍵字的全部話題
            var conversationsInfo = db.Conversations
                .Where(
                    (c => c.Title.Contains(keyword) ||
                          c.Content.Contains(keyword) ||
                         (c.Anonymous == false && c.MyUser.NickName.Contains(keyword))
                    )
                )
                .OrderByDescending(a => a.InitDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            //取得總話題數
            var totalItems = db.Conversations
                .Where(
                    (c => c.Title.Contains(keyword) ||
                          c.Content.Contains(keyword) ||
                         (c.Anonymous == false && c.MyUser.NickName.Contains(keyword))
                    )
                ).Count();

            //取得總頁數
            var totalPages = ((totalItems % pageSize) != 0) ? (totalItems / pageSize) + 1 : (totalItems / pageSize);

            if(nowpage <= 0)
            {
                return BadRequest("目前頁數請輸入大於0的正整數！");
            }
            else
            {
                //建立回傳前端資料
                var result = new
                {
                    StatusCode = HttpStatusCode.OK,
                    Status = "success",
                    Message = "關鍵字搜尋全部話題成功",
                    ConversationsNum = totalItems, //總話題數
                    TotalPages = totalPages, //總頁數
                    NowPage = nowpage, //目前所在頁數
                    ConversationsData = conversationsInfo.Select(c => new
                    {
                        Id = c.Id,
                        Title = c.Title,
                        c.Summary,
                        Initdate = c.InitDate,
                        Anonymous = c.Anonymous,
                        PosterId = c.UserId,
                        PosterNickName = (c.Anonymous == true) ? "匿名" : c.MyUser.NickName,
                        PosterImgUrl = (c.Anonymous == true) ? "" : "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + c.MyUser.ImgUrl,
                        ConversationImgUrl = (c.CoverUrl == null || c.CoverUrl == "") ? "" : "https://islandofhealing.rocket-coding.com/upload/conversationcover/" + c.CoverUrl,
                        CommentsNum = c.ConversationComments.Count()
                    })
                };

                return Ok(result);
            }
            
        }
    }
}
