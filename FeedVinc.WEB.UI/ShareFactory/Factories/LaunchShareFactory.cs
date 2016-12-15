using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.Common.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class LaunchShareFactory : ShareBaseFactory, IShare
    {
        public LaunchShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.projectLaunchRepo.Where(x => x.ID == shareID).Select(a => new ShareLaunch
            {
                AndroidLink = a.AndroidLink,
                IPhoneLink = a.AppleLink,
                WebLink = a.WebLink,
                MediaPath = a.MediaPath,
                MediaTypeID = a.MediaTypeID,
                OwnerID = a.ProjectID,
                Post = a.Information,
                PostID = a.ID,
                ShareTypeID = a.ShareTypeID,
                PrettyDate = DateTimeService.GetPrettyDate(a.PostDate, UIServices.LanguageService.getCurrentLanguage),
                ShareCount = a.ShareCount,
                Version = a.ProjectVersion

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