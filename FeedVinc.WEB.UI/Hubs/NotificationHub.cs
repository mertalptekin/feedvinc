using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.FollowFactory;
using FeedVinc.WEB.UI.ShareFactory.Factories;

namespace FeedVinc.WEB.UI.Hubs
{
    public class NotificationHub : Hub
    {
        private UnitOfWork _services;

        public NotificationHub()
        {
            _services = new UnitOfWork();
        }

        public NotificationShareVM CreateInstanceByShareTypeID(byte shareTypeID, long shareID, long userID)
        {
            
            ShareBaseFactory factory = new ShareBaseFactory(_services);
            var connector = factory.GetObjectInstance(shareTypeID);

            var data = connector.GetShareObject(shareID);

            var model = new NotificationShareVM();

            model.NotificationText = SiteLanguage.Share_Notification_Text;
            model.ProfilePhotoPath = data.ShareProfilePhoto;
            model.ShareProfileName = data.PostedBy;
            model.SharePrettyDate = data.PrettyDate;
            model.ProfilePhotoPath = data.ShareProfilePhoto;
            model.ShareProfileLink = "post?sharetype=" + shareTypeID + "&postid=" + shareID;

            return model;
        }

        /// <summary>
        /// //paylaşımı takip eden herkese bildirim gider
        /// </summary>
        /// <param name="model"></param>
        public void SendShare(string userID, NotificationSharePostVM model)
        {
            var userIDs = _services.appUserFollowRepo
                .Where(x => x.FollowedID == model.UserID)
                .Select(a => a.FollowerID.ToString())
                .ToList();

            var data = CreateInstanceByShareTypeID((byte)model.ShareTypeID, model.ShareID, model.UserID);

            foreach (var item in userIDs)
            {
                var entity = new ShareNotification()
                {
                    Link = data.ShareProfileLink,
                    NotificationPhotoPath = data.ProfilePhotoPath,
                    OwnerName = data.ShareProfileName,
                    OwnerID = long.Parse(item),
                    PostDate = DateTime.Now
                };

                _services.shareNotifyRepo.Add(entity);

            }

            _services.Commit();

            Clients.Users(userIDs).NotifyShare(data);

        }


        /// <summary>
        /// sadece takip edilen kullanıcıya bildirim gider
        /// </summary>
        /// <param name="userID">Takip eden kullanıcının idsi</param>
        public void SendFollow(string userID, string followedID, string followType)
        {

            FollowerFactory factory = new FollowerFactory(_services);
            IFollow connector =  factory.CreateObjectInstance(followType);

            FollowManager manager = new FollowManager(connector);
            var model = manager.Follow(long.Parse(userID),long.Parse(followedID));

            var entity = new FollowNotification();

            entity.NotificationPhotoPath = model.ProfilePhoto;
            entity.OwnerID = long.Parse(followedID);
            entity.PostDate = DateTime.Now;
            entity.OwnerName = model.NotificationName;
            entity.Link = model.Link;
            entity.NotificationText = model.NotificationText;

            _services.followNotifyRepo.Add(entity);
            _services.Commit();

            Clients.User(followedID).NotifyFollow(model);
        }

        /// <summary>
        /// sadece mesaj atılan kullanıcıya bildirim gider
        /// </summary>
        /// <param name="userID">mesaj atılan kullanıcı</param>
        public void SendMessage(string userID)
        {
            //sadece mesaj atılan kullanıcıya bildirim gider
            Clients.All.hello();
        }


    }
}