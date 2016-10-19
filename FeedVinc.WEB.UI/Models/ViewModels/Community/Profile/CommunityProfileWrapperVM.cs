using FeedVinc.WEB.UI.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community.Profile
{
    public class CommunityProfileWrapperVM
    {
        public List<CommunityProfileLaunchVM> LastLaunches { get; set; }
        public List<InvestedProjectVM> LastInvestedProjects { get; set; }
        public List<CommunityProjectVM> CommunityProjects { get; set; }
        public List<CommunityMemberVM> CommunityMembers { get; set; }
        public CommunityProfileVM CommunityProfile { get; set; }
        public List<ShareVM>  CommunityFeeds { get; set; }
    }
}