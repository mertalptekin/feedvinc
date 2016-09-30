using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationStore:BaseEntity<byte>
    {
        public string AppName { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public string Information { get; set; }
        public string AppIconPath { get; set; }
        public int ApplicationProjectID { get; set; }
        public decimal SalesPrice { get; set; }
        public string Currency { get; set; }

    }
}
