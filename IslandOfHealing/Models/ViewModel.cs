using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class ViewModel
    {
        public class LoginSignUp
        {
            /// <summary>
            /// 使用者帳號
            /// </summary>
            [Required]
            [MaxLength(40)]
            [EmailAddress]
            public string Account { get; set; }

            /// <summary>
            /// 使用者密碼
            /// </summary>
            [Required]
            [MinLength(8)]
            [MaxLength(40)]
            [Display(Name = "密碼")]
            public string Password { get; set; }
        }

        public class SetPwd
        {
            /// <summary>
            /// 使用者密碼
            /// </summary>
            [Required]
            [MinLength(8)]
            [MaxLength(40)]
            [Display(Name = "密碼")]
            public string Password { get; set; }

            /// <summary>
            /// 使用者密碼確認
            /// </summary>
            [Required]
            [MinLength(8)]
            [MaxLength(40)]
            [Display(Name = "密碼確認")]
            public string ConfirmPassword { get; set; }
        }

        public class UpdateUserInfo
        {
            [MaxLength(40)]
            [Display(Name = "暱稱")]
            public string NickName { get; set; }

            [Display(Name = "生日")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]//時間格式
            [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
            public DateTime? Birthday { get; set; }

            [MaxLength(15)]
            [Display(Name = "頭銜")]
            public string JobTitle { get; set; }

            [Display(Name = "介紹")]
            public string Bio { get; set; }
        }
        public class ChargeRequest
        {
            [Required]
            [MaxLength(40)]
            [Display(Name = "顧客名稱")]
            public string ClientName { get; set; }

            [Required]
            [MaxLength(40)]
            [EmailAddress]
            [Display(Name = "聯絡信箱")]
            public string ClientEmail { get; set; }

            [Required]
            [MaxLength(15)]
            [Phone]
            [Display(Name = "聯絡電話")]
            public string ClientPhone { get; set; }

            [Required]
            [Display(Name = "方案編號")]
            public int PlanId { get; set; }

        }
        public class ArticleProgress
        {
            [Required]
            [Display(Name = "文章進度")]
            public Progress Progress { get; set; }
        }
        public class UserApplyForWriterProgress
        {
            [Required]
            [Display(Name = "申請作家進度")]
            public WriterProgress WriterProgress { get; set; }
        }
        public class CreateArticle
        {
            [MaxLength(40)]
            [Display(Name = "文章標題")]
            public string Title { get; set; }

            [Display(Name = "文章內容")]
            public string Content { get; set; }

            [Display(Name = "標籤")]
            public string[] Tags { get; set; }

            [Required]
            [Display(Name = "閱讀權限")]
            public bool Pay { get; set; }

            [Required]
            [Display(Name = "文章分類")]
            public int ArticlesClassId { get; set; }

            [MaxLength(30)]
            [Display(Name = "內容摘要")]
            public string Summary { get; set; }

            [Required]
            [Display(Name = "文章進度")]
            public Progress Progress { get; set; }
        }
        public class ArticleComment
        {
            [Required]
            [MaxLength(200)]
            [Display(Name = "留言")]
            public string Comment { get; set; }

            [Display(Name = "文章編號")]
            public int ArticleId { get; set; }
        }
        public class ArticleCommentUpdate
        {
            [Required]
            [Display(Name = "留言編號")]
            public int CommentId { get; set; }

            [Required]
            [MaxLength(200)]
            [Display(Name = "留言")]
            public string Comment { get; set; }
        }
        public class ReadCategoryArticles
        {
            [Display(Name = "文章分類")]
            public int ArticlesCategoryId { get; set; }

            [Display(Name = "第幾頁")]
            public int Page { get; set; }

            [Display(Name = "使用者編號")]
            public int UserId { get; set; }

        }
        public class ReadAllArticles
        {

            [Display(Name = "第幾頁")]
            public int Page { get; set; }

            [Display(Name = "使用者編號")]
            public int UserId { get; set; }
        }
        public class InputExpense
        {
            [Required]
            [Display(Name = "費用單編號")]
            public int Id { get; set; }

            [Required]
            [MaxLength(40)]
            [Display(Name = "合約編號")]
            public string ContractId { get; set; }

            [Required(ErrorMessage = "{0}必填")]
            [Display(Name = "付款時間")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
            [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
            public DateTime PayDate { get; set; }

            [Required]
            [Display(Name = "金額")]
            public int Amount { get; set; }

            [Required]
            [Display(Name = "付款情形")]
            public bool Paid { get; set; }
        }

        public class InputAIMessage
        {
            [Required]
            [Display(Name = "問題")]
            public string UserQuestion { get; set; }

            [Required]
            [Display(Name = "AI")]
            public AI AI { get; set; }

            [Required]
            [Display(Name = "回答")]
            public string AIAnswer { get; set; }
        }
        public class IntputConversationsCategory
        {
            [Required]
            [MaxLength(15)]
            [Display(Name = "話題分類")]
            public string Name { get; set; }

            [Required]
            [MaxLength(200)]
            [Display(Name = "分類介紹")]
            public string Description { get; set; }
        }
        public class InputCreateConversation
        {
            [MaxLength(40)]
            [Display(Name = "話題標題")]
            public string Title { get; set; }

            [Display(Name = "話題內容")]
            public string Content { get; set; }

            [Display(Name = "標籤")]
            public string[] Tags { get; set; }

            [Required]
            [Display(Name = "匿名")]
            public Boolean Anonymous { get; set; }

            [Required]
            [Display(Name = "話題分類(XX相談室)")]
            public int ConversationsCategoryId { get; set; }

            [MaxLength(30)]
            [Display(Name = "內容摘要")]
            public string Summary { get; set; }
        }

        public class ConversationComment
        {
            [Required]
            [MaxLength(200)]
            [Display(Name = "留言")]
            public string Comment { get; set; }

            [Display(Name = "話題編號")]
            public int ConversationId { get; set; }
        }

        public class ConversationCommentUpdate
        {
            [Required]
            [Display(Name = "留言編號")]
            public int CommentId { get; set; }

            [Required]
            [MaxLength(200)]
            [Display(Name = "留言")]
            public string Comment { get; set; }
        }
        public class ReadCategoryConversations
        {
            [Display(Name = "話題分類")]
            public int ConversationsCategoryId { get; set; }

            [Display(Name = "第幾頁")]
            public int Page { get; set; }
        }
    }
}