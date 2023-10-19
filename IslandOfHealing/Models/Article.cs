using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class Article
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(40)]
        [Display(Name = "文章標題")]
        public string Title { get; set; }

        [Display(Name = "文章內容")]
        public string Content { get; set; }

        [Display(Name = "文章封面")]
        public string CoverUrl { get; set; }

        [Display(Name = "閱讀權限")]
        public bool Pay { get; set; }

        [Display(Name = "文章分類")]
        public int ArticlesCategoryId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("ArticlesCategoryId")]
        [Display(Name = "所屬文章分類")]
        public virtual ArticlesCategory MyArticlesCategory { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [MaxLength(30)]
        [Display(Name = "內容摘要")]
        public string Summary { get; set; }

        [Display(Name = "標籤")]
        public string Tags { get; set; }

        [Display(Name = "文章進度")]
        public Progress Progress { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }

        [Display(Name = "收藏與愛心")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<CollectLike> CollectLikes { get; set; }

        [Display(Name = "作家")]
        public int UserId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("UserId")]
        [Display(Name = "作家")]
        public virtual User MyUser { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Display(Name = "留言")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }

        [Required]
        [Display(Name = "精選")]
        public bool Selected { get; set; }

        [Display(Name = "付費文章點擊資料")]
        public virtual ICollection<PaidArticleClicks> PaidArticleClicksInfo { get; set; }
    }
}