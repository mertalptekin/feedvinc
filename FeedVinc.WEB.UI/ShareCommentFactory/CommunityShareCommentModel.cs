using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.WEB.UI.Resources;


namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public class CommunityShareCommentModel : ShareCommentFactoryModel, IShareComment
    {
        public CommunityShareCommentModel(UnitOfWork service) : base(service)
        {
        }

        public NotificationShareVM NotifyComment(ShareCommentPostModel model)
        {
            var user = _service.appUserRepo.FirstOrDefault(x => x.ID == model.CommentUserID);

            var share = _service.communityShareRepo.FirstOrDefault(x => x.ID == model.CommentShareID);

            var communityName = _service.communityRepo.FirstOrDefault(x => x.ID == share.CommunityID).CommunityName;

            var data = _service.communityShareCommentRepo
                .Where(x => x.CommunityShareID == model.CommentShareID && x.UserID == model.CommentUserID)
                .Select(c => new NotificationShareVM
                {
                    ShareProfileName = user.Name + " " + user.SurName,
                    SharePrettyDate = DateTimeService.GetPrettyDate(share.ShareDate, LanguageService.getCurrentLanguage),
                    ProfilePhotoPath = user.ProfilePhoto,
                    NotificationText = SiteLanguage.Share_CommunityComment +" " + communityName + " " + model.CommentText + " "
                })
            .FirstOrDefault();


            return data;

        }
    }
}