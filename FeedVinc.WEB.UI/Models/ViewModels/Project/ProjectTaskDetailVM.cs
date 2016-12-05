using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectTaskDetailVM
    {
        public string TaskDescription { get; set; }
        public string TaskTitle { get; set; }
        public string TaskLogo { get; set; }
        public int TaskID { get; set; }
        public long ProjectID { get; set; }
        public string Answer { get; set; }
        public string HyperLink { get; set; }
        public int TaskTypeID { get; set; }

        public bool HasTextInput { get; set; }
        public bool HasHyperLink { get; set; }

    }
}