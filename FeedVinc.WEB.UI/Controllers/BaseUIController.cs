using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void Dispose(bool disposing)
        {
            services.Dispose(disposing);
        }
    }
}