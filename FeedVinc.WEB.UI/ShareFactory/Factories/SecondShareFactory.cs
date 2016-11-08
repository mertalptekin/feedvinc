using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class SecondShareFactory :ISharePost
    {
        UnitOfWork _service;

        public SecondShareFactory(UnitOfWork service)
        {
            _service = service;
        }

        public NotificationShareVM Post(string userID,ShareBaseModel model)
        {
            long _userID = long.Parse(userID);

            var userIDs = _service.appUserFollowRepo
                .Where(x => x.FollowedID == _userID)
                .Select(a => a.FollowerID.ToString())
                .ToList();

            var entity = new SecondShare
            {
                Post = model.Post,
                OwnerID = model.OwnerID,
                IsActive = true,
                PostDate = DateTime.Now,
                Location = model.Location,
                MediaTypeID = (model is MediaShare) ? (model as MediaShare).MediaTypeID : 010,
                ShareID = model.PostID,
                PostMediaPath = model.ShareProfilePhoto

            };

            _service.secondShareRepo.Add(entity);
            _service.Commit();

            var _notification = new ShareNotification()
            {
                Link = model.ShareProfileLink,
                NotificationPhotoPath = model.ShareProfilePhoto,
                OwnerName = model.PostedBy,
                PostDate = DateTime.Now,
                NotificationText = SiteLanguage.Share_Notification_Text
            };

            _service.shareNotifyRepo.Add(_notification);
            _service.Commit();

            foreach (var item in userIDs)
            {
                var shareNotificationEntity = new ShareNotificationUser
                {
                    NotificationID = _notification.ID,
                    UserID = long.Parse(item)
                };

                _service.shareNotifyUserRepo.Add(shareNotificationEntity);
            }

            _service.Commit();

            var vm = new NotificationShareVM();

            vm.NotificationText = SiteLanguage.Share_Notification_Text;
            vm.ProfilePhotoPath = model.ShareProfilePhoto;
            vm.ShareProfileName = model.PostedBy;
            vm.SharePrettyDate = model.PrettyDate;
            vm.ProfilePhotoPath = model.ShareProfilePhoto;
            vm.ShareProfileLink = "post?sharetype=" + model.ShareTypeID + "&postid=" + model.PostID + "&notificationid=" + _notification.ID;

            return vm;
        }

       
    }
}