using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationMessageVM
    {
        public long SenderID { get; set; }
        public long RecieverID { get; set; }

        public string NotificationMessage { get; set; }
        public string UserMessage { get; set; }
        public string NotificationPrettyDate { get; set; }



    }
}