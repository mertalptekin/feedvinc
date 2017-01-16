using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class AnswerFinancialRequest:BaseEntity<int>
    {
        public string InvestmentStatus { get; set; }
        public string FuturePlans { get; set; }
        public string ProfitabilityPurchased { get; set; }
        public string CustomerCost { get; set; }
        public string SecondChance { get; set; }
        public string ProfitabilityRatio { get; set; }
        public string CashRatio { get; set; }
        public string FinancialHoisting { get; set; }
        public string ValueRatio { get; set; }
        public string ProductivityRate { get; set; }
        public string PurchaseCost { get; set; }


    }
}
