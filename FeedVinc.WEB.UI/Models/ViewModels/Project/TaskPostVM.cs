using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class TaskPostVM
    {
        [Required(ErrorMessage = null, ErrorMessageResourceType =typeof(SiteLanguage), ErrorMessageResourceName = "AnswerToQuestion")]
        public string TaskAnswer { get; set; }
        public int TaskID { get; set; }
        public long ProjectID { get; set; }

        public int TaskTypeID { get; set; }

    }
}