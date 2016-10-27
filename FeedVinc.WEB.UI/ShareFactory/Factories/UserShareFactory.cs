using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.WEB.UI.Resources;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class UserShareFactory : ShareBaseFactory, IShare
    {
        public UserShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
            var model = _service.appUserShareRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareNormal
                {
                    CommentCount = 0,
                    LikeCount = 0,
                    MediaPath = a.SharePath,
                    Post = a.Content,
                    PostID = a.ID,
                    ShareCount = 0,
                    ShareTypeID = a.ShareTypeID,
                    MediaTypeID = a.MediaType,
                    PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate, LanguageService.getCurrentLanguage),
                    ShareTypeText = SiteLanguage._AROUNDME,
                    OwnerID = a.UserID

                })
            .FirstOrDefault();

            var user = _service.appUserRepo.FirstOrDefault(x => x.ID == model.OwnerID);

            model.PostedBy = user.Name + " " + user.SurName;
            model.ShareProfilePhoto = user.ProfilePhoto;
            model.ShareProfileLink = "/profile/" + user.UserSlugify + "/" + user.UserCode;


            return model;
        }
    }
}