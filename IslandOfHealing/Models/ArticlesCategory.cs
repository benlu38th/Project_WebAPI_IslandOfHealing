using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class ArticlesCategory
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "文章分類")]
        public string Name { get; set; }

        [Display(Name = "文章")]
        [JsonIgnore]//不會產生無限迴圈
        public virtual ICollection<Article> Articles { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }


    }
}