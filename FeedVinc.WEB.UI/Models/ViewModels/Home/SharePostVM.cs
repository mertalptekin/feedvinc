using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class SharePostVM
    {
        [Required(ErrorMessage =null,ErrorMessageResourceName ="Post_Validation",ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Post { get; set; }

        public string Location { get; set; }

        public HttpPostedFileBase MediaPhoto { get; set; }
        public HttpPostedFileBase MediaVideo { get; set; }
        public byte ShareTypeID { get; set; }
        public string MediaPath { get; set; }
        public string ShareTitle { get; set; }
        public int MediaTypeID { get; set; }


    }
}