using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Event
{
    public class EventVM
    {
        public string EventTitle { get; set; }
        public string PostedUserName { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string EventProfilePhoto { get; set; }
        public string Location { get; set; }
        public long UserID { get; set; }
        public EventUserVM User { get; set; }

 
    }
}