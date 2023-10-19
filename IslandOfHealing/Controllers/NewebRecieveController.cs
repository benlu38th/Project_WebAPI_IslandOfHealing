using IslandOfHealing.Security;
using Newtonsoft.Json;
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
    [OpenApiTag("SubscriptionPlan", Description = "訂閱方案")]
    public class NewebRecieveController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 接收藍新回傳之資料
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage getpaymentdata(NewebPayReturn data)
        {
            // 付款失敗跳離執行
            var response = Request.CreateResponse(HttpStatusCode.OK);
            if (!data.Status.Equals("SUCCESS")) return response;

            // 加密用金鑰
            string hashKey = "I3bXuIHxxqDObOgUrv41lsUuLtvJprop";
            string hashIV = "CHL8q5JBcq9IaxxP";
            // AES 解密
            string decryptTradeInfo = CryptoUtil.DecryptAESHex(data.TradeInfo, hashKey, hashIV);
            PaymentResult result = JsonConvert.DeserializeObject<PaymentResult>(decryptTradeInfo);
            // 取出交易記錄資料庫的訂單ID
            string[] orderNo = result.Result.MerchantOrderNo.Split('_');
            int logId = Convert.ToInt32(orderNo[1]);

            // 用取得的"訂單ID"修改資料庫此筆訂單的付款狀態為 true
            var orderInfo = db.Orders.Where(o => o.Id == logId).FirstOrDefault();

            if(orderInfo != null)
            {
                orderInfo.Paid = true;
                orderInfo.PaidDate = DateTime.Now;
                if(orderInfo.MyPricingPlan.BillingCycle == "monthly")
                {
                    orderInfo.EndDate = DateTime.Now.AddMonths(1);
                }
                else
                {
                    orderInfo.EndDate = DateTime.Now.AddYears(1);
                }
            }

            //取得該訂單的UserId，將pay從false變更為true
            var userInfo = db.Users.Where(u => u.Id == orderInfo.UserId).FirstOrDefault();

            userInfo.MyPlan = orderInfo.MyPricingPlan.BillingCycle;
            userInfo.RenewMembership = true;

            //儲存變更到資料庫
            db.SaveChanges();

            // 用取得的"訂單ID"寄出付款完成訂單成立，商品準備出貨通知信
            Utility.SendSubcriptionSucessEmail(orderInfo.ClientEmail, orderInfo.ClientName);

            return response;
        }
    }
}
