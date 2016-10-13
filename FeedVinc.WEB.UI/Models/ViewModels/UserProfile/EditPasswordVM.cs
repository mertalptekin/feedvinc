using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.UserProfile
{
    public class EditPasswordVM
    {

        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})", ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "Password_Validation", ErrorMessage = null)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(SiteLanguage), ErrorMessageResourceName = "Password_ConfirmValidation", ErrorMessage = null)]
        public string PasswordConfirm { get; set; }

    }
}