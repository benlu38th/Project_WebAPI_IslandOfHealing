using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class User
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [EmailAddress]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(40)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "鹽")]
        public string Salt { get; set; }

        [MaxLength(40)]
        [Display(Name = "暱稱")]
        public string NickName { get; set; }

        [Display(Name = "照片")]
        public string ImgUrl { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "角色")]
        public string Role { get; set; }

        [Display(Name = "生日")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? Birthday { get; set; }

        [Required]
        [Display(Name = "方案")]
        public string MyPlan { get; set; }

        [Display(Name = "我的訂單")]
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [MaxLength(15)]
        [Display(Name = "頭銜")]
        public string JobTitle { get; set; }

        [Display(Name = "介紹")]
        public string Bio { get; set; }

        [Display(Name = "Guid")]
        public string Guid { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }

        [Display(Name = "文章")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<Article> Articles { get; set; }

        [Display(Name = "密碼時限")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime? PasswordTime { get; set; }

        [Display(Name = "申請成為作家進度")]
        public WriterProgress WriterProgress { get; set; }

        [Display(Name = "誰追蹤了他")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<FollowWriters> FollowWriters { get; set; }

        [Required]
        [Display(Name = "續訂會員")]
        public bool RenewMembership { get; set; }
        
        [Display(Name = "稿費")]
        [JsonIgnore]
        public virtual ICollection<Expense> Expenses { get; set; }

        [Required]
        [Display(Name = "使用AI次數")]
        public int UseAI { get; set; }

        [Display(Name ="AI問題紀錄")]
        [JsonIgnore]
        public virtual ICollection<AIMessage> AIMessages { get; set; }
    }
}