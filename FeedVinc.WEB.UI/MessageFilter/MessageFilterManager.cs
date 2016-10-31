using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class MessageFilterManager
    {
        IMessageFilter _filter;

        public MessageFilterManager(IMessageFilter filter)
        {
            _filter = filter;
        }

        public List<MessageContactVM> GetContact(string key,long senderID)
        {
           return _filter.GetContact(key,senderID);
        }
    }
}