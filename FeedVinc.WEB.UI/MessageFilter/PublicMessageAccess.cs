using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class PublicMessageAccess :MessageDataAccessBaseModel, IMessageFilter
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
                    MessageAccessType = SiteLanguage.Message_Access_Public,
                    ProfilePhoto=a.ProfilePhoto,
                    RecieverID = a.ID
                })
                .ToList();
        }
    }
}