using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class IdeaShareFactory : ShareBaseFactory, IShare
    {
        public IdeaShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.ideaShareRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareNormal
                {
                    OwnerID = a.ProjectID,
                    Post = a.Post,
                    PostID = a.ID,
                    ShareCount = a.ShareCount,
                    ShareTypeText = SiteLanguage._IDEA,
                    PrettyDate = DateTimeService.GetPrettyDate(a.PostDate, UIServices.LanguageService.getCurrentLanguage),
                    ShareTypeID = a.ShareTypeID

                })
                .FirstOrDefault();

            var project = _service.projectRepo
                .FirstOrDefault(x => x.ID == model.OwnerID);

            model.LikedCurrentUser = _service.ideaShareLikeRepo.Any(a => a.IdeaShareID == model.PostID && a.UserID == UserManagerService.CurrentUser.ID);

            model.LikeCount = _service.ideaShareLikeRepo.Count(a => a.IdeaShareID == model.PostID);
            model.CommentCount = _service.ideaShareCommentRepo.Count(a => a.IdeaShareID == model.PostID);

            model.ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectName;
            model.ShareProfilePhoto = model.ShareProfilePhoto;
            model.PostedBy = project.ProjectName;

            return model;
        }
    }
}