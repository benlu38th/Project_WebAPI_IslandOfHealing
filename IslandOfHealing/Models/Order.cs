using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IslandOfHealing.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "訂單編號")]
        public string MerchantOrderNo { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "顧客名稱")]
        public string ClientName { get; set; }

        [Required]
        [MaxLength(40)]
        [EmailAddress]
        [Display(Name = "聯絡信箱")]
        public string ClientEmail { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(0)]
        [Display(Name = "聯絡電話")]
        public string ClientPhone { get; set; }

        [Required(ErrorMessage = "{0}必填")]
        [Display(Name = "創建時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime InitDate { get; set; }

        [Display(Name = "付款時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime PaidDate { get; set; }

        [Display(Name = "到期時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]//時間格式
        [DataType(DataType.DateTime)]//送出時驗證是不是時間格式
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "方案名稱")]
        public string PlanName { get; set; }

        [Required]
        [Display(Name = "方案價格")]
        public int PlanPrice { get; set; }

        [Required]
        [Display(Name = "使用者編號")]
        public int UserId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("UserId")]
        [Display(Name = "所屬使用者")]
        public virtual User MyUser { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required]
        [Display(Name = "方案編號")]
        public int PricingPlanId { get; set; }
        [JsonIgnore]//不會產生無限迴圈
        [ForeignKey("PricingPlanId")]
        [Display(Name = "所屬方案")]
        public virtual PricingPlan MyPricingPlan { get; set; }//virtual=虛擬資料，會跟資料庫的對應資料相對應

        [Required]
        [Display(Name = "付款狀態")]
        public bool Paid { get; set; }
    }
}