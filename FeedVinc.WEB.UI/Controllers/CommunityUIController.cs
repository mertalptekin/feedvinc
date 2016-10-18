using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class CommunityUIController : BaseUIController
    {
        // GET: CommunityUI
        public ActionResult Profile(string communityName, string communityCode)
        {
            return View();
        }
    }
}