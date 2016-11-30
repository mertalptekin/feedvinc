using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.ShareFactory.Models;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class CommunitySecondShareFactory : SecondShareFactory, IShare,ISharePost
    {
        public CommunitySecondShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            throw new NotImplementedException();
        }

        public NotificationShareVM Post(string userID, ShareBaseModel model)
        {
            throw new NotImplementedException();
        }
    }
}