using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class FeedBackPostVM
    {
        public string TestLink { get; set; }
        public long ProjectID { get; set; }
        public string Information { get; set; }
        public string FeedBackLogo { get; set; }
        public bool InformationEnabledInvestor { get; set; }

    }
}