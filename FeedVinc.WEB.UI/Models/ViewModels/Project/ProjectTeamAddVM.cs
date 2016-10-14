using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectTeamAddVM
    {
        public long ProjectID { get; set; }
        public long OwnerID { get; set; }
        public string AddMemberEmail { get; set; }

    }
}