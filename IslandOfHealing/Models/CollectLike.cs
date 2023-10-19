using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class CollectLike
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "使用者編號")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "文章編號")]
        [JsonIgnore]//不會產生無限迴圈
        public int ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        [Display(Name = "所屬文章編號")]
        public virtual Article MyArticle { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required]
        [Display(Name = "愛心")]
        public bool Like { get; set; }

        [Display(Name = "愛心時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime LikeDate { get; set; }

        [Required]
        [Display(Name = "收藏")]
        public bool Collect { get; set; }

        [Display(Name = "收藏時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime CollectDate { get; set; }

    }
}