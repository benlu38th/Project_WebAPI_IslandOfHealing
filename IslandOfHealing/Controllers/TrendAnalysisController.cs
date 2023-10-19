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
    [OpenApiTag("AdministratorSystem", Description = "後台系統")]
    public class TrendAnalysisController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 取得熱門關鍵字(2、3、4個中文字)排名前20名(文章標題、文章、留言、AI問題)
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月(取得一整年資料輸入0)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/trendanalysis/get")]
        [JwtAuthFilter]
        public IHttpActionResult TrendAnalysisGet(int year, int month)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            int id = (int)jwtObject["Id"];

            var userInfo = db.Users.Where(u => u.Id == id && u.Role == "administrator").FirstOrDefault();

            if (userInfo == null)
            {
                return BadRequest("平台方才可以取得趨勢分析");
            }
            else
            {
                if (month != 0)//取得月資料
                {
                    var articlesInfo = db.Articles
                        .Where(a => a.InitDate.Year == year && a.InitDate.Month == month)
                        .Select(a => a.Content);

                    var commentsInfo = db.ArticleComments
                        .Where(a => a.InitDate.Year == year && a.InitDate.Month == month)
                        .Select(a => a.Comment);

                    var questionInfo = db.AIMessages
                        .Where(a => a.InitDate.Year == year && a.InitDate.Month == month)
                        .Select(a => a.UserQuestion);

                    var artitleTitle = db.Articles
                        .Where(a => a.InitDate.Year == year && a.InitDate.Month == month)
                        .Select(a => a.Title);

                    var articleList = articlesInfo
                        .Concat(commentsInfo)
                        .Concat(questionInfo)
                        .ToList();

                    string article = string.Join(",", articleList);


                    //移除非中文字元和標點符號，只保留中文字和空格，將非空格者轉成文章陣列
                    string[] articleString = Utility.GetWordsFromArticle(article);

                    //創建回傳結果List
                    List<string> result = new List<string>();

                    ////創建1個字陣列
                    //List<string> temptResult1 = GetWordsList(articleString, 1);

                    ////將1個字陣列塞入回傳結果
                    //foreach (var x in temptResult1)
                    //{
                    //    result.Add(x);
                    //}

                    //創建2個字陣列
                    List<string> temptResult2 = Utility.GetWordsList(articleString, 2);

                    //將2個字陣列塞入回傳結果
                    foreach (var x in temptResult2)
                    {
                        result.Add(x);
                    }

                    //創建3個字陣列
                    List<string> temptResult3 = Utility.GetWordsList(articleString, 3);

                    //將3個字陣列塞入回傳結果
                    foreach (var x in temptResult3)
                    {
                        result.Add(x);
                    }

                    //創建4個字陣列
                    List<string> temptResult4 = Utility.GetWordsList(articleString, 4);

                    //將4個字陣列塞入回傳結果
                    foreach (var x in temptResult4)
                    {
                        result.Add(x);
                    }

                    //統計一個List字串中的出現次數
                    Dictionary<string, int> returnResult = Utility.CountWordFrequency(result);

                    //排除的詞彙
                    string[] excludedKeys = new string[] { "特斯", "斯拉", "馬斯", "斯克", "柯文", "文哲", "的文", "的文本", "文本分", "本分析", "機器學", "器學習", "器學", "伊麗莎", "麗莎白", "莎白二", "白二世", "伊麗", "麗莎", "莎白", "白二", "車產業", "眾黨", "麗莎白二", "莎白二世", "己的", "們的", "們可", "我們可" , "們能夠", "我們能" , "們能" };

                    // 將字典按值進行降序排序，並排除excludedKeys，取出前20名，返回鍵值對列表
                    List<KeyValuePair<string, int>> sortedReturnResult = returnResult
                        .OrderByDescending(x => x.Value)
                        .ThenByDescending(x => x.Key.Count())
                        .Where(x => !excludedKeys.Contains(x.Key))
                        .Take(20)
                        .ToList();

                    var outpurResult = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得熱門關鍵字排名前20名(月)成功",
                        sortedReturnResult
                    };

                    return Ok(outpurResult);
                }
                else//取得年資料
                {
                    var articlesInfo = db.Articles
                        .Where(a => a.InitDate.Year == year)
                        .Select(a => a.Content);

                    var commentsInfo = db.ArticleComments
                        .Where(a => a.InitDate.Year == year)
                        .Select(a => a.Comment);

                    var questionInfo = db.AIMessages
                        .Where(a => a.InitDate.Year == year)
                        .Select(a => a.UserQuestion);

                    var artitleTitle = db.Articles
                        .Where(a => a.InitDate.Year == year)
                        .Select(a => a.Title);

                    var articleList = articlesInfo
                        .Concat(commentsInfo)
                        .Concat(questionInfo)
                        .ToList();

                    string article = string.Join(",", articleList);


                    //移除非中文字元和標點符號，只保留中文字和空格，將非空格者轉成文章陣列
                    string[] articleString = Utility.GetWordsFromArticle(article);

                    //創建回傳結果List
                    List<string> result = new List<string>();

                    ////創建1個字陣列
                    //List<string> temptResult1 = GetWordsList(articleString, 1);

                    ////將1個字陣列塞入回傳結果
                    //foreach (var x in temptResult1)
                    //{
                    //    result.Add(x);
                    //}

                    //創建2個字陣列
                    List<string> temptResult2 = Utility.GetWordsList(articleString, 2);

                    //將2個字陣列塞入回傳結果
                    foreach (var x in temptResult2)
                    {
                        result.Add(x);
                    }

                    //創建3個字陣列
                    List<string> temptResult3 = Utility.GetWordsList(articleString, 3);

                    //將3個字陣列塞入回傳結果
                    foreach (var x in temptResult3)
                    {
                        result.Add(x);
                    }

                    //創建4個字陣列
                    List<string> temptResult4 = Utility.GetWordsList(articleString, 4);

                    //將4個字陣列塞入回傳結果
                    foreach (var x in temptResult4)
                    {
                        result.Add(x);
                    }

                    //統計一個List字串中的出現次數
                    Dictionary<string, int> returnResult = Utility.CountWordFrequency(result);

                    //排除的詞彙
                    string[] excludedKeys = new string[] { "特斯", "斯拉", "馬斯", "斯克", "柯文", "文哲", "的文", "的文本", "文本分", "本分析", "機器學", "器學習", "器學", "伊麗莎", "麗莎白", "莎白二", "白二世", "伊麗", "麗莎", "莎白", "白二", "車產業", "眾黨", "麗莎白二", "莎白二世" };

                    // 將字典按值進行降序排序，並排除excludedKeys，取出前20名，返回鍵值對列表
                    List<KeyValuePair<string, int>> sortedReturnResult = returnResult
                        .OrderByDescending(x => x.Value)
                        .ThenByDescending(x => x.Key.Count())
                        .Where(x => !excludedKeys.Contains(x.Key))
                        .Take(20)
                        .ToList();

                    var outpurResult = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得熱門關鍵字排名前20名(年)成功",
                        sortedReturnResult
                    };

                    return Ok(outpurResult);
                }
            }
        }
    }
}
