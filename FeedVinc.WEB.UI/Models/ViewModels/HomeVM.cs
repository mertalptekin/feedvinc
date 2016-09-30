using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<ActivityHomeVM> Activities { get; set; }
        public IEnumerable<FriendSuggestionHomeVM> FriendSuggestions { get; set; }
        public IEnumerable<InvestmentHomeVM> Investments { get; set; }
        public IEnumerable<LaunchHomeVM> Launches { get; set; }
        public IEnumerable<TrendHomeVM> Trends { get; set; }
        public IEnumerable<ShareHomeVM> Shares { get; set; }
        public IEnumerable<CommunityHomeVM> Communities { get; set; }

    }
}