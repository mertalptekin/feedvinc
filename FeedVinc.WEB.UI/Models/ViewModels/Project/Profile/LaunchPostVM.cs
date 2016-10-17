using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class LaunchPostVM
    {
        public long ProjectID { get; set; }

        [Required(ErrorMessage =null, ErrorMessageResourceName ="Information_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Information { get; set; }

        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string WebLink { get; set; }

        [Required(ErrorMessage =null, ErrorMessageResourceName ="Version_Validation",ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Version { get; set; }

        public string LaunchPhoto { get; set; }

        [FileValidation(ErrorMessageResourceName ="File_Validation",ErrorMessageResourceType =typeof(SiteLanguage))]
        public HttpPostedFileBase Photo { get; set; }


    }
}