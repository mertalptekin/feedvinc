using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectPhotoPostVM
    {
        [MultipleImageUploadValidation(ErrorMessage =null,ErrorMessageResourceName ="Multiple_Image_Upload_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public long ProjectID { get; set; }


    }

}