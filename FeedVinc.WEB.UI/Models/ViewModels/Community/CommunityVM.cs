using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community
{
    public class CommunityVM
    {
        public string CommunityName { get; set; }
        public string CityName { get; set; }
        public string  CountryName { get; set; }
        public string CommunityProfilePhoto { get; set; }
        public string CommunityObjective { get; set; }
        public long CommunityID { get; set; }
        public string CommunityCode { get; set; }
        public string CommunitySlug { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public bool Joined { get; set; }


    }

}