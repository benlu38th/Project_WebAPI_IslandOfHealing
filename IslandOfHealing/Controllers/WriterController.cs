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
    [OpenApiTag("Writer", Description = "作家後台")]
    public class WriterController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 取得作家後台數據-流量收益及稿費
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月(全年資料請輸入0)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writer/trafficandroyalty")]
        [JwtAuthFilter]
        public IHttpActionResult GetTrafficAndRoyalty(int year, int month)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者(作家)資料
            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "writer");

            if (userInfo.FirstOrDefault() == null)//使用者不存在，或使用者非作家
            {
                return BadRequest("使用者不存在，或使用者非作家");
            }
            else//使用者存在，且使用者為作家
            {
                if (month != 0) //回傳月資料
                {
                    //使用者稿費資料
                    var royalty = userInfo.Select(u => u.Expenses).ToList();

                    //回傳前端特定年月的使用者稿費資料及稿費總和
                    var royaltyData = royalty[0].Where(r => r.InitDate.Year == year && r.InitDate.Month == month && r.Paid == true).Select(r => new
                    {
                        r.Id,
                        r.Amount,
                        r.InitDate
                    });
                    int totalAmount = 0;
                    foreach (var x in royaltyData)
                    {
                        totalAmount += x.Amount;
                    }

                    //全部流量收益資料
                    //平台月方案收益
                    var monthlyTrafficInfo = db.Orders.Where(o => o.Paid == true && o.PricingPlanId == 1 && o.PaidDate.Year == year && o.PaidDate.Month == month);
                    double monthlyTraffic = 0;
                    if (monthlyTrafficInfo.FirstOrDefault() != null)
                    {
                        monthlyTraffic = monthlyTrafficInfo.Sum(m => (double)m.PlanPrice);
                    }

                    //平台年方案收益
                    var yearlyTrafficInfo = db.Orders.Where(o => o.Paid == true && o.PricingPlanId == 2).ToList();
                    int yearlyTraffic = 0;
                    foreach (var x in yearlyTrafficInfo)
                    {
                        if (
                            ((x.PaidDate.Year == year) && (x.PaidDate.Month == month)) ||
                            ((x.PaidDate.AddMonths(1).Year == year) && (x.PaidDate.AddMonths(1).Month == month)) ||
                            ((x.PaidDate.AddMonths(2).Year == year) && (x.PaidDate.AddMonths(2).Month == month)) ||
                            ((x.PaidDate.AddMonths(3).Year == year) && (x.PaidDate.AddMonths(3).Month == month)) ||
                            ((x.PaidDate.AddMonths(4).Year == year) && (x.PaidDate.AddMonths(4).Month == month)) ||
                            ((x.PaidDate.AddMonths(5).Year == year) && (x.PaidDate.AddMonths(5).Month == month)) ||
                            ((x.PaidDate.AddMonths(6).Year == year) && (x.PaidDate.AddMonths(6).Month == month)) ||
                            ((x.PaidDate.AddMonths(7).Year == year) && (x.PaidDate.AddMonths(7).Month == month)) ||
                            ((x.PaidDate.AddMonths(8).Year == year) && (x.PaidDate.AddMonths(8).Month == month)) ||
                            ((x.PaidDate.AddMonths(9).Year == year) && (x.PaidDate.AddMonths(9).Month == month)) ||
                            ((x.PaidDate.AddMonths(10).Year == year) && (x.PaidDate.AddMonths(10).Month == month)) ||
                            ((x.PaidDate.AddMonths(11).Year == year) && (x.PaidDate.AddMonths(11).Month == month))
                            )
                        {
                            yearlyTraffic += 100;
                        }
                    }

                    //總收益=平台月方案收益+平台年方案收益
                    double totalProfit = monthlyTraffic + yearlyTraffic;

                    //作家收益=總收益*0.4
                    double writersProfit = totalProfit * 0.4;

                    //分潤制度=作家收益*0.8(0.2作為作家簽約預算)
                    double writersTrafficProfit = writersProfit * 0.8;

                    //作家點擊次數
                    double writerClicks = userInfo.FirstOrDefault().Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.PaidArticleClicksInfo).Where(pc => pc.InitDate.Year == year && pc.InitDate.Month == month).Count();

                    //全部作家點擊次數
                    double totalClicks = db.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.PaidArticleClicksInfo).Where(pc => pc.InitDate.Year == year && pc.InitDate.Month == month).Count();

                    //全部作家點擊次數(test)
                    //double totalClicksTest = db.Articles.Where(a => a.Progress == Progress.審核成功).Sum(a => a.PaidArticleClicksInfo.Count(pc => pc.InitDate.Year == year && pc.InitDate.Month == month));

                    //作家流量收入
                    double writerTraffic = 0;
                    if (totalClicks != 0)
                    {
                        writerTraffic = writersTrafficProfit * writerClicks / totalClicks;
                    }


                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得作家流量收益及稿費",
                        WriterId = id,
                        WriterNickName = userInfo.FirstOrDefault().NickName,
                        Year = year,
                        Month = month,
                        Royalty = totalAmount,
                        Traffic = writerTraffic,
                        RoyaltyData = royaltyData,
                        TrafficData = new
                        {
                            MonthlyTraffic = monthlyTraffic,
                            YearlyTraffic = yearlyTraffic,
                            TotalProfit = totalProfit,
                            WritersProfit = writersProfit,
                            WritersTrafficProfit = writersTrafficProfit,
                            WriterClicks = writerClicks,
                            TotalClicks = totalClicks,
                            //TotalClicksTest = totalClicksTest,
                            WriterTraffic = writerTraffic
                        }
                    };
                    return Ok(result);
                }
                else //回傳年資料
                {
                    double yearRoyalty = 0;
                    double yearTraffic = 0;
                    var data = new List<object>();

                    for (int eachMonth = 1; eachMonth <= 12; eachMonth++)
                    {
                        //使用者稿費資料
                        var royalty = userInfo.Select(u => u.Expenses).ToList();

                        //回傳前端特定年月的使用者稿費資料及稿費總和
                        var royaltyData = royalty[0].Where(r => r.InitDate.Year == year && r.InitDate.Month == eachMonth && r.Paid == true).Select(r => new
                        {
                            r.Id,
                            r.Amount,
                            r.InitDate
                        });
                        int totalAmount = 0;
                        foreach (var x in royaltyData)
                        {
                            totalAmount += x.Amount;
                        }

                        //全部流量收益資料
                        //平台月方案收益
                        var monthlyTrafficInfo = db.Orders.Where(o => o.Paid == true && o.PricingPlanId == 1 && o.PaidDate.Year == year && o.PaidDate.Month == eachMonth);
                        double monthlyTraffic = 0;
                        if (monthlyTrafficInfo.FirstOrDefault() != null)
                        {
                            monthlyTraffic = monthlyTrafficInfo.Sum(m => (double)m.PlanPrice);
                        }

                        //平台年方案收益
                        var yearlyTrafficInfo = db.Orders.Where(o => o.Paid == true && o.PricingPlanId == 2).ToList();
                        int yearlyTraffic = 0;
                        foreach (var x in yearlyTrafficInfo)
                        {
                            if (
                                ((x.PaidDate.Year == year) && (x.PaidDate.Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(1).Year == year) && (x.PaidDate.AddMonths(1).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(2).Year == year) && (x.PaidDate.AddMonths(2).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(3).Year == year) && (x.PaidDate.AddMonths(3).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(4).Year == year) && (x.PaidDate.AddMonths(4).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(5).Year == year) && (x.PaidDate.AddMonths(5).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(6).Year == year) && (x.PaidDate.AddMonths(6).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(7).Year == year) && (x.PaidDate.AddMonths(7).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(8).Year == year) && (x.PaidDate.AddMonths(8).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(9).Year == year) && (x.PaidDate.AddMonths(9).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(10).Year == year) && (x.PaidDate.AddMonths(10).Month == eachMonth)) ||
                                ((x.PaidDate.AddMonths(11).Year == year) && (x.PaidDate.AddMonths(11).Month == eachMonth))
                                )
                            {
                                yearlyTraffic += 100;
                            }
                        }

                        //總收益=平台月方案收益+平台年方案收益
                        double totalProfit = monthlyTraffic + yearlyTraffic;

                        //作家收益=總收益*0.4
                        double writersProfit = totalProfit * 0.4;

                        //分潤制度=作家收益*0.8(0.2作為作家簽約預算)
                        double writersTrafficProfit = writersProfit * 0.8;

                        //作家點擊次數
                        double writerClicks = userInfo.FirstOrDefault().Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.PaidArticleClicksInfo).Where(pc => pc.InitDate.Year == year && pc.InitDate.Month == eachMonth).Count();

                        //全部作家點擊次數
                        double totalClicks = db.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.PaidArticleClicksInfo).Where(pc => pc.InitDate.Year == year && pc.InitDate.Month == eachMonth).Count();

                        //作家流量收入
                        double writerTraffic = 0;
                        if (totalClicks != 0)
                        {
                            writerTraffic = writersTrafficProfit * writerClicks / totalClicks;
                        }

                        var eachResult = new
                        {
                            WriterId = id,
                            WriterNickName = userInfo.FirstOrDefault().NickName,
                            Year = year,
                            Month = eachMonth,
                            Royalty = totalAmount,
                            Traffic = writerTraffic,
                            RoyaltyData = royaltyData,
                            TrafficData = new
                            {
                                MonthlyTraffic = monthlyTraffic,
                                YearlyTraffic = yearlyTraffic,
                                TotalProfit = totalProfit,
                                WritersProfit = writersProfit,
                                WritersTrafficProfit = writersTrafficProfit,
                                WriterClicks = writerClicks,
                                TotalClicks = totalClicks,
                                WriterTraffic = writerTraffic
                            }
                        };

                        data.Add(eachResult);

                        if (totalAmount != null)
                        {
                            yearRoyalty += totalAmount;
                        }

                        if (writerTraffic != null)
                        {
                            yearTraffic += writerTraffic;
                        }
                    }
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得作家流量收益及稿費",
                        YearRoyalty = yearRoyalty,
                        YearTraffic = yearTraffic,
                        Data = data
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 取得作家後台數據-追蹤人數
        /// </summary>
        /// <param name="year">年</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writer/numberoffans/{year}")]
        [JwtAuthFilter]
        public IHttpActionResult GetNumberOfFans(int year)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者(作家)資料
            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "writer").FirstOrDefault();

            if (userInfo == null)//使用者不存在，或使用者非作家
            {
                return BadRequest("使用者不存在，或使用者非作家");
            }
            else//使用者存在，且使用者為作家
            {
                var fansInfo = userInfo.FollowWriters.Where(f => f.InitDate.Year == year).ToList();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得作家追蹤人數成功",
                    WriterId = userInfo.Id,
                    WriterNickName = userInfo.NickName,
                    Year = year,
                    NumberOfFansData = new
                    {
                        Jan = fansInfo.Where(f => f.InitDate.Month == 1).Count(),
                        Feb = fansInfo.Where(f => f.InitDate.Month == 2).Count(),
                        Mar = fansInfo.Where(f => f.InitDate.Month == 3).Count(),
                        Apr = fansInfo.Where(f => f.InitDate.Month == 4).Count(),
                        May = fansInfo.Where(f => f.InitDate.Month == 5).Count(),
                        Jun = fansInfo.Where(f => f.InitDate.Month == 6).Count(),
                        Jul = fansInfo.Where(f => f.InitDate.Month == 7).Count(),
                        Aug = fansInfo.Where(f => f.InitDate.Month == 8).Count(),
                        Sep = fansInfo.Where(f => f.InitDate.Month == 9).Count(),
                        Oct = fansInfo.Where(f => f.InitDate.Month == 10).Count(),
                        Nov = fansInfo.Where(f => f.InitDate.Month == 11).Count(),
                        Dec = fansInfo.Where(f => f.InitDate.Month == 12).Count(),
                        Total = fansInfo.Count()
                    }
                };
                return Ok(result);

            }
        }

        /// <summary>
        /// 取得作家後台數據-貼文分析
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writer/articleanalysis/{year}/{month}")]
        [JwtAuthFilter]
        public IHttpActionResult GetWriterArticleAnalysis(int year, int month)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者(作家)資料
            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "writer").FirstOrDefault();

            if (userInfo == null)//使用者不存在，或使用者非作家
            {
                return BadRequest("使用者不存在，或使用者非作家");
            }
            else//使用者存在，且使用者為作家
            {
                //找出符合條件的文章(是作家的文章且該文章是審核成功的且是指定年分及月分的)
                var articleInfo = db.Articles.Where(a => a.UserId == id && a.Progress == Progress.審核成功 && a.InitDate.Year == year && a.InitDate.Month == month).ToList();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得作家貼文分析數據成功",
                    ArticleAnalysisData = articleInfo.Select(a => new
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Initdate = a.InitDate,
                        Likes = a.CollectLikes.Where(c => c.Like == true).Count(),
                        Clicks = a.PaidArticleClicksInfo.Count(),
                        Comments = a.ArticleComments.Count()
                    })
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// 取得作家後台數據-總覽
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writer/overview/{year}/{month}")]
        [JwtAuthFilter]
        public IHttpActionResult GetWriterOverview(int year, int month)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得使用者(作家)資料
            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "writer").FirstOrDefault();

            if (userInfo == null)//使用者不存在，或使用者非作家
            {
                return BadRequest("使用者不存在，或使用者非作家");
            }
            else//使用者存在，且使用者為作家
            {
                //文章數量
                int TotalArticles = userInfo.Articles.Where(a => a.Progress == Progress.審核成功 && a.InitDate.Year == year && a.InitDate.Month == month).Count();

                //收藏數量
                int TotalCollects = userInfo.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.CollectLikes).Where(c => c.Collect == true && c.CollectDate.Year == year && c.CollectDate.Month == month).Count();

                //愛心數量
                double TotalLikes = userInfo.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.CollectLikes).Where(c => c.Like == true && c.CollectDate.Year == year && c.CollectDate.Month == month).Count();

                //留言次數
                double TotalComments = userInfo.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.ArticleComments).Where(ac => ac.InitDate.Year == year && ac.InitDate.Month == month).Count();

                //點擊次數
                int TotalClicks = userInfo.Articles.Where(a => a.Progress == Progress.審核成功).SelectMany(a => a.PaidArticleClicksInfo).Where(pc => pc.InitDate.Year == year && pc.InitDate.Month == month).Count();

                //被追蹤數
                double TotalFollow = userInfo.FollowWriters.Where(f => f.InitDate.Year == year && f.InitDate.Month == month).Count();

                //互動數
                double TotalInteractions;
                if (TotalFollow == 0)
                {
                    TotalInteractions = 0;
                }
                else
                {
                    TotalInteractions = (TotalLikes + TotalComments) / TotalFollow;
                }

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得作家總覽數據成功",
                    OverviewData = new
                    {
                        TotalArticles,
                        TotalCollects,
                        TotalLikes,
                        TotalComments,
                        TotalClicks,
                        TotalInteractions
                    }
                };
                return Ok(result);
            }
        }

        /// <summary>
        /// 取得作者文章列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/writer/articles")]
        [JwtAuthFilter]
        public IHttpActionResult GetWriterArticle()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者Id
            int id = (int)jwtObject["Id"];

            //取得作家文章列表
            var userArticles = db.Articles.Where(a => a.UserId == id).FirstOrDefault();

            // 取得資料表中的所有文章資料
            var articles = db.Articles.Where(a => a.UserId == id).ToList();

            //回傳資料
            var result = new
            {
                StatusCode = (int)HttpStatusCode.OK,
                Status = "success",
                Message = "取得作家文章資料成功",
                Data = new List<object>()
            };

            //處理回傳的Data資料
            foreach (var userArticle in articles)
            {
                var articleCommentsCount = db.ArticleComments.Count(a => a.ArticleId == userArticle.Id);
                var articleCollectNum = db.CollectLikes.Count(c => c.ArticleId == userArticle.Id && c.Collect == true);

                var Data = new
                {
                    Id = userArticle.Id,
                    Title = userArticle.Title,
                    CollectNum = articleCollectNum,
                    Initdate = userArticle.InitDate,
                    CommentNum = articleCommentsCount,
                    Progress = userArticle.Progress.ToString(),
                    Pay = userArticle.Pay,
                    Category = userArticle.MyArticlesCategory.Name
                };

                result.Data.Add(Data);
            }

            return Ok(result);

        }
    }
}
