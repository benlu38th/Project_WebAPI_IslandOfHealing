using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IslandOfHealing.Security;
using Newtonsoft.Json;
using IslandOfHealing.Models;
using NSwag.Annotations;
using IslandOfHealing.Models;

namespace IslandOfHealing.Controllers
{
    [OpenApiTag("SubscriptionPlan", Description = "訂閱方案")]
    public class NewebPayController : ApiController
    {
        private Context db = new Context();

        /// <summary>
        /// 創建訂單(未付款)
        /// </summary>
        /// <param name="chargeData"></param>
        /// <returns></returns>
        [HttpPost]
        [JwtAuthFilter]
        public IHttpActionResult setchargedata(ViewModel.ChargeRequest chargeData)
        {
            var planInfo = db.PricingPlans.Where(p => p.Id == chargeData.PlanId).FirstOrDefault();

            if (planInfo == null)
            {
                return BadRequest("訂閱方案不存在");
            }
            else
            {
                // Do Something ~ (相關資料檢查處理，成立訂單加入資料庫，並將訂單付款狀態設為未付款)
                // 解密後會回傳 Json 格式的物件 (即加密前的資料)
                var jwtObject = JwtAuthFilter.GetToken(Request.Headers.Authorization.Parameter);
                //取得使用者Id
                int userId = (int)jwtObject["Id"];

                var newOrder = new Order();
                newOrder.ClientName = chargeData.ClientName;
                newOrder.ClientEmail = chargeData.ClientEmail;
                newOrder.ClientPhone = chargeData.ClientPhone;
                newOrder.InitDate = DateTime.Now;
                newOrder.PlanName = planInfo.Name;
                newOrder.PlanPrice = planInfo.Price;
                newOrder.UserId = userId;
                newOrder.Paid = false;
                newOrder.PricingPlanId = chargeData.PlanId;
                newOrder.PaidDate = new DateTime(1900, 01 ,01);
                newOrder.EndDate = new DateTime(1900, 01, 01);
                newOrder.MerchantOrderNo = Guid.NewGuid().ToString();
                string temp = newOrder.MerchantOrderNo;

                //註冊成功塞資料進SQL
                db.Orders.Add(newOrder);
                db.SaveChanges();

                // 整理金流串接資料
                // 加密用金鑰
                string hashKey = "I3bXuIHxxqDObOgUrv41lsUuLtvJprop";
                string hashIV = "CHL8q5JBcq9IaxxP";

                // 金流接收必填資料
                string merchantID = "MS149585240";
                string tradeInfo = "";
                string tradeSha = "";
                string version = "2.0"; // 參考文件串接程式版本

                // tradeInfo 內容，導回的網址都需為 https 
                string respondType = "JSON"; // 回傳格式
                string timeStamp = ((int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
                string merchantOrderNo = timeStamp + "_" + newOrder.Id; // 底線後方為訂單ID，解密比對用，不可重覆(規則參考文件)

                //以下插入
                //merchantOrderNo決定後才能更新資料庫
                var update = db.Orders.Where(o => o.MerchantOrderNo == temp).FirstOrDefault();
                update.MerchantOrderNo = merchantOrderNo;
                //將資料更新進進SQL
                db.SaveChanges();
                //以上插入

                string amt = newOrder.PlanPrice.ToString(); //價格
                string itemDesc = newOrder.PlanName;//商品資訊
                string tradeLimit = "600"; // 交易限制秒數
                string notifyURL = @"https://" + "islandofhealing.rocket-coding.com" + "/api/NewebRecieve/getpaymentdata"; // NotifyURL 填後端接收藍新付款結果的 API 位置，如 : /api/users/getpaymentdata
                string returnURL = "https://island-of-healing.vercel.app/api/paymentcheck";  // 前端可用 Status: SUCCESS 來判斷付款成功，網址夾帶可拿來取得活動內容
                string email = newOrder.ClientEmail; // 通知付款完成用
                string loginType = "0"; // 0不須登入藍新金流會員

                // 將 model 轉換為List<KeyValuePair<string, string>>
                List<KeyValuePair<string, string>> tradeData = new List<KeyValuePair<string, string>>() {
        new KeyValuePair<string, string>("MerchantID", merchantID),
        new KeyValuePair<string, string>("RespondType", respondType),
        new KeyValuePair<string, string>("TimeStamp", timeStamp),
        new KeyValuePair<string, string>("Version", version),
        new KeyValuePair<string, string>("MerchantOrderNo", merchantOrderNo),
        new KeyValuePair<string, string>("Amt", amt),
        new KeyValuePair<string, string>("ItemDesc", itemDesc),
        new KeyValuePair<string, string>("TradeLimit", tradeLimit),
        new KeyValuePair<string, string>("NotifyURL", notifyURL),
        new KeyValuePair<string, string>("ReturnURL", returnURL),
        new KeyValuePair<string, string>("Email", email),
        new KeyValuePair<string, string>("LoginType", loginType)
    };

                // 將 List<KeyValuePair<string, string>> 轉換為 key1=Value1&key2=Value2&key3=Value3...
                var tradeQueryPara = string.Join("&", tradeData.Select(x => $"{x.Key}={x.Value}"));
                // AES 加密
                tradeInfo = CryptoUtil.EncryptAESHex(tradeQueryPara, hashKey, hashIV);
                // SHA256 加密
                tradeSha = CryptoUtil.EncryptSHA256($"HashKey={hashKey}&{tradeInfo}&HashIV={hashIV}");

                // 送出金流串接用資料，給前端送藍新用
                return Ok(new
                {
                    Status = true,
                    PaymentData = new
                    {
                        MerchantID = merchantID,
                        TradeInfo = tradeInfo,
                        TradeSha = tradeSha,
                        Version = version
                    }
                });
            }
        }
    }
}
