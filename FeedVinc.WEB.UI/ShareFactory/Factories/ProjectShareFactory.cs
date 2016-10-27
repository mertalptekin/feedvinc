using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class ProjectShareFactory : ShareBaseFactory, IShare
    {
        public ProjectShareFactory(UnitOfWork service) : base(service)
        {
        }

        public string SiteLangugage { get; private set; }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.projectShareRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareNormal
                {
                    CommentCount = 0,
                    LikeCount = 0,
                    ShareCount = 0,
                    Post = a.Content,
                    MediaPath = a.SharePath,
                    MediaTypeID = a.MediaType,
                    ShareTypeID = a.ShareTypeID,
                    OwnerID = a.ProjectID,
                    PostID = a.ID,
                    PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate, UIServices.LanguageService.getCurrentLanguage),
                    ShareTypeText = SiteLanguage._STORY_TELLING


                })
                .FirstOrDefault();

            var project = _service.projectRepo.FirstOrDefault(x => x.ID == model.OwnerID);

            model.ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode;
            model.PostedBy = project.ProjectName;
            model.ShareProfilePhoto = project.ProjectProfileLogo;

            return model;

        }
    }
}