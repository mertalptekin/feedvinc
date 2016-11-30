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
    public class FeedBackShareFactory :ShareBaseFactory, IShare
    {
        public FeedBackShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.projectFeedBackRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareFeedBack
            {
                MediaPath = a.FeedBackMedia,
                MediaTypeID = a.MediaTypeID,
                OwnerID = a.ProjectID,
                Post = a.Information,
                PostID = a.ID,
                PrettyDate = DateTimeService.GetPrettyDate(a.PostDate, UIServices.LanguageService.getCurrentLanguage),
                ShareCount = 0,
                ShareTypeID = (byte)a.ShareTypeID,
                ShareTypeText = SiteLanguage._FEEDBACK,
                TestLink = a.TestLink

            })
            .FirstOrDefault();

            var project = _service.projectRepo.FirstOrDefault(a => a.ID == model.OwnerID);

            model.ShareProfileLink = "/project-profile/" + project.ProjectSlugify + "/" + project.ProjectCode;
            model.ShareProfilePhoto = project.ProjectProfileLogo;
            model.PostedBy = project.ProjectName;

            model.VoteCount = _service.projectFeedBackVote.Where(x => x.ProjectFeedBackID == model.PostID).Average(y=> y.FeedBackVotePoint);

            return model;

        }
    }
}