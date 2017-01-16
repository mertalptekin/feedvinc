using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Financial
{
    public class AnswerFinancialRequestDTO
    {
        [Required]
        public string InvestmentStatus { get; set; }
        [Required]
        public string FuturePlans { get; set; }
        [Required]
        public string ProfitabilityPurchased { get; set; }
        [Required]
        public string CustomerCost { get; set; }
        [Required]
        public string SecondChance { get; set; }
        [Required]
        public string ProfitabilityRatio { get; set; }
        [Required]
        public string CashRatio { get; set; }
        [Required]
        public string FinancialHoisting { get; set; }
        [Required]
        public string ValueRatio { get; set; }
        [Required]
        public string ProductivityRate { get; set; }
        [Required]
        public string PurchaseCost { get; set; }

        public long InvestorID { get; set; }

        public long ProjectID { get; set; }


    }
}