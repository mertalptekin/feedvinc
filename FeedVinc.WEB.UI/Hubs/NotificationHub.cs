﻿using System;
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
using FeedVinc.WEB.UI.ShareCommentFactory;
using FeedVinc.WEB.UI.ShareLikeFactory;
using FeedVinc.WEB.UI.MessageFilter;

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

            return model;
        }

        public void SendSecondShare(string userID, long shareID, int shareTypeID)
        {
            long _userID = long.Parse(userID);

            var userIDs = _services.appUserFollowRepo
                .Where(x => x.FollowedID == _userID)
                .Select(a => a.FollowerID.ToString())
                .ToList();

            userIDs.Add(userID);

            ShareBaseFactory factory = new ShareBaseFactory(_services);
            var connector = factory.GetObjectInstance(shareTypeID);
            var data = connector.GetShareObject(shareID);

            SecondShareFactory Secfactory = new SecondShareFactory(_services);
            var secondConnector = Secfactory.GetObjectInstance(shareTypeID);
            var vm = secondConnector.Post(userID, data);
            

            Clients.Users(userIDs).NotifySecondShare(vm);

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

            var _notification = new ShareNotification()
            {
                Link = data.ShareProfileLink,
                NotificationPhotoPath = data.ProfilePhotoPath,
                OwnerName = data.ShareProfileName,
                PostDate = DateTime.Now,
                NotificationText = SiteLanguage.Share_Notification_Text
            };

            _services.shareNotifyRepo.Add(_notification);
            _services.Commit();

            foreach (var item in userIDs)
            {
                var entity = new ShareNotificationUser
                {
                    NotificationID = _notification.ID,
                    UserID = long.Parse(item)
                };

                _services.shareNotifyUserRepo.Add(entity);
            }

            _services.Commit();

            _services.shareNotifyRepo
               .FirstOrDefault(x => x.ID == _notification.ID)
               .Link = "post?sharetype=" + model.ShareTypeID + "&postid=" + model.ShareID + "&notificationid=" + _notification.ID;
            _services.Commit();

            data.ShareProfileLink = "post?sharetype=" + model.ShareTypeID + "&postid=" + model.ShareID +"&notificationid=" + _notification.ID;

            Clients.Users(userIDs).NotifyShare(data);

        }


        /// <summary>
        /// sadece takip edilen kullanıcıya bildirim gider
        /// </summary>
        /// <param name="userID">Takip eden kullanıcının idsi</param>
        public void SendFollow(string userID, string followedID, string followType)
        {

            FollowerFactory factory = new FollowerFactory(_services);
            IFollow connector = factory.CreateObjectInstance(followType);

            var follower = long.Parse(userID);
            string followedUserID = null;

            if (followType=="project")
            {
                var id = long.Parse(followedID);
                followedUserID = (_services.projectRepo.FirstOrDefault(x => x.ID == id).UserID).ToString();
            }
            else if (followType=="community")
            {
                var id = long.Parse(followedID);
                followedUserID = (_services.communityRepo.FirstOrDefault(x => x.ID == id).OwnerID).ToString();
            }
            else if (followType=="user")
            {
                followedUserID = followedID;
            }


            var followed = long.Parse(followedID);

            bool isExist = connector.FollowerIsExist(follower, followed);
            var model = isExist ? connector.UnFollow(follower, followed) : connector.Follow(follower, followed);

            var entity = new FollowNotification();

            entity.NotificationPhotoPath = model.ProfilePhoto;
            entity.OwnerID = long.Parse(followedUserID);
            entity.PostDate = DateTime.Now;
            entity.OwnerName = model.NotificationName;
            entity.Link = model.Link;
            entity.NotificationText = model.NotificationText;

            _services.followNotifyRepo.Add(entity);
            _services.Commit();

            List<string> Users = new List<string>();
            Users.Add(userID);
            Users.Add(followedUserID);

            model.PostID = entity.ID;
            
            Clients.Users(Users).NotifyFollow(model);
        }

        /// <summary>
        /// sadece mesaj atılan kullanıcıya bildirim gider
        /// </summary>
        /// <param name="userID">mesaj atılan kullanıcı</param>
        public void SendMessage(string userID,NotificationMessagePostVM model)
        {
            string recieverID = model.RecieverID.ToString();

            MessageManager manager = new MessageManager(new NormalUserChatService(_services));
            var data = manager.SendMessage(model);

            Clients.User(recieverID).NotifyMessage(data);
        }

        /// <summary>
        /// paylaşım türüne göre tüm commentlerin yönetildiği merkezi yer
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="model"></param>
        public void SendComment(string userID,ShareCommentPostModel model)
        {

            long followedID = long.Parse(userID);

            var userIDs = _services.appUserFollowRepo
               .Where(x => x.FollowedID == followedID)
               .Select(a => a.FollowerID.ToString())
               .ToList();

            userIDs.Add(userID);

            ShareCommentFactoryModel factory = new ShareCommentFactoryModel(_services);
            var connector =  factory.CreateObjectInstance(model.ShareType);

            var data = connector.NotifyComment(model,userIDs);

            Clients.Users(userIDs).NotifyComment(data);
        }

        /// <summary>
        /// paylaşımlara göre tüm likeların yönetildiği merkezi yer
        /// </summary>
        /// <param name="userID">beğenilen</param>
        /// <param name="model">post model</param>
        public void SendLike(string userID,ShareLikePostModel model)
        {
            long followedID = long.Parse(userID);

            var userIDs = _services.appUserFollowRepo
               .Where(x => x.FollowedID == followedID)
               .Select(a => a.FollowerID.ToString())
               .ToList();

            userIDs.Add(userID);

            ShareLikeFactoryModel factory = new ShareLikeFactoryModel(_services);
            var connector = factory.CreateObjectInstance(model.ShareType);

            var Likestatus = connector.CheckLikeIsExist(model);

            if (Likestatus==true)
            {
                var data = connector.UnLike(model);
                Clients.Users(userIDs).NotifyLike(data);
            }
            else
            {
                var data = connector.NotifyLike(model, userIDs);
                Clients.Users(userIDs).NotifyLike(data);
            }

        }
    }
}