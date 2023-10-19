using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Models;
using IslandOfHealing.Security;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("Users", Description = "會員設定")]
    public class UserOrderController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 瀏覽個人訂閱細節
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/userorderdetail/get")]
        [JwtAuthFilter]
        public IHttpActionResult UserOrderDetailGet()
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
                //取得最新的訂閱(已付款、相同使用者、PaidDate日期最新)
                var userOrderDetailInfo = db.Orders
    .Where(o => o.UserId == id && o.Paid == true)
    .OrderByDescending(o => o.PaidDate)
    .FirstOrDefault();

                if(userOrderDetailInfo == null)//訂單不存在
                {
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得個人訂閱細節成功",
                        Plan = "free"
                    };
                    return Ok(result);
                }
                else if(userOrderDetailInfo.PricingPlanId == 1)
                {
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得個人訂閱細節成功",
                        Plan = "monthly",
                        userOrderDetailInfo.PlanName,
                        userOrderDetailInfo.EndDate,
                        userInfo.RenewMembership
                    };
                    return Ok(result);
                }
                else if(userOrderDetailInfo.PricingPlanId == 2)
                {
                    var result = new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Status = "success",
                        Message = "取得個人訂閱細節成功",
                        Plan = "yearly",
                        userOrderDetailInfo.PlanName,
                        userOrderDetailInfo.EndDate,
                        userInfo.RenewMembership
                    };
                    return Ok(result);
                }
                else
                {
                    return BadRequest("訂閱方案不存在，請檢查資料庫");
                }
                
            }
        }

        /// <summary>
        /// 取得使用者已付款歷史訂單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/userorders/get")]
        [JwtAuthFilter]
        public IHttpActionResult UserOrdersGet()
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
                var userOrdersInfo = db.Orders.Where(o => o.UserId == id && o.Paid == true).ToList();

                var result = new
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Status = "success",
                    Message = "取得使用者已付款訂單列表成功",
                    OrdersData = userOrdersInfo.Select(u => new
                    {
                        u.PaidDate,
                        u.EndDate,
                        u.MerchantOrderNo,
                        u.PlanName,
                        u.PlanPrice,
                        PaymentMethod = "信用卡"
                    })
                };

                return Ok(result);
            }
        }
    }
}
