using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationShareVM
    {
        public long ShareNotificationID { get; set; }
        public string NotificationText { get; set; }
        public string ShareProfileName { get; set; }
        public string SharePrettyDate { get; set; }
        public string ShareProfileLink { get; set; }
        public string ProfilePhotoPath { get; set; }
        public long ShareID { get; set; }
        public string Status { get; set; }
        public long OwnerID { get; set; }

        public string NotificationPostResult { get; set; }

    }
}