using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectProfileOwner
    {
        public string ProjectOwnerPhotoPath { get; set; }
        public long ProjectOwnerID { get; set; }
        public string ProjectOwnerSlugify { get; set; }
        public string ProjectOwnerName { get; set; }

    }
}