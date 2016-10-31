using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public interface IMessageFilter
    {
        List<MessageContactVM> GetContact(string key, long senderID);
    }
}
