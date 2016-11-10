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
        long _currentUserID = UserManagerService.CurrentUser == null ? 0 : UserManagerService.CurrentUser.ID;


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
            ViewBag.CurrentUserID = UserManagerService.CurrentUser.ID;
             
            var model = new HomeVM();

            #region Events

            model.ClosestEvents = services.appUserActivityRepo
                .Where(x=> x.StartDate >= DateTime.Now)
                .OrderBy(a => a.StartDate)
                .Take(5)
                .Select(a => new EventHomeVM
                {
                    HuminizeDate = Convert.ToDateTime(a.StartDate).GetHuminizeDate(new System.Globalization.CultureInfo(LanguageService.getCurrentLanguage), a.Time),
                    Title = a.Title,
                    Logo = a.ActivityLogo

                })
            .ToList();

            #endregion

            #region Launches


            model.Launches = services.projectLaunchRepo.
               ToList().
               Select(f => new LastestLaunchVM
               {
                   Information = f.Information,
                   LaunchProfilePhoto = f.MediaPath,
                   PostDate = f.PostDate,
                   ProjectID = f.ProjectID

               }).OrderBy(x => x.PostDate).Take(10).ToList();


            model.Launches.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            model.Launches.ForEach(a => a.ProjectSlug = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectSlugify);

            model.Launches.ForEach(a => a.ProjectCode = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectCode);

            #endregion

            #region Communities

            int MaxSize = (int)services.communityRepo.Count();
            var randomIds = MaxSize.GenerateRandomOrder(5);

            model.RandomCommunities = services.communityRepo
                .Where(x => randomIds.Contains(x.ID))
                .Select(a => new CommunityHomeVM
                {
                    CommunityLink = "/community-profile/" + a.CommunitySlug + "/" + a.CommunityCode,
                    CommunityProfilePhoto = a.CommunityLogo
                })
            .ToList();

            #endregion

            #region FriendSuggestions

            var _UserMaxSize = (int)services.appUserRepo.Count();

            var randomUserIds = _UserMaxSize.GenerateRandomOrder(5);

            model.FriendSuggestions = services.appUserRepo
                .Where(x => randomUserIds.Contains(x.ID) && x.ID != UserManagerService.CurrentUser.ID)
                .Select(c => new FriendSuggestionHomeVM
                {

                    UserName = c.Name + " " + c.SurName,
                    UserProfileLink = "/profile/" + c.UserSlugify + "/" + c.UserCode,
                    ProfilePhoto = c.ProfilePhoto,
                    UserID = c.ID

                })
            .ToList();


            #endregion

            #region Trends

            model.Top10Trends = new List<TrendHomeVM>();

            #endregion


            return View(model);
        }
    }
}