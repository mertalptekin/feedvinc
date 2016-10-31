using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Message
{
    public class MessageVM
    {
        public long MessageID { get; set; }
        public long SenderID { get; set; }
        public string LastMessage { get; set; }
        public string LastLook { get; set; }
        public MessageUserVM User { get; set; }
        public List<MessageDetailVM> MessageDetails { get; set; }

    }
}