using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class HomeVM
    {
        public List<LastestLaunchVM> Launches { get; set; }
        public List<FriendSuggestionHomeVM> FriendSuggestions { get; set; }
        public List<EventHomeVM> ClosestEvents { get; set; }
        public List<CommunityHomeVM> RandomCommunities { get; set; }

        public List<InvestedProjectVM> RandomInvestedProjects { get; set; }

        public List<TrendHomeVM> Top10Trends { get; set; }

    }
}