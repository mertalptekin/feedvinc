using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectVideoVM
    {
        public IEnumerable<ProjectVideoEditVM> ProjectVideos{ get; set; }
        public ProjectMenuVM Menu { get; set; }
    }
}