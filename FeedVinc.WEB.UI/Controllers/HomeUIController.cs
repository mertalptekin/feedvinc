﻿using FeedVinc.Common.Services;
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
            var model = services.appUserShareRepo.Where(x => x.ShareTypeID == 1).Select(a => new ShareVM
            {
                
                ShareCount = 0,
                LikeCount = 0,
                CommentCount = 0,
                MediaTypeID = a.MediaType,
                Location = a.Location,
                PrettyDate = DateTimeService.GetPrettyDate((DateTime)a.ShareDate, LanguageService.getCurrentLanguage),
                Post = a.Content,
                PostMediaPath = a.SharePath,
                ShareTypeID = (byte)a.ShareTypeID,
                ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                PostDate = a.ShareDate

            }).OrderByDescending(x=> x.PostDate).Take(2).ToList();

            model.ForEach(a => a.User = services.appUserRepo.Where(y=> y.ID==a.User.ID).Select(z=> new UserVM {
                ID = z.ID,
                FullName = z.Name + " " + z.SurName,
                UserTypeID = z.UserTypeID,
                ProfilePhoto = z.ProfilePhoto 

            }).FirstOrDefault());

               

            return PartialView("~/Views/HomeUI/FeedPartial/_feed.cshtml", model);
        }

        public async Task<PartialViewResult> GetAroundMe(string uri)
        {

            uri = uri.Replace(":", "&");
            IEnumerable<ShareVM> model = null;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60029/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    model = await response.Content.ReadAsAsync<IEnumerable<ShareVM>>();

                    model.ToList().ForEach(a => a.PrettyDate = DateTimeService.GetPrettyDate((DateTime)a.PostDate, LanguageService.getCurrentLanguage));

                    model.ToList().ForEach(a => a.ShareTypeText = GetShareTypeTextByLanguage(a.ShareTypeID));

                }
            }

            return PartialView("~/Views/HomeUI/FeedPartial/_feed.cshtml", model);
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