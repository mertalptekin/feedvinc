using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Trend;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class TrendUIController : BaseUIController
    {
        // GET: Trend
        public ActionResult Index(string tag)
        {
            
            ViewBag.Tag = "#" + tag;
            tag = "#" + tag;
            var vm = new TrendWrapperVM();

            #region TrendPost

            var trendPosts = services.appUserShareRepo.Where(x => x.Content.Contains(tag)).Select(a => new TrendPostVM
            {
                ShareID = a.ID,
                ShareCount = a.ShareCount,
                Content = a.Content,
                Type = SiteLanguage._AROUNDME,
                UserID = a.UserID,
                PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate,LanguageService.getCurrentLanguage)

            }).ToList();

            trendPosts.ForEach(a => a.LikedCurrentUser = services.appUserShareLikeRepo.Any(x => x.UserID == a.UserID && x.ApplicationUserShareID == a.ShareID));

            trendPosts.ForEach(c => c.LikeCount = services.appUserShareLikeRepo.Count(z => z.ApplicationUserShareID == c.ShareID));

            trendPosts.ForEach(y => y.CommentCount = services.appUserShareCommentRepo.Count(z => z.ApplicationUserShareID == y.ShareID));

            trendPosts.ForEach(c => c.User = services.appUserRepo.Where(x => x.ID == c.UserID).Select(v => new TrendUserVM
            {
                UserCode = v.UserCode,
                UserName = v.Name + " " + v.SurName,
                UserProfilePhoto = v.ProfilePhoto,
                UserSlug = v.UserSlugify

            }).FirstOrDefault());

            #endregion

            #region TrendHashTag

            var hashTags = services.appUserShareTagDetailRepo
                .ToList()
                .GroupBy(c => c.ApplicationUserShareTagID)
                .Select(a => new TrendHomeVM
                {
                    HashTagID = a.Key,
                    ShareCount = a.Count()

                })
            .OrderByDescending(y => y.ShareCount)
            .Take(5)
            .ToList();

            hashTags.ForEach(a => a.HashTag = services.appUserShareTagRepo.FirstOrDefault(x => x.ID == a.HashTagID).HashTag);

            #endregion

            vm.HashTags = hashTags;
            vm.TrendPosts = trendPosts;


            return View(vm);
        }
    }
}