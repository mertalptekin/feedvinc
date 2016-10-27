using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class UserFollowModel :FollowerService, IFollow
    {
        public UserFollowModel(UnitOfWork service) : base(service)
        {

        }

        public NotificationFollowVM Follow(long follower, long followed)
        {
            var entity = new ApplicationUserFollow
            {
                FollowerID = follower,
                FollowedID = followed
            };

            _service.appUserFollowRepo.Add(entity);
            _service.Commit();

            return _service.appUserRepo.Where(a => a.ID == entity.FollowerID).Select(x => new NotificationFollowVM
            {
                NotificationName = x.Name + " " + x.SurName,
                ProfilePhoto = x.ProfilePhoto,
                NotificationText = SiteLanguage.Follow_User_Notification_Text,
                Link = "/profile/" + x.UserSlugify + "/" + x.UserCode 

            }).FirstOrDefault();
        }
    }
}