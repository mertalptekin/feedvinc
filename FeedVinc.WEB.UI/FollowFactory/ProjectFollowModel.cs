using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class ProjectFollowModel :FollowerService, IFollow
    {
        public ProjectFollowModel(UnitOfWork service) : base(service)
        {
        }

        public NotificationFollowVM Follow(long follower, long followed)
        {
            var entity = new ProjectFollow
            {
                ProjectID = followed,
                UserID = follower
            };

            _service.projectFollowRepo.Add(entity);
            _service.Commit();

            var projectName = _service.projectRepo.FirstOrDefault(x => x.ID == followed).ProjectName;

            return _service.appUserRepo.Where(a => a.ID == follower).Select(x => new NotificationFollowVM
            {
                NotificationName = x.Name + " " + x.SurName,
                ProfilePhoto = x.ProfilePhoto,
                NotificationText = LanguageService.getCurrentLanguage == "tr-TR" ? projectName + " " + SiteLanguage.Follow_Project_Notification_Text: SiteLanguage.Follow_Project_Notification_Text + " " + projectName,
                Link = "/profile/" + x.UserSlugify + "/" + x.UserCode,
                FollowType="project",
                FollowerID = follower,
                FollowedID = followed,
                FollowStatus = "follow"

            }).FirstOrDefault();

        }

        public bool FollowerIsExist(long follower, long followed)
        {
            return _service.projectFollowRepo.Any(x => x.ProjectID == followed && x.UserID == follower);
        }

        public NotificationFollowVM UnFollow(long follower, long followed)
        {
            _service.projectFollowRepo.Remove(x => x.ProjectID == followed && x.UserID == follower);
            _service.Commit();

            var projectName = _service.projectRepo.FirstOrDefault(x => x.ID == followed).ProjectName;

            return _service.appUserRepo.Where(a => a.ID == followed).Select(x => new NotificationFollowVM
            {
                NotificationName = x.Name + " " + x.SurName,
                ProfilePhoto = x.ProfilePhoto,
                NotificationText = LanguageService.getCurrentLanguage == "tr-TR" ? projectName + " " + SiteLanguage.Follow_Project_Notification_Text : SiteLanguage.Follow_Project_Notification_Text + " " + projectName,
                Link = "/profile/" + x.UserSlugify + "/" + x.UserCode,
                FollowStatus = "unfollow",
                FollowerID = follower,
                FollowedID = followed,
                FollowType= "project"

            }).FirstOrDefault();
        }
    }
}