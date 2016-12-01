using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Notification
{
    public class NotificationSharePostVM
    {
        public long ShareID { get; set; }
        public long UserID { get; set; }
        public int ShareTypeID { get; set; }
        public string Post { get; set; }
        public long ProjectID { get; set; }
        public long CommunityID { get; set; }

    }
}