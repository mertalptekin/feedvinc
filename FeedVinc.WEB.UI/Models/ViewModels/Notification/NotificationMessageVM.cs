using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationMessageVM
    {
        public string Message { get; set; }
        public string UserProfilePhoto { get; set; }
        public string UserProfileName { get; set; }
        public string UserSlug { get; set; }
        public string UserCode { get; set; }
        public string NotificationPrettyDate { get; set; }


    }
}