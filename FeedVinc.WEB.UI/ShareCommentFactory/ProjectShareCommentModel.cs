﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels;

namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public class ProjectShareCommentModel : ShareCommentFactoryModel, IShareComment
    {
        public ProjectShareCommentModel(UnitOfWork service) : base(service)
        {
        }

        public CommentWrapper GetCommmentsByShareID(long shareID, int? pageIndex = 0)
        {

            var model = new CommentWrapper();

            model.ShareComments = _service.projectShareCommentRepo
                .Where(x => x.ProjectShareID == shareID)
                .OrderByDescending(x=> x.ID)
                .Skip(5 * (int)pageIndex)
                .Take(5)
                .Select(a => new Models.ViewModels.Home.ShareCommentVM
                {
                    CommentText = a.Comment,
                    PrettyDate = DateTimeService.GetPrettyDate(a.PostDate, LanguageService.getCurrentLanguage),
                    CommentUserID = a.UserID

                })
            .ToList();

            model.ShareComments.ForEach(a => a.CommentUser = _service.appUserRepo.Where(x => x.ID == a.CommentUserID).Select(c => new ShareCommentUserVM
            {
                UserCode = c.UserCode,
                UserName = c.Name + " " + c.SurName,
                UserProfilePhoto = c.ProfilePhoto,
                UserSlug = c.UserSlugify


            }).FirstOrDefault());

            model.PreviousPagerCount = _service.projectShareCommentRepo.Count(x => x.ProjectShareID == shareID) / 5;

            return model;
        }

        public NotificationShareVM NotifyComment(ShareCommentPostModel model, List<string> notifyUserIds)
        {
            var entity = new ProjectShareComment
            {
                ProjectShareID = (int)model.CommentShareID,
                Comment = model.CommentText,
                UserID = (int)model.CommentUserID,
                PostDate = DateTime.Now
            };

            _service.projectShareCommentRepo.Add(entity);
            _service.Commit();

            var user = _service.appUserRepo
               .FirstOrDefault(x => x.ID == model.CommentUserID);

            var share = _service.projectShareRepo
                .FirstOrDefault(x => x.ID == model.CommentShareID);

            var projectName = _service.projectRepo.FirstOrDefault(x => x.ID == share.ProjectID).ProjectName;


            if (UserManagerService.CurrentUser.ID!=model.CommentUserID)
            {
                var _notificationEntity = new ShareNotification()
                {
                    NotificationPhotoPath = user.ProfilePhoto,
                    OwnerName = user.Name + " " + user.SurName,
                    PostDate = DateTime.Now,
                    NotificationText = SiteLanguage.Share_ProjectComment + " " + projectName + " " + model.CommentText
                };

                _service.shareNotifyRepo.Add(_notificationEntity);
                _service.Commit();

                _service.shareNotifyRepo.FirstOrDefault(x => x.ID == _notificationEntity.ID).Link = "post?sharetype=" + model.ShareTypeID + "&postid=" + model.CommentShareID + "&notificationid=" + _notificationEntity.ID;

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
                    NotificationText = SiteLanguage.Share_ProjectComment + " " + projectName + " " + model.CommentText,
                    NotificationPostResult = model.CommentText,
                    ShareID = model.CommentShareID,
                    OwnerID = user.ID
                };


                return data;
            }

            return new NotificationShareVM
            {
                ShareProfileName = user.Name + " " + user.SurName,
                SharePrettyDate = DateTimeService.GetPrettyDate(DateTime.Now, LanguageService.getCurrentLanguage),
                ProfilePhotoPath = user.ProfilePhoto,
                NotificationPostResult = model.CommentText,
                ShareID = model.CommentShareID,
                OwnerID = user.ID,
                Status = "Owner"
            };


        }
    }
}