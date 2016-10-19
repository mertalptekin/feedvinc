using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class FeedBackShareVM
    {
        public string ProjectName { get; set; }
        public string ProjectProfileLogo { get; set; }
        public long ProjectID { get; set; }
        public double? FeedBackVotePoint { get; set; }
        public string FeedBackTestLink { get; set; }
        public string Information { get; set; }


    }
}