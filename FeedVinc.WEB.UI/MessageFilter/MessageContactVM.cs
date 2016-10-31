using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class MessageContactVM
    {
        public long RecieverID { get; set; }
        public string ContactName { get; set; }
        public string MessageAccessType { get; set; }
        public string ProfilePhoto { get; set; }

    }
}