using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.UserProfile
{
    public class EditUserProfile
    {

        [Range(1,10000,ErrorMessage = null, ErrorMessageResourceName = "City_Required_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int? CityID { get; set; }
        [Range(1,10000,ErrorMessage = null, ErrorMessageResourceName = "Country_Required_Validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public int? CountryID { get; set; }
        public long UserID { get; set; }
        public string FullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }
        public HttpPostedFileBase profile_picture { get; set; }
        public string ProfilePhotoPath { get; set; }
        public bool EmailIsShow { get; set; }

        [MaxLength(ErrorMessage =null,ErrorMessageResourceName = "Profile_about_validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string About { get; set; }
        public string CompanyInformation { get; set; }

        [Range(1,3,ErrorMessage =null,ErrorMessageResourceName = "UserType_Required_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public byte UserTypeID { get; set; }
        public string PhoneNumber { get; set; }
        [MaxLength(80,ErrorMessage = null, ErrorMessageResourceName = "Profile_desc_validation", ErrorMessageResourceType = typeof(SiteLanguage))]
        public string Description { get; set; }

    }
}