using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class ConversationComment
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "留言")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }

        [Display(Name = "最後更新時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime LatestDate { get; set; }

        [Display(Name = "使用者編號")]
        public int UserId { get; set; }

        [Display(Name = "話題編號")]
        public int ConversationId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("ConversationId")]
        [Display(Name = "所屬話題")]
        public virtual Conversation MyConversation { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應
    }
}