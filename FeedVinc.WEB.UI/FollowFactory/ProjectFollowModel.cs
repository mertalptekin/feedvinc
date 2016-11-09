using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.Resources;

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

            return _service.projectRepo.Where(a => a.ID == entity.ProjectID).Select(x => new NotificationFollowVM
            {
                NotificationName = x.ProjectName,
                ProfilePhoto = x.ProjectProfileLogo,
                NotificationText = SiteLanguage.Follow_Project_Notification_Text,
                Link = "/project-profile/" + x.ProjectSlugify + "/" + x.ProjectCode,
                FollowType="follow",
                FollowerID = follower

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

            return _service.projectRepo.Where(a => a.ID == follower).Select(x => new NotificationFollowVM
            {
                NotificationName = x.ProjectName,
                ProfilePhoto = x.ProjectProfileLogo,
                NotificationText = SiteLanguage.UnFollow_Project_Notification_Text,
                Link = "/project-profile/" + x.ProjectSlugify + "/" + x.ProjectCode,
                FollowType = "unfollow",
                FollowerID = follower

            }).FirstOrDefault();
        }
    }
}