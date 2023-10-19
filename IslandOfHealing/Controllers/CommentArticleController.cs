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
    [OpenApiTag("ArticlesPage", Description = "文章頁面")]
    public class CommentArticleController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 新增文章留言
        /// </summary>
        /// <param name="inputArticleComment">文章留言、文章編號</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/articlecomment/create")]
        [JwtAuthFilter]
        public IHttpActionResult AddArticleComment(ViewModel.ArticleComment inputArticleComment)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得作家(使用者)Id及Role
            int userId = (int)jwtObject["Id"];
            var articleExist = db.Articles.Any(a => a.Id == inputArticleComment.ArticleId && a.Progress == Progress.審核成功);

            if (!articleExist)
            {
                return BadRequest("文章不存在，或文章未審核成功，無法留言");
            }
            else
            {
                //新增文章的資料
                var newComment = new ArticleComment();
                newComment.Comment = inputArticleComment.Comment;
                newComment.ArticleId = inputArticleComment.ArticleId;
                newComment.InitDate = DateTime.Now;
                newComment.LatestDate = DateTime.Now;
                newComment.UserId = userId;

                // 文章新增成功塞資料進SQL
                db.ArticleComments.Add(newComment);
                db.SaveChanges();

                // 文章新增成功回傳
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "新增文章留言成功"
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// 更新文章留言
        /// </summary>
        /// <param name="articleCommentUpdate">文章留言Id、文章留言內容</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/articlecomment/update")]
        [JwtAuthFilter]
        public IHttpActionResult ArticleCommentDeleteUpdate(ViewModel.ArticleCommentUpdate articleCommentUpdate)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == id).FirstOrDefault();

            //取得留言資料(條件：留言id=傳入的留言id；使用者id=傳入的使用者id)
            var commentInfo = db.ArticleComments.Where(a => a.Id == articleCommentUpdate.CommentId && a.UserId == id).FirstOrDefault();

            if(userInfo == null)//使用者不存在
            {
                return BadRequest("使用者不存在");
            }
            else//使用者存在
            {
                if(commentInfo == null)//這筆留言不屬於該使用者
                {
                    return BadRequest("該留言不屬於這個使用者");
                }
                else//這筆留言屬於該使用者
                {
                    // 從資料庫中更新留言
                    commentInfo.Comment = articleCommentUpdate.Comment;
                    commentInfo.LatestDate = DateTime.Now;
                    db.SaveChanges();

                    // 留言更新成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "文章留言更新成功"
                    };
                    return Ok(result);
                }
            }
        }


        /// <summary>
        /// 刪除文章留言
        /// </summary>
        /// <param name="commentid">文章留言id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/articlecomment/delete/{commentid}")]
        [JwtAuthFilter]
        public IHttpActionResult ArticleCommentDelete(int commentid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int userId = (int)jwtObject["Id"];


            //判斷留言是否存在
            var commentExist = db.ArticleComments.Any(a => a.Id == commentid);

            if (!commentExist)
            {

                return BadRequest("留言不存在，無法刪除");
            }
            else
            {
                //取得該使用者的留言資料，若返回null代表該留言不是該使用者所留的言
                var commentInfo = db.ArticleComments.Where(a => a.Id == commentid && a.UserId == userId).FirstOrDefault();

                if(commentInfo == null)
                {
                    return BadRequest("只有該留言的使用者才能刪除留言");
                }
                else
                {
                    // 從資料庫中刪除留言
                    db.ArticleComments.Remove(commentInfo);
                    db.SaveChanges();

                    // 留言刪除成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "留言刪除成功"
                    };
                    return Ok(result);
                }
            }
        }
    }
}
