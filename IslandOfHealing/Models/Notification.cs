using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using IslandOfHealing.Models;

namespace IslandOfHealing.Models
{
    public class Notification
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "被通知人編號")]
        public int UserId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("UserId")]
        [Display(Name = "所屬被通知人")]
        public virtual User MyUser { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required]
        [Display(Name = "通知人編號")]
        public int SenderId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("SenderId")]
        [Display(Name = "所屬通知人")]
        public virtual NotificationSender MyNotificationSender { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required]
        [Display(Name = "訊息代碼")]
        public NotificationContentId NotificationContentId { get; set; }

        [Required]
        [Display(Name = "訊息內容")]
        public string NotificationContent { get; set; }

        [Display(Name = "追蹤者新文章id")]
        public int? FollowedWriterNewArticleId { get; set; }

        [Display(Name = "追蹤者新文章Title")]
        public string FollowedWriterNewArticleTitle { get; set; }

        [Required]
        [Display(Name ="已讀")]
        public bool IsRead { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}