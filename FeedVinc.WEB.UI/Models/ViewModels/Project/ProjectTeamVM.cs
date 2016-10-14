using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectTeamVM
    {
        public long OwnerID { get; set; }
        public IEnumerable<ProjectTeamUserVM> TeamUsers { get; set; }
        public ProjectMenuVM Menu { get; set; }
        public long ProjectID { get; set; }


    }
}