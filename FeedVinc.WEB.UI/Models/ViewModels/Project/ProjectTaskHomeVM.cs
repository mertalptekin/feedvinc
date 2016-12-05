using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectTaskHomeVM
    {
        public long CompletedTaskCount { get; set; }
        public int ProjectPhaseID { get; set; }
        public string ProjectStepLink { get; set; }

        public long TotalTaskCount { get; set; }

        public string ProjectLevel { get; set; }



    }
}