using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectTeamUserVM
    {
        public long UserID { get; set; }
        public string UserSlugify { get; set; }
        public string UserName { get; set; }
        public string ProfilePhoto { get; set; }
        public string UserJobType { get; set; }
        public long ProjectID { get; set; }


    }
}