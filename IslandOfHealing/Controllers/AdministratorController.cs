using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Security;
using IslandOfHealing.Models;
using NSwag.Annotations;
using IslandOfHealing.Models.Function;

namespace IslandOfHealing.Controllers
{

    [OpenApiTag("AdministratorSystem", Description = "後台系統")]
    public class AdministratorController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 移除作家身分
        /// </summary>
        /// <param name="writerid">欲移除作家身分的使用者id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/cancelwriter/{writerid}")]
        [JwtAuthFilter]
        public IHttpActionResult CancelWriter(int writerid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得移除使用者作家身分");
            }
            else//使用者是"administrator"
            {
                //判斷欲移除作家身分的使用者是否為 "writer"身分
                var writerInfo = db.Users.Where(u => u.Id == writerid && u.Role == "writer").FirstOrDefault();

                if (writerInfo == null)
                {
                    return BadRequest("writerid不存在或非作家身分");
                }
                else
                {
                    writerInfo.Role = "user";
                    writerInfo.WriterProgress = WriterProgress.未申請;
                    db.SaveChanges();

                    //返回前端訊息
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "移除作家身分成功，該使用者已不具作家身分"
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 創建費用單
        /// </summary>
        /// <param name="userid">被委託人(作家)id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/expense/create")]
        [JwtAuthFilter]
        public IHttpActionResult ExpenseCreate(int userid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            //判斷被委託人是否存在
            var writerExist = db.Users.Any(u => u.Id == userid && u.Role == "writer");

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得創建費用單");
            }
            else//使用者是"administrator"
            {
                if (!writerExist)//被委託人不存在或被委託人不是作家
                {
                    return BadRequest("被委託人不存在或被委託人不是作家");
                }
                else
                {
                    //創建費用資料
                    var newExpenseInfo = new Expense();

                    newExpenseInfo.PayDate = DateTime.Parse("1900/01/01");
                    newExpenseInfo.UserId = userid;
                    newExpenseInfo.ContractId = "請填寫合約編號";
                    newExpenseInfo.InitDate = DateTime.Now;
                    newExpenseInfo.Paid = false;
                    newExpenseInfo.Amount = 0;

                    //新增至SQL
                    db.Expenses.Add(newExpenseInfo);
                    db.SaveChanges();

                    //返回前端訊息
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "新增費用單成功"
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 取得費用資料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/expense/get")]
        [JwtAuthFilter]
        public IHttpActionResult ExpenseGet()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得取得費用紀錄");
            }
            else//使用者是"administrator"
            {
                //取得費用資料
                var expenseInfo = db.Expenses.ToList();

                //返回前端訊息
                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得費用紀錄成功",
                    ExpenseData = expenseInfo.Select(e => new
                    {
                        e.Id,
                        e.ContractId,
                        WriterId = e.UserId,
                        e.Amount,
                        e.PayDate,
                        e.Paid,
                        e.InitDate,
                        Edit = false
                    })
                };

                return Ok(result);
            }
        }

        /// <summary>
        /// 更新費用單
        /// </summary>
        /// <param name="inputExpense">費用單編號、合約編號、付款時間、金額、付款情形</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/expense/update")]
        [JwtAuthFilter]
        public IHttpActionResult ExpenseUpdate(ViewModel.InputExpense inputExpense)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            //判斷費用單是否存在
            var expenseInfo = db.Expenses.Where(e => e.Id == inputExpense.Id).FirstOrDefault();

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得更新費用紀錄");
            }
            else//使用者是"administrator"
            {
                if (expenseInfo == null)//費用單不存在
                {
                    return BadRequest("費用單不存在");
                }
                else//費用單存在
                {
                    //更新費用單資料
                    expenseInfo.ContractId = inputExpense.ContractId;
                    expenseInfo.PayDate = inputExpense.PayDate;
                    expenseInfo.Amount = inputExpense.Amount;
                    expenseInfo.Paid = inputExpense.Paid;

                    //儲存至SQL
                    db.SaveChanges();

                    //返回前端訊息
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "更新費用單成功",
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 給予文章精選
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/selectedarticle/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult SelectedArticle(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            //取得article資料，article不存在返回null
            var articleInfo = db.Articles.Where(a => a.Id == articleid).FirstOrDefault();

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得給予文章精選");
            }
            else//使用者是"administrator"
            {
                if (articleInfo == null)//article不存在
                {
                    return BadRequest("文章不存在");
                }
                else
                {
                    //更新資料庫
                    articleInfo.Selected = true;
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "給予文章精選成功"
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 取消文章精選
        /// </summary>
        /// <param name="articleid">文章id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/cancelselectedarticle/{articleid}")]
        [JwtAuthFilter]
        public IHttpActionResult CancelSelectedArticle(int articleid)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            //取得article資料，article不存在返回null
            var articleInfo = db.Articles.Where(a => a.Id == articleid).FirstOrDefault();

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得取消文章精選");
            }
            else//使用者是"administrator"
            {
                if (articleInfo == null)//article不存在
                {
                    return BadRequest("文章不存在");
                }
                else
                {
                    //更新資料庫
                    articleInfo.Selected = false;
                    db.SaveChanges();

                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取消文章精選成功"
                    };

                    return Ok(result);
                }
            }
        }

        /// <summary>
        /// 審核使用者申請作家結果
        /// </summary>
        /// <param name="userid">使用者id</param>
        /// <param name="result">2=審核失敗；3=審核成功</param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/applyforwriterresult/{userid}/{result}")]
        [JwtAuthFilter]
        public IHttpActionResult ApplyForWriterResult(int userid, int result)
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            //取得user資料，user不存在返回null
            var userInfo = db.Users.Where(a => a.Id == userid).FirstOrDefault();

            if (!userExist)//使用者不是"administrator"
            {
                return BadRequest("非平台方不得給予使用者作家身分");
            }
            else//使用者是"administrator"
            {
                if (userInfo == null)//申請人不存在
                {
                    return BadRequest("申請人不存在");
                }
                else//申請人存在
                {
                    if (userInfo.WriterProgress != WriterProgress.已申請)
                    {
                        return BadRequest("該使用者沒有申請成為作家");
                    }
                    else if (result == 2)
                    {
                        userInfo.WriterProgress = Models.WriterProgress.申請失敗;

                        db.SaveChanges();

                        //返回結果
                        var toResult = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "申請人未通過作家審核，審核失敗"
                        };

                        return Ok(toResult);
                    }
                    else if (result == 3)
                    {
                        userInfo.WriterProgress = Models.WriterProgress.申請成功;

                        userInfo.Role = "writer";
                        userInfo.JobTitle = "新人作家";
                        userInfo.Bio = "剛剛登島，請多指教！";

                        db.SaveChanges();

                        //返回結果
                        var toResult = new
                        {
                            StatusCode = (int)HttpStatusCode.OK,
                            Status = "success",
                            Message = "已給予作家身分，審核成功"
                        };

                        return Ok(toResult);
                    }
                    else
                    {
                        return BadRequest("result請填寫2或3");
                    }
                }
            }
        }

        /// <summary>
        /// 取得待審核、審核中、審核失敗、審核成功文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/administratorarticles/get")]
        [JwtAuthFilter]
        public IHttpActionResult GetAdministratorArticles()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)
            {
                return BadRequest("非平台方不得瀏覽待審核文章");
            }
            else
            {
                var articlesInfo = db.Articles.Where(a => a.Progress == Progress.待審核 || a.Progress == Progress.審核中 || a.Progress == Progress.審核失敗 || a.Progress == Progress.審核成功);

                if (articlesInfo.FirstOrDefault() == null)
                {
                    return BadRequest("找不到任何文章");
                }
                else
                {
                    //返回結果
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得待審核、審核中、審核失敗、審核成功文章成功",
                        ArticlesData = new List<object>()
                    };

                    //處理回傳的全部文章資料
                    foreach (var eacharticleInfo in articlesInfo.ToList())
                    {
                        var Data = new
                        {
                            Id = eacharticleInfo.Id,
                            Title = eacharticleInfo.Title,
                            Initdate = eacharticleInfo.InitDate,
                            WriterNickName = eacharticleInfo.MyUser.NickName,
                            ArticleImgUrl = Utility.ReturnArticleCover(eacharticleInfo.CoverUrl),
                            Progress = eacharticleInfo.Progress.ToString(),
                            Selected = eacharticleInfo.Selected
                        };

                        result.ArticlesData.Add(Data);
                    }

                    return Ok(result);

                }
            }
        }

        /// <summary>
        /// 取得全部作家及已申請作家之使用者
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/administratorwriters/get")]
        [JwtAuthFilter]
        public IHttpActionResult GetAdministratorWriters()
        {
            // 解密後會回傳 Json 格式的物件 (即加密前的資料)
            var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);

            //取得使用者id
            int id = (int)jwtObject["Id"];

            //判斷該使用者是否為 "administrator"
            var userExist = db.Users.Any(u => u.Id == id && u.Role == "administrator");

            if (!userExist)
            {
                return BadRequest("非平台方不得瀏覽作家及已申請作家之使用者");
            }
            else
            {
                var UserInfo = db.Users.Where(u => u.WriterProgress == WriterProgress.已申請 || u.WriterProgress == WriterProgress.申請失敗 || u.WriterProgress == WriterProgress.申請成功);

                if (UserInfo.FirstOrDefault() == null)
                {
                    return BadRequest("找不到任何作家及已申請作家之使用者");
                }
                else
                {
                    //返回結果
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得全部作家及已申請作家之使用者成功",
                        UserData = new List<object>()
                    };

                    //處理回傳的全部文章資料
                    foreach (var eachUserInfo in UserInfo)
                    {
                        var Data = new
                        {
                            Id = eachUserInfo.Id,
                            Account = eachUserInfo.Account,
                            Nickname = eachUserInfo.NickName,
                            WriterProgress = eachUserInfo.WriterProgress.ToString(),
                            ImgUrl = "https://islandofhealing.rocket-coding.com/upload/userimgurl/" + eachUserInfo.ImgUrl,
                        };

                        result.UserData.Add(Data);
                    }

                    return Ok(result);

                }
            }
        }
    }
}

