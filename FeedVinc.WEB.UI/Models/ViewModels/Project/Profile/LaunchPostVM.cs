using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class LaunchPostVM
    {
        public long ProjectID { get; set; }
        public string Information { get; set; }
        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string WebLink { get; set; }
        public string Version { get; set; }
        public string LaunchPhoto { get; set; }

    }
}