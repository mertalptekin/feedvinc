using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUserActivityCategory:BaseEntity<byte>
    {
        public string CategoryName { get; set; }
        public int ApplicationUserActivityID { get; set; }

    }
}
