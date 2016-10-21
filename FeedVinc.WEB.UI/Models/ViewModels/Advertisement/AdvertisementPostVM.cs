using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Models.ViewModels.Advertisement
{
    public class AdvertisementPostVM
    {
        [Required(ErrorMessage = null, ErrorMessageResourceName = "Required_validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string Description { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "UserType_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int? UserTypeID { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "City_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int? CityID { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "County_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int? CountryID { get; set; }

        [Range(1, 10000, ErrorMessageResourceName = "Project_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public long ProjectID { get; set; }

        [Required(ErrorMessage = null, ErrorMessageResourceName ="Required_validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Title { get; set; }


    }
}