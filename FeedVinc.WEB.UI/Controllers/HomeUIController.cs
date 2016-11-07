using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.ShareCommentFactory;
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
        long _currentUserID = UserManagerService.CurrentUser == null ? 0: UserManagerService.CurrentUser.ID;


        [OverrideActionFilters]
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["ApplicationUser"] != null)
            {
                return Redirect("/home");
            }

            return View();
        }

        [HttpGet]
        public JsonResult GetComments(long ShareID, int shareTypeID, int? pageIndex = 0)
        {
            string shareType = GetShareTypeIDString(shareTypeID);

            ShareCommentFactoryModel factory = new ShareCommentFactoryModel(services);
            var connector = factory.CreateObjectInstance(shareType);

            var model = connector.GetCommmentsByShareID(ShareID, pageIndex);

            return Json(model, JsonRequestBehavior.AllowGet);

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
                    ShareID = a.ID

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

            model.ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "user").ShareComments);

            model
             .ForEach(a => a.CommentCount = services.appUserShareCommentRepo
            .Count(x => x.ApplicationUserShareID == a.ShareID));

            model
                .ToList()
                .ForEach(c => c.LikeCount = services.appUserShareLikeRepo.Count(k => k.ApplicationUserShareID == c.ShareID));

            model
                .ToList()
                .ForEach(a => a.LikedCurrentUser = services.appUserShareLikeRepo.Any(f => f.ApplicationUserShareID == a.ShareID && f.UserID == UserManagerService.CurrentUser.ID));

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
        }
    


        public async Task<PartialViewResult> GetFeedCommunity(string uri)
        {

            uri = uri.Replace(":", "&");

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

                model.
                     ToList().ForEach(a => a.LikedCurrentUser = services.communityShareLikeRepo.Any(f => f.CommunityShareID == a.ShareID && f.UserID == _currentUserID));

                model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "community").ShareComments);

            }

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_community.cshtml", model);
        }

        public async Task<PartialViewResult> GetFeedStoryTellin(string uri)
        {

            uri = uri.Replace(":", "&");

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

                model.
                       ToList().ForEach(a => a.LikedCurrentUser = services.projectShareLikeRepo.Any(f => f.ProjectShareID == a.ShareID && f.UserID == _currentUserID));

                model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "project").ShareComments);

            }

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_story_tellin.cshtml", model);
        }

        public async Task<PartialViewResult> GetFeedIdea(string uri)
        {

            uri = uri.Replace(":", "&");

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

                model.
                        ToList().ForEach(a => a.LikedCurrentUser = services.ideaShareLikeRepo.Any(f => f.IdeaShareID == a.ShareID && f.UserID == _currentUserID));

                model.ToList().ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "idea").ShareComments);

            }

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_idea.cshtml", model);
        }

        public async Task<PartialViewResult> GetFeedLaunch(string uri)
        {

            uri = uri.Replace(":", "&");

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

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_launch.cshtml", model);
        }

        public async Task<PartialViewResult> GetFeedFeedBack(string uri)
        {
            
            uri = uri.Replace(":", "&");

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

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_feedback.cshtml", model);
        }

        public async Task<PartialViewResult> GetFeedAroundMe(string uri)
        {

            uri = uri.Replace(":", "&");

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

                model
                    .ToList()
                    .ForEach(a => a.LikedCurrentUser = services.appUserShareLikeRepo.Any(f => f.ApplicationUserShareID == a.ShareID && f.UserID == _currentUserID));

                model
                    .ToList()
                    .ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID, "user").ShareComments);


            }

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
        }

        public ActionResult Feed()
        {
            return View();
        }
    }
}