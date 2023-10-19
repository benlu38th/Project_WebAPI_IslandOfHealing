using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace IslandOfHealing.Models.Function
{
    public class Utility
    {
        private Context db = new Context();

        public static string ReturnArticleCover(string inputArticleImgUrl)
        {
            if (inputArticleImgUrl == "" || inputArticleImgUrl==null)
            {
                return "";
            }
            else
            {
                return "https://islandofhealing.rocket-coding.com/upload/articlecover/" + inputArticleImgUrl;
            }

        }

        public static string ReturnConversationCover(string inputConversationImgUrl)
        {
            if (inputConversationImgUrl == "" || inputConversationImgUrl == null)
            {
                return "";
            }
            else
            {
                return "https://islandofhealing.rocket-coding.com/upload/conversationcover/" + inputConversationImgUrl;
            }
        }

        /// <summary>
        /// 建立一個指定長度的隨機salt值
        /// </summary>
        /// <param name="saltLenght">鹽的長度</param>
        /// <returns></returns>
        public static string CreateSalt(int saltLenght)
        {
            //生成一個加密的隨機數
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[saltLenght];
            rng.GetBytes(buff);

            //返回一個Base64隨機數的字串
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// 密碼加salt後返回加密後的字串(密碼 , salt)
        /// </summary>
        /// <param name="pwd">原始密碼</param>
        /// <param name="strSalt">鹽</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string pwd, string strSalt)
        {
            //把密碼和Salt連起來
            string saltAndPwd = String.Concat(pwd, strSalt);
            //對密碼進行雜湊
            string hashenPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA256");
            //轉為小寫字元並擷取前16個字串
            hashenPwd = hashenPwd.ToLower().Substring(0, 16);
            //返回雜湊後的值
            return hashenPwd;
        }

        /// <summary>
        /// 產生重設密碼連結
        /// </summary>
        /// <param name="email">電子信箱</param>
        /// <returns></returns>
        public static string GenerateResetPasswordLink(string email, string guid)
        {
            // 根据邮箱地址生成一个包含重置密码令牌的链接
            string resetLink = "http://localhost:3000/login/" + "?email=" + email + "&guid=" + guid;

            return resetLink;
        }

        /// <summary>
        /// 忘記密碼寄通知信
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="resetLink"></param>
        public static void SendResetPasswordEmail(string toEmail, string resetLink)
        {
            //新密碼
            string newPwd = "A" + resetLink.Substring(resetLink.Length - 12);

            try
            {
                // 取得收件者信箱
                string toMail = toEmail;

                // 設定寄件者信箱和應用程式密碼
                string fromMail = "xxx@gmail.com";
                string fromPassword = "password";

                // 建立郵件訊息物件
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "IslandOfHealing 忘記密碼";
                message.To.Add(new MailAddress(toMail));
                message.Body = $"{toEmail}用戶您好，您的新密碼是{newPwd}<br/>欲激活新密碼，請在十分鐘內點擊下方連結，並盡快登入重設密碼！<br/><a href='{resetLink}'>{resetLink}</a><br/>如過您並未忘記密碼，請忽略該信件！";
                message.IsBodyHtml = true;

                // 設定 SMTP 客戶端
                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                // 送出郵件
                smptClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("發送電子郵件失敗。錯誤訊息：" + ex.Message);
            }
        }
        public static void SendSubcriptionSucessEmail(string toEmail, string nickname)
        {
            try
            {
                // 取得收件者信箱
                string toMail = toEmail;

                // 設定寄件者信箱和應用程式密碼
                string fromMail = "benlu38th@gmail.com";
                string fromPassword = "awnhywrxhaybbujg";

                // 建立郵件訊息物件
                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = "恭喜，你現在是 小島聊癒所 會員了！";
                message.To.Add(new MailAddress(toMail));
                message.Body = @"
<html>
<head>
    <style>
        body {
            background-color: #f4ede8;
            font-family: 'Courier New', Courier, monospace;
            color: #603c39;
            text-align: center;
            line-height: 1.6;
        }

        h1 {
            font-size: 32px;
            color: #603c39;
            text-decoration: underline;
            margin: 20px 0;
        }

        p {
            color: #603c39;
            font-size: 20px;
            margin-bottom: 10px;
        }

        .highlight {
            font-weight: bold;
            color: #8b5a4c;
        }

        .message {
            font-size: 24px;
            margin-bottom: 20px;
        }

        .button {
            display: inline-block;
            background-color: #8b5a4c;
            color: #fff;
            font-size: 18px;
            padding: 12px 24px;
            border-radius: 4px;
            text-decoration: none;
            margin-top: 20px;
            transition: background-color 0.3s;
        }

        .button:hover {
            background-color: #69453b;
        }

        hr {
            border: none;
            border-top: 2px solid #8b5a4c;
            margin: 40px auto;
            max-width: 80%;
        }

        .contact-info {
            font-size: 16px;
            margin-bottom: 20px;
        }

        .logo {
            max-width: 100px;
            margin-top: 20px;
        }
    </style>
</head>" + $@"<body>
    <h1> 恭喜成為會員 </h1>
    <p> 安安，{nickname} 您好！</p>
<p> 恭喜您成為 <span class='highlight'>Island Of Healing 小島聊癒所</span> 的會員！</p>
    <p>您將可無限制的閱覽本站的所有文章，</p>
    <p>享受 AI 聊癒身心的深度對話，</p>
    <p>趕快點擊進入多元閱讀的世界吧！</p>
    <a href = 'https://' class='button'>點擊登入</a>
    <hr />
    <div style = 'display: flex; justify-content: center; align-items: center' >
        <img src='https://islandofhealing.rocket-coding.com/upload/logo/IOHlogo.png' alt='' class='logo'/>
        <p class='contact-info'>
            客服信箱：benlu@38th@gmail.com<br/>
            小島聊癒所 Island of Healing
        </p>
    </div>
</body>
</html>";
                message.IsBodyHtml = true;

                // 設定 SMTP 客戶端
                var smptClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };

                // 送出郵件
                smptClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("發送電子郵件失敗。錯誤訊息：" + ex.Message);
            }
        }
        public static string GenerateToken()
        {
            // 生成随机的字节数组作为令牌
            byte[] tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }

            // 将字节数组转换为 Base64 字符串作为令牌
            string token = Convert.ToBase64String(tokenBytes);

            return token;
        }

        /// <summary>
        /// 輸入字串陣列，根據切割的數量，返回該數量的字串List
        /// </summary>
        /// <param name="input">輸入的字串陣列</param>
        /// <param name="cut">欲切割的字元輛</param>
        /// <returns></returns>
        public static List<string> GetWordsList(string[] input, int cut)
        {
            List<string> result = new List<string>();

            for (int y = 0; y < input.Length; y++)
            {
                for (int i = 0; i <= input[y].Length - cut; i++)
                {
                    result.Add(input[y].Substring(i, cut));
                }
            }
            return result;
        }

        /// <summary>
        /// 將輸入的字串文章切割成字串陣列詞彙
        /// </summary>
        /// <param name="article">輸入的string文章</param>
        /// <returns></returns>
        public static string[] GetWordsFromArticle(string article)
        {
            // 移除非中文字元和標點符號，只保留中文字和空格
            string cleanedArticle = Regex.Replace(article, @"[^\u4e00-\u9fa5]", " ");

            // 切割成詞彙，以空格作為分隔符號，並排除空字串
            string[] words = cleanedArticle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return words;
        }

        /// <summary>
        /// 這是一個靜態方法，用來統計一個字串List中每個詞彙的出現次數，並回傳結果以字典形式存儲。
        /// </summary>
        /// <param name="words">List，儲存詞彙</param>
        /// <returns></returns>
        public static Dictionary<string, int> CountWordFrequency(List<string> words)
        {
            // 創建一個空的字典，用於儲存詞彙與其出現次數之間的對應關係。
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();

            // 使用 foreach 迴圈來遍歷字串陣列中的每個詞彙。
            foreach (string word in words)
            {
                // 判斷字典中是否已經包含了這個詞彙。
                if (wordFrequency.ContainsKey(word))
                {
                    // 如果已經存在於字典中，則將詞彙出現次數增加1。
                    wordFrequency[word]++;
                }
                else
                {
                    // 如果詞彙不存在於字典中，則將其添加到字典中，並設置出現次數為1。
                    wordFrequency[word] = 1;
                }
            }
            // 完成詞彙統計後，回傳包含詞彙與對應出現次數的字典。
            return wordFrequency;
        }
    }
}