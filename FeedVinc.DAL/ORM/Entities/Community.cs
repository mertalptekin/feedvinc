using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class Community:BaseEntity<int>
    {
        public string CommunityName { get; set; }
        //Topluluk Hedefi
        public string CommunityObjective { get; set; }
        public string CommunityLogo { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public string WebLink { get; set; }
        public string About { get; set; }
        public long OwnerID { get; set; }
        public string CommunitySlug { get; set; }
        public string CommunityCode { get; set; }



    }
}
