using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class City:BaseEntity<int>
    {
        public string CityName { get; set; }
        public int? CountryID { get; set; }

    }
}
