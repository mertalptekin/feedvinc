using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AccountController : Controller
    {

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            return View();
        }


        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordVM modal)
        {
            return View();
        }

        public ActionResult LogOut()
        {
            return View();
        }
    }
}