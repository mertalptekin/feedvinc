using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class InvestmentNewsShareFactory : ShareBaseFactory, IShare
    {
        public InvestmentNewsShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.InvestmentNewsShareRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareNormal
                {
                    ShareCount = a.ShareCount,
                    Post = a.InvestmentShareText,
                    MediaPath = "",
                    MediaTypeID = null,
                    ShareTypeID = 7,
                    OwnerID = a.ProjectID,
                    PostID = a.ID,
                    PrettyDate = DateTimeService.GetPrettyDate(a.PrettyDate, UIServices.LanguageService.getCurrentLanguage),
                    ShareTypeText = SiteLanguage.InvestedNews


                })
                .FirstOrDefault();

            var project = _service.projectRepo.FirstOrDefault(x => x.ID == model.OwnerID);

            model.LikedCurrentUser = _service.InvestmentNewsLikeRepo.Any(a => a.InvestmentNewsID == model.PostID && a.ApplicationUserID == UserManagerService.CurrentUser.ID);

            model.LikeCount = _service.InvestmentNewsLikeRepo.Count(a => a.ApplicationUserID == UserManagerService.CurrentUser.ID);
            model.CommentCount = _service.InvestmentNewsCommentRepo.Count(a => a.InvestmentNewsID == model.PostID);


            model.ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode;
            model.PostedBy = project.ProjectName;
            model.ShareProfilePhoto = project.ProjectProfileLogo;

            return model;
        }
    }
}