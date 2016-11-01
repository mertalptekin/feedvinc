using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.MessageFilter
{
    public class PrivateMessageAccess : MessageDataAccessBaseModel, IMessageFilter
    {
        public PrivateMessageAccess(UnitOfWork services) : base(services)
        {
        }

        public List<MessageContactVM> GetContact(string key, long senderID)
        {

            var followerIDs = _services.appUserFollowRepo
                .Where(x => x.FollowedID == senderID)
                .Select(a => a.FollowerID).ToList();

            return _services.appUserRepo
                .Where(x => followerIDs.Contains(x.ID) && (x.FollowerMessageAccess==true) && (x.Name.Contains(key) || x.SurName.Contains(key)))
                .Select(a => new MessageContactVM
                {
                    ContactName = a.Name + " " + a.SurName,
                    MessageAccessType = SiteLanguage.Message_Acess_Only_Followers,
                    ProfilePhoto = a.ProfilePhoto,
                    RecieverID = a.ID
                })
                .ToList();
        }
    }
}