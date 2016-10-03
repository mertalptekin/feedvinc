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
            if (UserManagerService.CurrentUser!=null)
            {
                return Redirect("/home");
            }

            return View();
        }

        
        public ActionResult Feed()
        {
            return View();
        }
    }
}