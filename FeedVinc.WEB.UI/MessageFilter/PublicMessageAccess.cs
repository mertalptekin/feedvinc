using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class PublicMessageAccess :MessageAccessBaseModel, IMessageFilter
    {
        public PublicMessageAccess(UnitOfWork services) : base(services)
        {
        }

        public List<MessageContactVM> GetContact(string key,long senderID)
        {
            return _services.appUserRepo
                .Where(x => (x.Name.Contains(key) || x.SurName.Contains(key)) && (x.PublicMessageAccess==true))
                .Select(a => new MessageContactVM
                {
                    ContactName = a.Name + " " + a.SurName,
                    MessageAccessType = "public",
                    ProfilePhoto=a.ProfilePhoto,
                    RecieverID = a.ID
                })
                .ToList();
        }
    }
}