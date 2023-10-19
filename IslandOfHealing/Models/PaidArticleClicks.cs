using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class PaidArticleClicks
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "點擊人編號")]
        public int ClickerId { get; set; }

        [Required]
        [Display(Name = "付費文章編號")]
        public int PaidArticleId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("PaidArticleId")]
        [Display(Name = "所屬付費文章")]
        public virtual Article MyPaidArticle { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}