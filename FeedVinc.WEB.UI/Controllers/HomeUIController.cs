using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class HomeUIController : BaseUIController
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Feed()
        {
            return View();
        }
    }
}