using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community.Profile
{
    public class CommunityProfileLaunchVM
    {
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public string ProjectCode { get; set; }
        public string LaunchProfilePhoto { get; set; }
        public string Information { get; set; }
        public DateTime? PostDate { get; set; }
        public long ProjectID { get; set; }

    }
}