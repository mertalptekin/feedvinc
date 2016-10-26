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
            var model = new NotificationShareVM();

            if (shareTypeID == 1)
            {
                var user = _services.appUserRepo.FirstOrDefault(x => x.ID == userID);

                model = _services.appUserShareRepo
                .Where(x => x.ID == shareID)
                .Select(z => new NotificationShareVM
                {
                    NotificationText = SiteLanguage.Share_Notification_Text,
                    SharePrettyDate = DateTimeService.GetPrettyDate(z.ShareDate, LanguageService.getCurrentLanguage),
                    ShareProfileName = user.Name + " " + user.SurName,
                    ProfilePhotoPath = user.ProfilePhoto,
                    ShareProfileLink = "/user-profile/" + user.UserSlugify + "/" + user.UserCode

                })
                .FirstOrDefault();
            }
            else if (shareTypeID == 6)
            {
                var share = _services.communityShareRepo
                .FirstOrDefault(x => x.ID == shareID);

                var community = _services.communityRepo
                .FirstOrDefault(x => x.ID == share.CommunityID);

                model = _services
               .communityShareRepo.Where(x => x.ID == shareID)
               .Select(z => new NotificationShareVM
               {
                   NotificationText = SiteLanguage.Share_Notification_Text,
                   SharePrettyDate = DateTimeService.GetPrettyDate(z.ShareDate, LanguageService.getCurrentLanguage),
                   ShareProfileName = community.CommunityName,
                   ProfilePhotoPath = community.CommunityLogo,
                   ShareProfileLink = "/community-profile/" + community.CommunitySlug + "/" + community.CommunityCode

               })
               .FirstOrDefault();
            }
            else if (shareTypeID == 3)
            {
                var share = _services.projectShareRepo
                .FirstOrDefault(x => x.ID == shareID);

                var project = _services.projectRepo
                .FirstOrDefault(x => x.ID == share.ProjectID);

                return model = _services
                .projectShareRepo.Where(x => x.ID == shareID)
                .Select(z => new NotificationShareVM
                {
                    NotificationText = SiteLanguage.Share_Notification_Text,
                    ProfilePhotoPath = project.ProjectProfileLogo,
                    SharePrettyDate = DateTimeService.GetPrettyDate(z.ShareDate, LanguageService.getCurrentLanguage),
                    ShareProfileName = project.ProjectName,
                    ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode

                })
                .FirstOrDefault();
            }
            else if (shareTypeID == 5)
            {
                var share = _services.projectLaunchRepo
                .FirstOrDefault(x => x.ID == shareID);

                var project = _services.projectRepo
                .FirstOrDefault(x => x.ID == share.ProjectID);

                return model = _services
                .projectLaunchRepo.Where(x => x.ID == shareID)
                .Select(z => new NotificationShareVM
                {
                    NotificationText = SiteLanguage.Share_Notification_Text,
                    SharePrettyDate = DateTimeService.GetPrettyDate(z.PostDate, LanguageService.getCurrentLanguage),
                    ShareProfileName = project.ProjectName,
                    ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode

                })
                .FirstOrDefault();
            }
            else if (shareTypeID == 4)
            {
                var share = _services.projectFeedBackRepo
                .FirstOrDefault(x => x.ID == shareID);

                var project = _services.projectRepo
                .FirstOrDefault(x => x.ID == share.ProjectID);

                return model = _services
                .projectFeedBackRepo.Where(x => x.ID == shareID)
                .Select(z => new NotificationShareVM
                {
                    NotificationText = SiteLanguage.Share_Notification_Text,
                    SharePrettyDate = DateTimeService.GetPrettyDate(z.PostDate, LanguageService.getCurrentLanguage),
                    ShareProfileName = project.ProjectName,
                    ProfilePhotoPath = project.ProjectProfileLogo,
                    ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode

                })
                .FirstOrDefault();
            }
            else if (shareTypeID == 2)
            {
                var share = _services.ideaShareRepo
                .FirstOrDefault(x => x.ID == shareID);

                var project = _services.projectRepo
                .FirstOrDefault(x => x.ID == share.ProjectID);

                return model = _services
                .ideaShareRepo.Where(x => x.ID == shareID)
                .Select(z => new NotificationShareVM
                {
                    NotificationText = SiteLanguage.Share_Notification_Text,
                    SharePrettyDate = DateTimeService.GetPrettyDate(z.PostDate, LanguageService.getCurrentLanguage),
                    ShareProfileName = project.ProjectName,
                    ProfilePhotoPath = project.ProjectProfileLogo,
                    ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode

                })
                .FirstOrDefault();
            }

            return model;
        }

      /// <summary>
      /// //paylaşımı takip eden herkese bildirim gider
      /// </summary>
      /// <param name="model"></param>
        public void SendShare(string userID,NotificationSharePostVM model)
        {
            var userIDs = _services.appUserFollowRepo.
                Where(x => x.FollowedID == model.UserID).
                Select(a => a.FollowerID.ToString()).ToList();

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
        /// <param name="userID">Takip edilen kullanıcının idsi</param>
        public void SendFollow(string userID)
        {
            Clients.All.hello();
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