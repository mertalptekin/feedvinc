using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationMarket:BaseEntity<int>
    {
        public int ApplicationProjectID { get; set; }
        public int FeedPoint { get; set; }
        public int FollowerCount { get; set; }
        public int TeamSize { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public int ApplicationProjectCategoryID { get; set; }

    }
}
