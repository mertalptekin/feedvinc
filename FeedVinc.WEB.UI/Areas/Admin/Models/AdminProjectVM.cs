using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AdminProjectVM
    {
        public string ProjectName { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectLevel { get; set; }
        public string ProjectMissionLevel { get; set; }
        public bool AccountIsActive { get; set; }
        public string ProjectLogo { get; set; }
        public int ProjectPhaseID { get; set; }
        public int ProjectCategoryID { get; set; }
        public long ProjectOwnerID { get; set; }
        public string ProjectMissionName { get; set; }
        public ProjectOwnerVM ProjectOwner { get; set; }

    }
}