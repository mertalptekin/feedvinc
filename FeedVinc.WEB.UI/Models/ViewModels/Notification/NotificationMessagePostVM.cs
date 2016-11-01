using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationMessagePostVM
    {
        public long SenderID { get; set; }
        public long RecieverID { get; set; }
        public string Message { get; set; }

    }
}