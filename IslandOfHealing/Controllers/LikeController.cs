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
    public class LikeController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 給予文章愛心
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/article/like/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult LikeArticle(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出資料，不存在返回null
            var CollectLikes = db.CollectLikes.Where(c => c.UserId == id && c.ArticleId == articleid).FirstOrDefault();

            //判斷文章id是否存在
            var articleExist = db.Articles.Any(a => a.Id == articleid && a.Progress == Progress.審核成功);

            if (articleExist)//文章存在
            {
                if (CollectLikes == null)//資料不存在
                {
                    //寫法1
                    var newCollectLike = new CollectLike();
                    newCollectLike.ArticleId = articleid;
                    newCollectLike.UserId = id;
                    newCollectLike.Like = true;
                    newCollectLike.LikeDate = DateTime.Now;
                    newCollectLike.CollectDate = DateTime.Now;

                    //給予文章愛心成功，塞資料進SQL
                    db.CollectLikes.Add(newCollectLike);
                    db.SaveChanges();

                    // 成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "已給予文章愛心"
                    };

                    return Ok(result);
                }
                else//資料存在
                {
                    //給予文章愛心
                    CollectLikes.Like = true;
                    CollectLikes.LikeDate = DateTime.Now;

                    //將資料更新至資料庫
                    db.SaveChanges();

                    // 成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "已給予文章愛心"
                    };

                    return Ok(result);
                }
            }
            else
            {
                return BadRequest("文章不存在或文章未審核成功，無法給予愛心");
            }
        }

        /// <summary>
        /// 取消文章愛心
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/article/cancellike/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult CancelLikeArticle(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取出資料，不存在返回null
            var CollectLikes = db.CollectLikes.Where(c => c.UserId == id && c.ArticleId == articleid).FirstOrDefault();

            var articleExist = db.Articles.Any(a => a.Id == articleid);

            if (articleExist)
            {
                if (CollectLikes == null)
                {
                    // 成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "已取消文章愛心"
                    };

                    return Ok(result);
                }
                else
                {
                    //取消文章愛心
                    CollectLikes.Like = false;

                    //將資料更新至資料庫
                    db.SaveChanges();

                    // 成功回傳
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "已取消文章愛心"
                    };

                    return Ok(result);
                }
            }
            else
            {
                return BadRequest("文章不存在，無法取消愛心");
            }
        }
    }
}
