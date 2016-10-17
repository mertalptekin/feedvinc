using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class FeedBackPostVM
    {

        [Required(ErrorMessage =null, ErrorMessageResourceName ="TestLink_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string TestLink { get; set; }
        public long ProjectID { get; set; }

        [Required(ErrorMessage =null, ErrorMessageResourceName ="Information_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Information { get; set; }


        public string FeedBackLogo { get; set; }
        public bool InformationEnabledInvestor { get; set; }

        [FileValidation(ErrorMessageResourceName ="File_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public HttpPostedFileBase Photo { get; set; }


    }
}