using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AppUserUIController : Controller
    {
        // GET: AppUserUI
        public ActionResult Profile(string username)
        {
            var user = UserManagerService.CurrentUser;

            return View(user);
        }
    }
}