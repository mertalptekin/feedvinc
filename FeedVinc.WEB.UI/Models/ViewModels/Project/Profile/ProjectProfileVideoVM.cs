using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectProfileVideoVM
    {
        public long VideoID { get; set; }
        public string ProjectSlugify { get; set; }
        public string ProjectCode { get; set; }
        public string VideoPath { get; set; }
    }
}