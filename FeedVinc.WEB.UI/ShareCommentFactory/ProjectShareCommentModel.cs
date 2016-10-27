using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public class ProjectShareCommentModel : ShareCommentFactoryModel, IShareComment
    {
        public ProjectShareCommentModel(UnitOfWork service) : base(service)
        {
        }

        public NotificationShareVM NotifyComment(ShareCommentPostModel model)
        {
            var user = _service.appUserRepo
                .FirstOrDefault(x => x.ID == model.CommentUserID);

            var share = _service.projectShareRepo
                .FirstOrDefault(x => x.ID == model.CommentShareID);

            var projectName = _service.projectRepo.FirstOrDefault(x => x.ID == share.ProjectID).ProjectName;

            var data = new NotificationShareVM
            {
                ShareProfileName = user.Name + " " + user.SurName,
                SharePrettyDate = DateTimeService.GetPrettyDate(share.ShareDate, LanguageService.getCurrentLanguage),
                ProfilePhotoPath = user.ProfilePhoto,
                NotificationText = SiteLanguage.Share_ProjectComment + " " + projectName + " " + model.CommentText + " "
            };


            return data;
        }
    }
}