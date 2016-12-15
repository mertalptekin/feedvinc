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
    public class CommunityShareFactory : ShareBaseFactory, IShare
    {
        public CommunityShareFactory(UnitOfWork service) : base(service)
        {
        }

        public ShareBaseModel GetShareObject(long shareID)
        {
           var model = _service.communityShareRepo
                .Where(x => x.ID == shareID)
                .Select(a => new ShareNormal
            {
               MediaPath = a.SharePath,
               ShareTypeID = a.ShareTypeID,
               PostID = a.ID,
               Post = a.Content,
               ShareCount = a.ShareCount,
               MediaTypeID = a.MediaType,
               PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate,LanguageService.getCurrentLanguage),
               ShareTypeText = SiteLanguage._COMMUNITY,
               OwnerID = a.CommunityID

            })
            .FirstOrDefault();

            var community = _service.communityRepo
                .FirstOrDefault(x => x.ID == model.OwnerID);

            model.LikedCurrentUser = _service.communityShareLikeRepo.Any(a => a.CommunityShareID == model.PostID && a.UserID == UserManagerService.CurrentUser.ID);

            model.LikeCount = _service.communityShareLikeRepo.Count(a => a.CommunityShareID == model.PostID);
            model.CommentCount = _service.communityShareCommentRepo.Count(a => a.CommunityShareID == model.PostID);

            model.PostedBy = community.CommunityName;
            model.ShareProfilePhoto = community.CommunityLogo;
            model.ShareProfileLink = "/community-profile/" + community.CommunitySlug + "/" + community.CommunityCode;

            return model;
        }
    }
}