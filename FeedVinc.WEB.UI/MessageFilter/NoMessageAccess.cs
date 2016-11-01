using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class NoMessageAccess :MessageDataAccessBaseModel, IMessageFilter
    {
        public NoMessageAccess(UnitOfWork services) : base(services)
        {
        }

        public List<MessageContactVM> GetContact(string key, long senderID)
        {

            return _services.appUserRepo
               .Where(x => x.Name.Contains(key) || x.SurName.Contains(key) && ((x.NoMessageAccess==true)))
               .Select(a => new MessageContactVM
               {
                   ContactName = a.Name + " " + a.SurName,
                   MessageAccessType = SiteLanguage.Message_Access_no_One,
                   ProfilePhoto = a.ProfilePhoto,
                   RecieverID = a.ID
               })
               .ToList();
        }
    }
}