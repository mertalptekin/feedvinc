using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class NotificationHeaderDTO
    {
        public int FollowNotificationsCount { get; set; }
        public int MessageNotificationCount { get; set; }
        public int ShareNotificationCount { get; set; }
        public int MissionNotificationCount { get; set; }

    }
}