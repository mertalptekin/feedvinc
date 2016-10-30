using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;

namespace FeedVinc.WEB.UI.ShareLikeFactory
{
    public class IdeaShareLikeModel : ShareLikeFactoryModel, IShareLike
    {
        public IdeaShareLikeModel(UnitOfWork service) : base(service)
        {
        }

        public bool CheckLikeIsExist(ShareLikePostModel model)
        {
            return _service.ideaShareLikeRepo.Any(x => x.IdeaShareID == model.PostShareID && x.UserID == model.LikeOwnerID);
        }

        public NotificationShareVM NotifyLike(ShareLikePostModel model, List<string> notifyUserIds)
        {


            var _likeEntity = new ProjectIdeaShareLike
            {
                IdeaShareID = model.PostShareID,
                UserID = model.LikeOwnerID
            };

            _service.ideaShareLikeRepo.Add(_likeEntity);
            _service.Commit();


            var user = _service.appUserRepo
               .FirstOrDefault(x => x.ID == model.LikedUserID);

            var share = _service.ideaShareRepo
                .FirstOrDefault(x => x.ID == model.PostShareID);


            var _notificationEntity = new ShareNotification()
            {
                NotificationPhotoPath = user.ProfilePhoto,
                OwnerName = user.Name + " " + user.SurName,
                PostDate = DateTime.Now,
                NotificationText = SiteLanguage.Share_Idea_Like
            };

            _service.shareNotifyRepo.Add(_notificationEntity);
            _service.Commit();

            _service.shareNotifyRepo.FirstOrDefault(x => x.ID == _notificationEntity.ID).Link = "post?sharetype=" + model.ShareTypeID + "&postid=" + model.ShareTypeID + "&notificationid=" + _notificationEntity.ID;

            _service.Commit();


            foreach (var item in notifyUserIds)
            {
                var _notificationUser = new ShareNotificationUser
                {
                    NotificationID = _notificationEntity.ID,
                    UserID = long.Parse(item)
                };

                _service.shareNotifyUserRepo.Add(_notificationUser);
            }

            _service.Commit();

            var data = new NotificationShareVM
            {
                ShareProfileName = user.Name + " " + user.SurName,
                SharePrettyDate = DateTimeService.GetPrettyDate(share.PostDate, LanguageService.getCurrentLanguage),
                ProfilePhotoPath = user.ProfilePhoto,
                NotificationText = SiteLanguage.Share_Idea_Like,
                ShareProfileLink = _notificationEntity.Link,
                ShareID = share.ID,
                OwnerID = model.LikeOwnerID
            };

            return data;
        }

        public NotificationShareVM UnLike(ShareLikePostModel model)
        {
            _service.ideaShareLikeRepo.Remove(x => x.IdeaShareID == model.PostShareID && x.UserID == model.LikeOwnerID);
            _service.Commit();

            var share = _service.ideaShareRepo.FirstOrDefault(x => x.ID == model.PostShareID);

            var data = new NotificationShareVM
            {
                ShareID = share.ID,
                Status = "unlike"
            };

            return data;
        }
    }
}