using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community
{
    public class CommunityPostVM
    {
        public long CommunityID { get; set; }
        [Required(ErrorMessage = null, ErrorMessageResourceName = "Community_Name_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string CommunityName { get; set; }

        [MaxLength(80, ErrorMessageResourceName = "Community_Objective_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string CommunityObjective { get; set; }

        [FileValidation(ErrorMessage = null, ErrorMessageResourceName = "File_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public HttpPostedFileBase CommunityPhoto { get; set; }
        public string CommunityProfilePhoto { get; set; }
        [Range(1, 10000, ErrorMessageResourceName = "County_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int CountryID { get; set; }
        [Range(1, 10000, ErrorMessageResourceName = "City_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int CityID { get; set; }
        public string WebLink { get; set; }

        public string About { get; set; }

        public CommunityMenuVM CommunityMenu { get; set; }

    }
}