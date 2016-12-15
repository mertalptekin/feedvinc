using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.UIServices;

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

            var communityName = _service.communityRepo.FirstOrDefault(x => x.ID == followed).CommunityName;

            return _service.appUserRepo.Where(a => a.ID == entity.CommunityID).Select(x => new NotificationFollowVM
            {
                NotificationName = x.Name + " " + x.SurName,
                ProfilePhoto = x.ProfilePhoto,
                NotificationText = LanguageService.getCurrentLanguage == "tr-TR" ? communityName + " " + SiteLanguage.Follow_Community_Notification_Text : SiteLanguage.Follow_Community_Notification_Text + " " + communityName,
                Link = "/profile/" + x.UserSlugify + "/" + x.UserCode,
                FollowStatus = "follow",
                FollowType="community",
                FollowerID = follower,
                FollowedID = followed

            }).FirstOrDefault();
        }

        public bool FollowerIsExist(long follower, long followed)
        {
            return _service.communityUserRepo.Any(x => x.CommunityID == followed && x.UserID == follower);
        }

        public NotificationFollowVM UnFollow(long follower, long followed)
        {
            _service.communityUserRepo.Remove(x => x.CommunityID == followed && x.UserID == follower);
            _service.Commit();

            var communityName = _service.communityRepo.FirstOrDefault(x => x.ID == followed).CommunityName;

            return _service.appUserRepo.Where(a => a.ID == followed).Select(x => new NotificationFollowVM
            {
                NotificationName = x.Name + " " + x.SurName,
                ProfilePhoto = x.ProfilePhoto,
                NotificationText = LanguageService.getCurrentLanguage == "tr-TR" ? communityName + " " + SiteLanguage.Follow_Community_Notification_Text : SiteLanguage.Follow_Community_Notification_Text + " " + communityName,
                Link = "/profile/" + x.UserSlugify + "/" + x.UserCode,
                FollowStatus = "unfollow",
                FollowerID = follower,
                FollowedID = followed,
                FollowType="community"

            }).FirstOrDefault();
        }
    }
}