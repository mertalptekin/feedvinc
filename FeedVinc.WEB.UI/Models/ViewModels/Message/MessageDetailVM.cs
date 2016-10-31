using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Message
{
    public class MessageDetailVM
    {
        public int MessageID { get; set; }
        public string Message { get; set; }
        public bool IsSent { get; set; }
        public bool IsRecieved { get; set; }
        public string PrettyPostDate { get; set; }

    }
}