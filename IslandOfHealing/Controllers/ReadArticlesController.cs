using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;
using IslandOfHealing.Models.Function;

namespace IslandOfHealing.Controllers

{
    [OpenApiTag("ArticlesPage", Description = "文章頁面")]
    public class ReadArticlesController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 取得熱門使用者
        /// </summary>
        /// <param name="userid">使用者編號(未登入userid=0)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/hotwriters/get")]
        public IHttpActionResult GetHotWriters(int userid)
        {
            //取得使用者資料
            var userInfo = db.Users.Where(u => u.Id == userid);

            //取得作家資料
            var writersInfo = db.Users.Where(u => u.Role == "writer").ToList();

            // 計算過去一個月的日期範圍
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddMonths(-1);

            //取得作家的被追蹤資料(過去一個月內;groupby作家id;Select作家id的追蹤數量)
            var writersFollowedInfo = db.FollowtWriters
                .Where(f => f.InitDate < startDate && f.InitDate > endDate).ToList()//找出從現在起算過去一個月
                .GroupBy(f => f.WriterWhoBeFollowedId)//相同WriterWhoBeFollowedId為一個group
                .Select(g => new
                {
                    WriterId = g.Key,
                    FollowCount = g.Count()
                }).ToList();

            if (userInfo.FirstOrDefault() == null) //使用者不存在
            {
                ////做法1
                //var mostFollowedWriters = (from writer in writersInfo
                //                           join followed in writersFollowedInfo on writer.Id equals followed.WriterId into gj
                //                           from subFollowed in gj.DefaultIfEmpty()
                //                           select new
                //                           {
                //                               WriterId = writer.Id,
                //                               Name = writer.NickName,
                //                               Imgurl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + writer.ImgUrl,
                //                               JobTitle = writer.JobTitle,
                //                               FollowCount = (subFollowed != null) ? subFollowed.FollowCount : 0,
                //                               IsFollowed = false
                //                           })
                //                .OrderByDescending(w => w.FollowCount)
                //                .Take(6)
                //                .ToList();

                //做法2(Lambda)
                var mostFollowedWritersLambda = writersInfo
    .GroupJoin(writersFollowedInfo,
        writer => writer.Id,
        followed => followed.WriterId,
        (writer, followed) => new
        {
            Writer = writer,
            Followed = followed.FirstOrDefault()
        })
    .Select(result => new
    {
        WriterId = result.Writer.Id,
        Name = result.Writer.NickName,
        Imgurl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + result.Writer.ImgUrl,
        JobTitle = result.Writer.JobTitle,
        FollowCount = (result.Followed != null) ? result.Followed.FollowCount : 0,
        IsFollowed = false
    })
    .OrderByDescending(w => w.FollowCount)
    .Take(6)
    .ToList();

                return Ok(mostFollowedWritersLambda);

            }
            else //使用者存在
            {
                var isFollowed = db.FollowtWriters.Where(f => f.FollowerId == userid);

                ////做法1
                //var mostFollowedWriters = (from writer in writersInfo
                //                           join followed in writersFollowedInfo on writer.Id equals followed.WriterId into gj
                //                           from subFollowed in gj.DefaultIfEmpty()
                //                           select new
                //                           {
                //                               WriterId = writer.Id,
                //                               Name = writer.NickName,
                //                               Imgurl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + writer.ImgUrl,
                //                               JobTitle = writer.JobTitle,
                //                               FollowCount = (subFollowed != null) ? subFollowed.FollowCount : 0,
                //                               IsFollowed = isFollowed.Any(i => i.WriterWhoBeFollowedId == writer.Id)
                //                           })
                //                .OrderByDescending(w => w.FollowCount)
                //                .Take(6)
                //                .ToList();

                //做法2(Lambda)
                var mostFollowedWritersLambda = writersInfo
    .GroupJoin(writersFollowedInfo,
        writer => writer.Id,
        followed => followed.WriterId,
        (writer, followed) => new
        {
            Writer = writer,
            Followed = followed.FirstOrDefault()
        })
    .Select(result => new
    {
        WriterId = result.Writer.Id,
        Name = result.Writer.NickName,
        Imgurl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + result.Writer.ImgUrl,
        JobTitle = result.Writer.JobTitle,
        FollowCount = (result.Followed != null) ? result.Followed.FollowCount : 0,
        IsFollowed = isFollowed.Any(i => i.WriterWhoBeFollowedId == result.Writer.Id)
    })
    .OrderByDescending(w => w.FollowCount)
    .Take(6)
    .ToList();

                return Ok(mostFollowedWritersLambda);
            }
        }

