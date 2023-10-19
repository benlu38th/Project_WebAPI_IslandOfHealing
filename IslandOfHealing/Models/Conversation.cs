using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class Conversation
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(40)]
        [Display(Name = "話題標題")]
        public string Title { get; set; }

        [Display(Name = "話題內容")]
        public string Content { get; set; }

        [Display(Name = "話題封面")]
        public string CoverUrl { get; set; }

        [Display(Name = "匿名")]
        public Boolean Anonymous { get; set; }

        [Display(Name = "話題分類")]
        public int ConversationCategoryId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("ConversationCategoryId")]
        [Display(Name = "所屬話題分類")]
        public virtual ConversationsCategory MyConversationsCategory { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [MaxLength(30)]
        [Display(Name = "話題摘要")]
        public string Summary { get; set; }

        [Display(Name = "標籤")]
        public string Tags { get; set; }

        [Display(Name = "發起人")]
        public int UserId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("UserId")]
        [Display(Name = "發所屬起人")]
        public virtual User MyUser { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }

        [Display(Name = "留言")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<ConversationComment> ConversationComments { get; set; }
    }
}