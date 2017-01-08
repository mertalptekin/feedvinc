using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class InvestmentNewsShare:BaseEntity<long>
    {
        public string ProjectName { get; set; }
        public decimal InvestmentPrice { get; set; }
        public string Currency { get; set; }
        public DateTime? PrettyDate { get; set; }
        public string ProjectProfileLogo { get; set; }
        public long ShareCount { get; set; }
        public string InvestmentShareText { get; set; }
        public long ProjectID { get; set; }

    }
}
