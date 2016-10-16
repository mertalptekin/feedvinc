using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectProfileTeamVM
    {
        public string UserName { get; set; }
        public string Title { get; set; }
        public List<string> ProjectNames { get; set; }
        public string UserProfilePhoto { get; set; }
        public string UserSlugify { get; set; }
        public long UserID { get; set; }



    }
}