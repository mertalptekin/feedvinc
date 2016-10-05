using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class HomeUIController : BaseUIController
    {

        [OverrideActionFilters]
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["ApplicationUser"]!=null)
            {
                return Redirect("/home");
            }

            return View();
        }


        public PartialViewResult GetNavbar()
        {
            IEnumerable<byte> appMenuIDs = services.appMenuDetailRepo.Where(x => x.UserTypeID == UserManagerService.CurrentUser.UserTypeID).Select(x => x.ApplicationMenuID).ToList();

            var model = services.appMenuRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage && appMenuIDs.Contains(x.ID)).Select(a => new ApplicationUserMenuVM
            {
                MenuName = a.Name,
                RedirectURL = a.Url

            });

            return PartialView("~/Views/HomeUI/FeedPartial/_navbar.cshtml",model);
        }

        
        public ActionResult Feed()
        {

            

            return View();
        }
    }
}