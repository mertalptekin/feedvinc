using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class CommunityFollowModel : FollowerService, IFollow
    {
        public CommunityFollowModel(UnitOfWork service) : base(service)
        {
        }

        public NotificationFollowVM Follow(long follower, long followed)
        {
            var entity = new CommunityUser
            {
                CommunityID = (int)followed,
                UserID = follower
            };

            _service.communityUserRepo.Add(entity);
            _service.Commit();

            return _service.communityRepo.Where(a => a.ID == entity.CommunityID).Select(x => new NotificationFollowVM
            {
                NotificationName = x.CommunityName,
                ProfilePhoto = x.CommunityLogo,
                NotificationText = SiteLanguage.Follow_Project_Notification_Text,
                Link = "/community-profile/" + x.CommunitySlug + "/" + x.CommunityCode

            }).FirstOrDefault();
        }
    }
}