using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.UserProfile
{
    public class EmailSettingsVM
    {
        [EmailAddress(ErrorMessage =null,ErrorMessageResourceName = "EmailAddess_Validation", ErrorMessageResourceType =typeof(SiteLanguage))]
        public string Email { get; set; }
        public bool EmailNotificationEnabled { get; set; }
        public bool AccountEmailNotificationEnabled { get; set; }

    }
}