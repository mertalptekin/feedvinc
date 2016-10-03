using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Account
{
    public class RegisterVM
    {
        [Required(ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "FullName_Validation")]
        public string FullName { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "EmailAddess_Validation")]
        public string Email { get; set; }

        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})", ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "Password_Validation")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "Password_ConfirmValidation")]
        public string PasswordConfirm { get; set; }

        [Range(1,3,ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "UserType_Validation")]
        public byte UserTypeID { get; set; }

    }
}