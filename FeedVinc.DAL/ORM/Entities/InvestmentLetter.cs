using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class InvestmentLetter: BaseEntity<int>
    {
        public string Message { get; set; }
        public string CustomerSegment { get; set; }
        public string ProjectOverview { get; set; }
        public string MarketPotential  { get; set; }
        public string ValueProposition  { get; set; }
        public string InvestmentStatus { get; set; }
        public string InvestmentExpectancy  { get; set; }
        public string TeamAndCollaborators { get; set; }
        public string CompetitorAnalysis  { get; set; }
        public string FinancialCondition  { get; set; }
        public int? ProjectID { get; set; }


    }
}