        /// <summary>
        /// 搜尋全部文章、特定文章
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <param name="categoryid">文章分類id(全部文章categoryid=0)</param>
        /// <param name="userid">使用者id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/searcharticle/{keyword}/{categoryid}/{userid}")]
        public IHttpActionResult SearchArticle(string keyword, int categoryid, int userid)
        {
            var articlesInfo = db.Articles
                .Where(a => a.Progress == Progress.審核成功 &&
            (a.Title.Contains(keyword) || a.Content.Contains(keyword) || a.MyUser.NickName.Contains(keyword)))
                .ToList();

            var userInfo = db.Users.Where(u => u.Id == userid).FirstOrDefault();

            if (categoryid == 0) //全部文章
            {
                if (userInfo == null) //使用者不存在或未登入
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "搜尋全部文章成功",
                        ArticlesData = articlesInfo.Select(a => new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Initdate = a.InitDate,
                            WriterNickName = a.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(a.CoverUrl),
                            UserLike = false,
                            UserCollect = false
                        })
                    };

                    return Ok(result);
                }
                else //使用者存在
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "搜尋全部文章成功",
                        ArticlesData = articlesInfo.Select(a => new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Initdate = a.InitDate,
                            WriterNickName = a.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(a.CoverUrl),
                            UserLike = a.CollectLikes.Any(c => c.UserId == userid && c.Like == true),
                            UserCollect = a.CollectLikes.Any(c => c.UserId == userid && c.Collect == true)
                        })
                    };

                    return Ok(result);
                }
            }
            else //特定分類的文章
            {
                if (userInfo == null) //使用者不存在或未登入
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "搜尋特定文章成功",
                        ArticlesData = articlesInfo.Where(a => a.ArticlesCategoryId == categoryid).Select(a => new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Initdate = a.InitDate,
                            WriterNickName = a.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(a.CoverUrl),
                            UserLike = false,
                            UserCollect = false
                        })
                    };

                    return Ok(result);
                }
                else //使用者存在
                {
                    var result = new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Status = "success",
                        Message = "搜尋特定文章成功",
                        ArticlesData = articlesInfo.Where(a => a.ArticlesCategoryId == categoryid).Select(a => new
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Initdate = a.InitDate,
                            WriterNickName = a.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(a.CoverUrl),
                            UserLike = a.CollectLikes.Any(c => c.UserId == userid && c.Like == true),
                            UserCollect = a.CollectLikes.Any(c => c.UserId == userid && c.Collect == true)
                        })
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 登入帳號觀看付費文章次數(計算點擊次數 + 1)
        /// </summary>
        /// <param name="articleid"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/paidarticleclick/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult PaidArticleClick(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //使用者是否存在且是會員
            bool userExist = db.Users.Any(u => u.Id == id && (u.MyPlan == "monthly" || u.MyPlan == "yearly"));

            if (!userExist) //使用者不存在或使用者不是會員
            {
                return BadRequest("使用者不存在或使用者不是會員");
            }
            else //使用者是會員
            {
                bool articleExist = db.Articles.Any(a => a.Id == articleid && a.Pay == true && a.Progress == Progress.審核成功);

                if (!articleExist)//文章不存在或該文章不是付費文章或該文章未審核成功
                {
                    return BadRequest("文章不存在或該文章不是付費文章或該文章未審核成功");
                }
                else//文章存在
                {
                    //取得付費文章點擊資料
                    var paidArticleClicksInfo = db.PaidArticleClicks.Where(p => p.PaidArticleId == articleid && p.ClickerId == id);

                    if (paidArticleClicksInfo.FirstOrDefault() == null)//找無文章點擊資料
                    {
                        //建立新增的付費文章點擊資料
                        var newPaidArticleClicksInfo = new PaidArticleClicks();
                        newPaidArticleClicksInfo.ClickerId = id;
                        newPaidArticleClicksInfo.PaidArticleId = articleid;
                        newPaidArticleClicksInfo.InitDate = DateTime.Now;

                        //將新增的資料儲存至資料庫
                        db.PaidArticleClicks.Add(newPaidArticleClicksInfo);
                        db.SaveChanges();

                        //建立返回前端的訊息
                        var result = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Status = "success",
                            Message = "會員點擊付費文章成功，資料庫紀錄+1"
                        };

                        return Ok(result);
                    }
                    else//已經有文章點擊資料
                    {
                        //建立返回前端的訊息
                        var result = new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Status = "success",
                            Message = "會員已點擊過此付費文章，資料庫紀錄不會+1"
                        };

                        return Ok(result);
                    }
                }
            }
        }

        /// <summary>
        /// 瀏覽作家個人頁面
        /// </summary>
        /// <param name="writerid">作家id</param>
        /// <param name="userid">使用者id(未登入=0)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writerarticles/{writerid}/{userid}")]
        public IHttpActionResult WriterArticlesGet(int writerid, int userid)
        {
            //使用者資料
            var userInfo = db.Users.Where(u => u.Id == userid);

            //作家資料
            var writerExist = db.Users.Any(w => w.Id == writerid && w.Role == "writer");

            //該作家文章資料
            var writerArticlesInfo = db.Articles.Where(a => a.UserId == writerid && a.Progress == Progress.審核成功);

            if (!writerExist)//作家id不存在或此人非作家身分
            {
                return Ok("作家不存在或非作家身分");
            }
            else //作家id存在且此人是作家身分
            {
                //作家資料
                var writerInfo = db.Users.Where(w => w.Id == writerid && w.Role == "writer").FirstOrDefault();

                if (userInfo.FirstOrDefault() == null)//使用者不存在(未登入)
                {
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得作家個人頁面成功",
                        WriterData = new
                        {
                            writerInfo.Id,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + writerInfo.ImgUrl,
                            writerInfo.NickName,
                            writerInfo.JobTitle,
                            writerInfo.Bio,
                            ArticlesNum = writerInfo.Articles.Where(a => a.Progress == Progress.審核成功).Count(),
                            FanNum = writerInfo.FollowWriters.Count(),
                            FollowNum = db.FollowtWriters.Where(f => f.FollowerId == writerInfo.Id).Count(),
                            Follow = false
                        },
                        ArticlesData = writerArticlesInfo.ToList().Select(w => new
                        {
                            w.Id,
                            ImgUrl = Utility.ReturnArticleCover(w.CoverUrl),
                            w.Title,
                            w.Summary,
                            w.InitDate,
                            Collect = false,
                            Like = false
                        })
                    };

                    return Ok(result);
                }
                else//使用者存在
                {
                    //創建是否收藏文章、愛心文章的布林值
                    bool articleLike = false;
                    bool artocleCollect = false;

                    //作家是否被追蹤
                    bool followStatus = db.FollowtWriters.Any(f => f.FollowerId == userid && f.WriterWhoBeFollowedId == writerid);

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得作家個人頁面成功",
                        WriterData = new
                        {
                            writerInfo.Id,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + writerInfo.ImgUrl,
                            writerInfo.NickName,
                            writerInfo.JobTitle,
                            writerInfo.Bio,
                            ArticlesNum = writerInfo.Articles.Where(a => a.Progress == Progress.審核成功).Count(),
                            FanNum = writerInfo.FollowWriters.Count(),
                            FollowNum = db.FollowtWriters.Where(f => f.FollowerId == writerInfo.Id).Count(),
                            Follow = followStatus
                        },
                        ArticlesData = writerArticlesInfo.ToList().Select(w => new
                        {
                            w.Id,
                            ImgUrl = Utility.ReturnArticleCover(w.CoverUrl),
                            w.Title,
                            w.Summary,
                            w.InitDate,
                            Collect = db.CollectLikes.Any(c => c.UserId == userid && c.ArticleId == w.Id && c.Collect == true),
                            Like = db.CollectLikes.Any(c => c.UserId == userid && c.ArticleId == w.Id && c.Like == true)
                        })
                    };
                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 瀏覽一篇文章
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <param name="userid">使用者id(若未登入輸入0)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/readarticle/{articleid}/{userid}")]
        public IHttpActionResult ReadOneArticle(int articleid, int userid)
        {
            //取得文章資訊
            var articleInfo = db.Articles.Where(a => a.Id == articleid).FirstOrDefault();

            //取得使用者的收藏及愛心資訊
            var collectLikeInf = db.CollectLikes.Where(c => c.ArticleId == articleid && c.UserId == userid).FirstOrDefault();

            //取得回傳的愛心及收藏狀態
            bool userLikeStatus = false;
            bool userCollectStatus = false;

            if (collectLikeInf != null)//若未建立資料不需判斷，都是愛心及收藏都是false
            {
                if (collectLikeInf.Like == true)//有資料，且如果愛心是true，將愛心從false改成true
                {
                    userLikeStatus = true;
                }
                if (collectLikeInf.Collect == true)//有資料，且如果收藏是true，將收藏從false改成true
                {
                    userCollectStatus = true;
                }
            }

            if (articleInfo == null)//文章資訊不存在
            {
                //回傳前端"文章不存在"
                return BadRequest("文章不存在");
            }
            else//文章資訊存在
            {
                //判斷使用者是否有追蹤這篇文章的作者
                bool followStatus = db.FollowtWriters.Any(f => f.FollowerId == userid && f.WriterWhoBeFollowedId == articleInfo.UserId);

                //建立回傳給前端的資料
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得一篇文章成功",
                    WriterData = new
                    {
                        Id = articleInfo.UserId, //作家id
                        NickName = articleInfo.MyUser.NickName,
                        Bio = articleInfo.MyUser.Bio,
                        ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + articleInfo.MyUser.ImgUrl,
                        Follow = followStatus
                    },
                    ArticleData = new
                    {
                        Id = articleInfo.Id,
                        Title = articleInfo.Title,
                        Content = articleInfo.Content,
                        Initdate = articleInfo.InitDate,
                        Tags = (articleInfo.Tags.Split(';')[0]=="")? new string[0] : articleInfo.Tags.Split(';'),
                        Pay = articleInfo.Pay,
                        Category = articleInfo.MyArticlesCategory.Name,
                        ImgUrl = Utility.ReturnArticleCover(articleInfo.CoverUrl),
                        Summary = articleInfo.Summary,
                        IsRead = articleInfo.PaidArticleClicksInfo.Any(p => p.ClickerId == userid)
                    },
                    Like = userLikeStatus,
                    Collect = userCollectStatus,
                    Comment = new List<object>()
                };

                // 取得該文章中的所有留言資料
                var commentInfo = db.ArticleComments.Where(a => a.ArticleId == articleid);

                //處理回傳的Comment資料
                foreach (var eachCommentInfo in commentInfo)
                {
                    var userInfo = db.Users.Where(u => u.Id == eachCommentInfo.UserId).FirstOrDefault();

                    if(userInfo != null)
                    {
                        var Data = new
                        {
                            CommentId = eachCommentInfo.Id,
                            UserId = userInfo.Id,
                            NickName = userInfo.NickName,
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + userInfo.ImgUrl,
                            Comment = eachCommentInfo.Comment,
                            LatestDate = eachCommentInfo.LatestDate
                        };

                        result.Comment.Add(Data);
                    }
                }

                return Ok(result);
            }
        }

        /// <summary>
        /// 瀏覽特定文章
        /// </summary>
        /// <param name="readCategoryArticles">第幾頁、使用者編號</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/readcategoryarticles")]
        public IHttpActionResult ReadCategoryArticles(ViewModel.ReadCategoryArticles readCategoryArticles)
        {

            //取得使用者id
            int userId = readCategoryArticles.UserId;

            //取得文章分類Id
            int categoryId = readCategoryArticles.ArticlesCategoryId;

            //一頁幾篇文章
            int pageSize = 12;

            //現在第幾頁
            int pageNumber = readCategoryArticles.Page;

            //取得文章類別
            var category = db.ArticlesCategories.Where(a => a.Id == categoryId).FirstOrDefault();

            if (category == null)//文章類別不存在
            {
                return BadRequest("文章類別不存在");
            }
            else//文章類別存在
            {
                if (pageNumber <= 0 || pageSize <= 0)//頁數和一頁幾個不得小於等於0
                {
                    return BadRequest("一頁幾篇文章或目前頁數請輸入>0的正整數");
                }
                else
                {
                    

                    //取得文章資料
                    var articlesInfo = db.Articles
                        .Where(a => a.ArticlesCategoryId == categoryId && ((int)a.Progress) == 4)
                        .OrderByDescending(a => a.InitDate)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

                    //取得總項目
                    int totalItems = db.Articles
                        .Where(a => a.ArticlesCategoryId == categoryId && ((int)a.Progress) == 4).Count();

                    //取得總頁數
                    int TotalPages = ((totalItems % pageSize) != 0) ? (totalItems / pageSize)+1 : (totalItems / pageSize);

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得特定分類的文章成功",
                        Category = category.Name,
                        TotalPages,
                        ArticleData = new List<object>()
                    };

                    //創建收藏及愛心狀態，改下面foreach處理用
                    bool collectStatus;
                    bool likeStatus;

                    //處理回傳的特定類別文章資料
                    foreach (var eacharticleInfo in articlesInfo)
                    {
                        collectStatus = false;
                        likeStatus = false;

                        //取得使用者對這篇文章的愛心及收藏資料
                        var collectLikesInfo = db.CollectLikes.Where(c => c.UserId == userId && c.ArticleId == eacharticleInfo.Id).FirstOrDefault();

                        if (collectLikesInfo != null)//資料存在
                        {
                            if (collectLikesInfo.Collect == true)//該使用者有收藏此文章
                            {
                                collectStatus = true;
                            }
                            if (collectLikesInfo.Like == true)//該使用者有愛心此文章
                            {
                                likeStatus = true;
                            }
                        }

                        var Data = new
                        {
                            Id = eacharticleInfo.Id,
                            Title = eacharticleInfo.Title,
                            Initdate = eacharticleInfo.InitDate,
                            WriterNickName = eacharticleInfo.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(eacharticleInfo.CoverUrl),
                            UserLike = likeStatus,
                            UserCollect = collectStatus,
                        };

                        result.ArticleData.Add(Data);
                    }

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 取得全部文章
        /// </summary>
        /// <param name="readAllArticles">第幾頁、使用者編號</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/readallarticles")]
        public IHttpActionResult ReadAllArticles(ViewModel.ReadAllArticles readAllArticles)
        {
            //取得使用者id
            int userId = readAllArticles.UserId;

            //一頁幾篇文章
            int pageSize = 6;

            //現在第幾頁
            int pageNumber = readAllArticles.Page;

            if (pageNumber <= 0 || pageSize <= 0)//頁數和一頁幾個不得小於等於0
            {
                return BadRequest("一頁幾篇文章或目前頁數請輸入>0的正整數");
            }
            else
            {
                //取得總項目
                int totalLatestItems = db.Articles
                    .Where(a => ((int)a.Progress) == 4 && a.Selected == false).Count();

                //取得總頁數
                int TotalLatestPages = ((totalLatestItems % pageSize) != 0) ? (totalLatestItems / pageSize) + 1 : (totalLatestItems / pageSize);

                //取得總項目
                int totalSelectedItems = db.Articles
                    .Where(a => ((int)a.Progress) == 4 && a.Selected == true).Count();

                //取得總頁數
                int TotalSelectedPages = ((totalSelectedItems % pageSize) != 0) ? (totalSelectedItems / pageSize) + 1 : (totalSelectedItems / pageSize);

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得全部文章成功",
                    TotalLatestPages,
                    TotalSelectedPages,
                    LatestArticleData = new List<object>(),
                    SelectedArticleData = new List<object>()
                };

                //創建收藏及愛心狀態，改下面foreach處理用
                bool collectStatus;
                bool likeStatus;

                //取得全部文章資料(條件：已審核成功、非精選)
                var allArticlesInfo = db.Articles
                    .Where(a => ((int)a.Progress) == 4 && a.Selected == false)
                    .OrderByDescending(a => a.InitDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize).ToList();

                //處理回傳的全部文章資料
                foreach (var eacharticleInfo in allArticlesInfo)
                {
                    collectStatus = false;
                    likeStatus = false;

                    //取得使用者對這篇文章的愛心及收藏資料
                    var collectLikesInfo = db.CollectLikes.Where(c => c.UserId == userId && c.ArticleId == eacharticleInfo.Id).FirstOrDefault();

                    if (collectLikesInfo != null)//資料存在
                    {
                        if (collectLikesInfo.Collect == true)//該使用者有收藏此文章
                        {
                            collectStatus = true;
                        }
                        if (collectLikesInfo.Like == true)//該使用者有愛心此文章
                        {
                            likeStatus = true;
                        }
                    }

                    var Data = new
                    {
                        Id = eacharticleInfo.Id,
                        Title = eacharticleInfo.Title,
                        Initdate = eacharticleInfo.InitDate,
                        WriterNickName = eacharticleInfo.MyUser.NickName,
                        ArticleImgUrl = Utility.ReturnArticleCover(eacharticleInfo.CoverUrl),
                        UserLike = likeStatus,
                        UserCollect = collectStatus,
                        Category = eacharticleInfo.MyArticlesCategory.Name,
                        Summary = eacharticleInfo.Summary,

                    };

                    result.LatestArticleData.Add(Data);
                }

                //取得精選文章資料(條件：已審核成功、精選)
                var selectedArticlesInfo = db.Articles
                    .Where(a => ((int)a.Progress) == 4 && a.Selected == true)
                    .OrderByDescending(a => a.InitDate)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);




                //處理回傳的精選文章資料
                foreach (var eacharticleInfo in selectedArticlesInfo)
                {
                    collectStatus = false;
                    likeStatus = false;

                    //取得使用者對這篇文章的愛心及收藏資料
                    var collectLikesInfo = db.CollectLikes.Where(c => c.UserId == userId && c.ArticleId == eacharticleInfo.Id).FirstOrDefault();

                    if (collectLikesInfo != null)//資料存在
                    {
                        if (collectLikesInfo.Collect == true)//該使用者有收藏此文章
                        {
                            collectStatus = true;
                        }
                        if (collectLikesInfo.Like == true)//該使用者有愛心此文章
                        {
                            likeStatus = true;
                        }
                    }

                    var Data = new
                    {
                        Id = eacharticleInfo.Id,
                        Title = eacharticleInfo.Title,
                        Initdate = eacharticleInfo.InitDate,
                        WriterNickName = eacharticleInfo.MyUser.NickName,
                        ArticleImgUrl = Utility.ReturnArticleCover(eacharticleInfo.CoverUrl),
                        UserLike = likeStatus,
                        UserCollect = collectStatus
                    };

                    result.SelectedArticleData.Add(Data);
                }

                return Ok(result);
            }

        }
    }
}
