using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class FollowWriters
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "追蹤人編號")]
        public int FollowerId { get; set; }

        [Required]
        [Display(Name = "被追蹤人編號")]
        public int WriterWhoBeFollowedId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("WriterWhoBeFollowedId")]
        [Display(Name = "使用者編號")]
        public virtual User MyUser { get; set; } //virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}