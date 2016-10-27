using FeedVinc.WEB.UI.ShareFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class LaunchShareVM
    {
        public string AndroidLink { get; set; }
        public string WebLink { get; set; }
        public string AppleLink { get; set; }
        public string Version { get; set; }
        public string ProjectName { get; set; }
        public string ProjectProfileLogo { get; set; }
        public double? ProjectLaunchVote { get; set; }
        public string ProjectLaunchVersion { get; set; }

    }
}