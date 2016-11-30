using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class IdeaSecondShareFactory : SecondShareFactory,IShare, ISharePost
    {
        public IdeaSecondShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            throw new NotImplementedException();
        }

        public NotificationShareVM Post(string userID, ShareBaseModel model)
        {
            long _userID = long.Parse(userID);

            var userIDs = _service.appUserFollowRepo
                .Where(x => x.FollowedID == _userID)
                .Select(a => a.FollowerID.ToString())
                .ToList();



            var _currentShare = _service.ideaShareRepo.FirstOrDefault(x => x.ID == model.PostID);
            _currentShare.ShareCount = _currentShare.ShareCount + 1;
            _service.Commit();

            var mediaShare = model as IdeaShare;

            var entity = new ProjectIdeaShare
            {
                Post = model.Post,
                ProjectID = (int)model.OwnerID,
                IsSecondShare = true,
                PostDate = DateTime.Now,
                ShareTypeID = (byte)model.ShareTypeID,
                OwnerID = _userID
            };

            _service.ideaShareRepo.Add(entity);
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
            vm.ShareID = model.PostID;
            vm.NotificationText = SiteLanguage.Share_Notification_Text;
            vm.ProfilePhotoPath = model.ShareProfilePhoto;
            vm.ShareProfileName = model.PostedBy;
            vm.SharePrettyDate = model.PrettyDate;
            vm.ProfilePhotoPath = model.ShareProfilePhoto;
            vm.ShareProfileLink = "post?sharetype=3&postid=" + entity.ID + "&notificationid=" + _notification.ID;

            return vm;
        }
    }
}