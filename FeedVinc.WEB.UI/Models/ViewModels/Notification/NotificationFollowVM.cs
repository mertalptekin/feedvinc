using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationFollowVM
    {
        public string NotificationName { get; set; }
        public string Link { get; set; }
        public string ProfilePhoto { get; set; }
        public string NotificationText { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string FollowType { get; set; }
        public long FollowerID { get; set; }
        public long FollowedID { get; set; }
        public string FollowStatus { get; set; }
        public long PostID { get; set; }



    }
}