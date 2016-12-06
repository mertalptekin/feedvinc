using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationStore:BaseEntity<int>
    {
        public string AppNameTR { get; set; }
        public string AppNameEn { get; set; }
        public bool IsFree { get; set; }
        public string InformationTR { get; set; }
        public string InformationEN { get; set; }
        public string AppIconPath { get; set; }
        public decimal SalesPrice { get; set; }
        public string Currency { get; set; }

    }
}
