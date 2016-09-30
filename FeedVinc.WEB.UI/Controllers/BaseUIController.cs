using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class BaseUIController : Controller
    {
        protected UnitOfWork services;

        public BaseUIController()
        {
            services = new UnitOfWork();
        }

        public ActionResult ChangeLanguage(string language="en-US")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            HttpCookie cookie = new HttpCookie("LanguageInfo");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            return Redirect("/home");
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose(disposing);
        }
    }
}