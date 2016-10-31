using FeedVinc.WEB.UI.MessageFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Message
{
    public class MessageWrapperVM
    {
        public List<MessageVM> LastMessages { get; set; }
        public List<MessageDetailVM> MessageDetails { get; set; }
        public List<MessageContactVM> Contacts { get; set; }
    }
}