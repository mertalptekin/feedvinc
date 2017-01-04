using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectStoreVM
    {
        public int appID { get; set; }
        public string AppLogo { get; set; }
        public string AppName { get; set; }
        public long ProjectID { get; set; }
        public string PojectSlugify { get; set; }
        public string ProjectCode { get; set; }
        public long OwnerID { get; set; }


    }
}