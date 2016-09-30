using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        UnitOfWork services;

        public AdminBaseController()
        {
            services = new UnitOfWork();
        }
    }
}