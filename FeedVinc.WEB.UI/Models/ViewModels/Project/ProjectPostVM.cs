using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectPostVM
    {
        public int? ID { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName = "Project_Name_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string ProjectName { get; set; }

        [MaxLength(80, ErrorMessageResourceName = "Project_SalesPitch_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string SalesPitch { get; set; }

        [FileValidation(ErrorMessage =null,ErrorMessageResourceName = "File_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public HttpPostedFileBase ProjectPhoto { get; set; }
        public string ProjectProfileLogo { get; set; }

        [Range(1,10000, ErrorMessageResourceName = "Project_Category_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public byte CategoryID { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "Project_County_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int CountryID { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "Project_City_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int CityID { get; set; }

        public string WebLink { get; set; }
        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string About { get; set; }

        [Range(1, 10, ErrorMessageResourceName = "Project_Status_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public byte ProjectStatus { get; set; }

        [Range(1, 10, ErrorMessageResourceName = "Project_Status_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public byte ProjectInvestmentStatus { get; set; }

        public string ProjectTags { get; set; }
        public string ProjectSlugify { get; set; }
        public string ProjectCode { get; set; }
        public int MenuID { get; set; }

    }
}