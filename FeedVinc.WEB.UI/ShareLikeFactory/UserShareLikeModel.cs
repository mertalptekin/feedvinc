﻿using System;
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
    public class UserShareLikeModel : ShareLikeFactoryModel, IShareLike
    {
        public UserShareLikeModel(UnitOfWork service) : base(service)
        {
        }

        public bool CheckLikeIsExist(ShareLikePostModel model)
        {
            return _service.appUserShareLikeRepo.Any(x => x.ApplicationUserShareID == model.PostShareID && x.UserID == model.LikeOwnerID);
        }

        public NotificationShareVM NotifyLike(ShareLikePostModel model, List<string> notifyUserIds)
        {

            var _likeEntity = new ApplicationUserShareLike
            {
                ApplicationUserShareID = model.PostShareID,
                UserID = model.LikeOwnerID,
                IsSecondShare = false
            };

            _service.appUserShareLikeRepo.Add(_likeEntity);
            _service.Commit();


            var user = _service.appUserRepo
               .FirstOrDefault(x => x.ID == model.LikeOwnerID);

            var share = _service.appUserShareRepo
                .FirstOrDefault(x => x.ID == model.PostShareID);

            if (UserManagerService.CurrentUser.ID!=model.LikeOwnerID)
            {
                var _notificationEntity = new ShareNotification()
                {
                    NotificationPhotoPath = user.ProfilePhoto,
                    OwnerName = user.Name + " " + user.SurName,
                    PostDate = DateTime.Now,
                    NotificationText = SiteLanguage.Share_User_Like
                };

                _service.shareNotifyRepo.Add(_notificationEntity);
                _service.Commit();

                _service.shareNotifyRepo.FirstOrDefault(x => x.ID == _notificationEntity.ID).Link = "post?sharetype=" + share.ShareTypeID + "&postid=" + share.ID + "&notificationid=" + _notificationEntity.ID;

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
                    SharePrettyDate = DateTimeService.GetPrettyDate(DateTime.Now, LanguageService.getCurrentLanguage),
                    ProfilePhotoPath = user.ProfilePhoto,
                    NotificationText = SiteLanguage.Share_User_Like,
                    ShareProfileLink = _notificationEntity.Link,
                    ShareID = share.ID,
                    Status = "like",
                    OwnerID = model.LikeOwnerID
                };

                return data;

            }

            return new NotificationShareVM
            {
                ShareID = share.ID,
                Status = "Owner"
            };

        }

        public NotificationShareVM UnLike(ShareLikePostModel model)
        {
            _service.appUserShareLikeRepo.Remove(x => x.ApplicationUserShareID == model.PostShareID && x.UserID == model.LikeOwnerID);
            _service.Commit();

            var share = _service.appUserShareRepo.FirstOrDefault(x => x.ID == model.PostShareID);

            var data = new NotificationShareVM
            {
                ShareID = share.ID,
                Status = "unlike"
            };

            return data;
        }
    }
}