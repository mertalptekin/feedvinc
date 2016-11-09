using FeedVinc.WEB.UI.Areas.Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    [AuthenticationControl]
    public class AdminAppController : AdminBaseController
    {
        // GET: Admin/AdminApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}