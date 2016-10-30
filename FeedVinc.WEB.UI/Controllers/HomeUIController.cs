using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using app = FeedVinc.WEB.UI.MvcApplication;

namespace FeedVinc.WEB.UI.Controllers
{
    public class HomeUIController : BaseUIController
    {

        [OverrideActionFilters]
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["ApplicationUser"] != null)
            {
                return Redirect("/home");
            }

            return View();
        }

        public PartialViewResult GetFeedDefault()
        {
            var model = services.appUserShareRepo
                .ToList()
                .Select(a => new ShareVM
                {

                    UserID = a.UserID,
                    ShareCount = 0,
                    LikeCount = 0,
                    CommentCount = 0,
                    MediaTypeID = a.MediaType,
                    Location = a.Location,
                    PostDate = (DateTime)a.ShareDate,
                    Post = a.Content,
                    PostMediaPath = a.SharePath,
                    ShareTypeID = (byte)a.ShareTypeID,
                    PrettyDate = DateTimeService.GetPrettyDate((DateTime)a.ShareDate, LanguageService.getCurrentLanguage),
                    ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),

                })
            .OrderByDescending(x => x.PostDate)
            .Take(2)
            .ToList();

            model.ForEach(a => a.User = services.appUserRepo
            .Where(y => y.ID == a.UserID)
            .Select(z => new UserVM
            {
                ID = z.ID,
                FullName = z.Name + " " + z.SurName,
                UserTypeID = z.UserTypeID,
                ProfilePhoto = z.ProfilePhoto

            })
           .FirstOrDefault());

            model
                .ToList()
                .ForEach(c => c.LikeCount = services.appUserShareLikeRepo.Count(k => k.ApplicationUserShareID == c.ShareID));

            model
                .ToList()
                .ForEach(a => a.LikedCurrentUser = services.appUserShareLikeRepo.Any(f => f.ApplicationUserShareID == a.ShareID && f.UserID == UserManagerService.CurrentUser.ID));

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
        }


        public async Task<PartialViewResult> GetFeed(string uri)
        {
            var currentUserID = UserManagerService.CurrentUser.ID;

            uri = uri.Replace(":", "&");
            var index = uri.IndexOf("eq") + 3;
            var shareTypeID = uri.Substring(index, 1);

            IEnumerable<ShareVM> model = null;

            HttpResponseMessage response = await MvcApplication.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<IEnumerable<ShareVM>>();

                model
                    .ToList()
                    .ForEach(a => a.PrettyDate = DateTimeService.GetPrettyDate((DateTime)a.PostDate, LanguageService.getCurrentLanguage));

                model
                    .ToList()
                    .ForEach(a => a.ShareTypeText = GetShareTypeTextByLanguage(a.ShareTypeID));

            }

            switch (shareTypeID)
            {
                case "1":

                    model
                        .ToList()
                        .ForEach(a => a.LikedCurrentUser = services.appUserShareLikeRepo.Any(f => f.ApplicationUserShareID == a.ShareID && f.UserID == currentUserID));

                    model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "user"));

                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
                case "2":

                    model.
                        ToList().ForEach(a => a.LikedCurrentUser = services.ideaShareLikeRepo.Any(f => f.IdeaShareID == a.ShareID && f.UserID == currentUserID));

                    model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "idea"));

                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_idea.cshtml", model);
                case "3":

                    model.
                        ToList().ForEach(a => a.LikedCurrentUser = services.projectShareLikeRepo.Any(f => f.ProjectShareID == a.ShareID && f.UserID == currentUserID));

                    model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "project"));

                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_story_tellin.cshtml", model);
                case "4":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_feedback.cshtml", model);
                case "5":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_launch.cshtml", model);
                case "6":

                    model.
                        ToList().ForEach(a => a.LikedCurrentUser = services.communityShareLikeRepo.Any(f => f.CommunityShareID == a.ShareID && f.UserID == currentUserID));

                    model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "community"));

                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_community.cshtml", model);
                default:
                    return PartialView();
            }
        }


        public ActionResult Feed()
        {
            return View();
        }
    }
}