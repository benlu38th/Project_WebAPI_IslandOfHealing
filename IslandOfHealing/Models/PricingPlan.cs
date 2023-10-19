using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class PricingPlan
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "方案名稱")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Display(Name = "方案介紹")]
        public string Features { get; set; }

        [Required]
        [Display(Name = "價格")]
        public int Price { get; set; }

        [Required]
        [Display(Name = "週期")]
        public string BillingCycle { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }
    }
}