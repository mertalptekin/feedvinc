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
            var model = services.appUserShareRepo.ToList().Select(a => new ShareVM
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

            }).OrderByDescending(x => x.PostDate).Take(2).ToList();

            model.ForEach(a => a.User = services.appUserRepo.Where(y => y.ID == a.UserID).Select(z => new UserVM
            {
                ID = z.ID,
                FullName = z.Name + " " + z.SurName,
                UserTypeID = z.UserTypeID,
                ProfilePhoto = z.ProfilePhoto

            }).FirstOrDefault());

            return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
        }


        public async Task<PartialViewResult> GetFeed(string uri)
        {
           
            uri = uri.Replace(":", "&");
            var index = uri.IndexOf("eq") + 3;
            var shareTypeID = uri.Substring(index, 1);
            IEnumerable<ShareVM> model = null;

            HttpResponseMessage response = await MvcApplication.client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                model = await response.Content.ReadAsAsync<IEnumerable<ShareVM>>();

                model.ToList().ForEach(a => a.PrettyDate = DateTimeService.GetPrettyDate((DateTime)a.PostDate, LanguageService.getCurrentLanguage));

                model.ToList().ForEach(a => a.ShareTypeText = GetShareTypeTextByLanguage(a.ShareTypeID));

            }

            switch (shareTypeID)
            {
                case "1":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_around.cshtml", model);
                case "2":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_idea.cshtml", model);
                case "3":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_story_tellin.cshtml", model);
                case "4":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_feedback.cshtml", model);
                case "5":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_launch.cshtml", model);
                case "6":
                    return PartialView("~/Views/HomeUI/FeedPartial/_feed_community.cshtml", model);
                default:
                    return PartialView();
            }
        }

        

        public PartialViewResult GetNavbar()
        {
            IEnumerable<byte> appMenuIDs = services.appMenuDetailRepo.Where(x => x.UserTypeID == UserManagerService.CurrentUser.UserTypeID).Select(x => x.ApplicationMenuID).ToList();

            var model = services.appMenuRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage && appMenuIDs.Contains(x.ID)).Select(a => new ApplicationUserMenuVM
            {
                MenuName = a.Name,
                RedirectURL = a.Url


            });

            return PartialView("~/Views/HomeUI/FeedPartial/_navbar.cshtml", model);
        }


        public ActionResult Feed()
        {



            return View();
        }
    }
}