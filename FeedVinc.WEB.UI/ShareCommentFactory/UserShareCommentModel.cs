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
    public class UserShareCommentModel : ShareCommentFactoryModel,IShareComment
    {
        public UserShareCommentModel(UnitOfWork service) : base(service)
        {
        }

        public NotificationShareVM NotifyComment(ShareCommentPostModel model)
        {
            var user = _service.appUserRepo
                .FirstOrDefault(x => x.ID == model.CommentUserID);

            var share = _service.appUserShareRepo
                .FirstOrDefault(x => x.ID == model.CommentShareID);

            var data = new NotificationShareVM
            {
                ShareProfileName = user.Name + " " + user.SurName,
                SharePrettyDate = DateTimeService.GetPrettyDate(share.ShareDate, LanguageService.getCurrentLanguage),
                ProfilePhotoPath = user.ProfilePhoto,
                NotificationText = SiteLanguage.Share_UserComment + " " + model.CommentText + " "
            };


            return data;
        }
    }
}