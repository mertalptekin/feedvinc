using ETicaret.Web.UI.Models.Account;
using FeedVinc.WEB.UI.Areas.Admin.Controllers;
using FeedVinc.WEB.UI.Areas.Admin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminUserController : AdminBaseController
    {

        [HttpGet]
        public ActionResult Login()
        {
            CookieManager cookieMng = new CookieManager();
            var cookie = cookieMng.GetCookie("AdminUserCookie");

            if (cookie!=null)
            {
                return Redirect("/Admin/AdminHome/Index");
            }

            return View();
        }

        //login olmuş kişi anca logout yapabilir.
        [AuthenticationControl]
        public ActionResult LogOut()
        {
            CookieManager cookieMng = new CookieManager();
            cookieMng.DeleteCookie("AdminUserCookie");


            return Redirect("/Admin/AdminHome/Index");

        }
       
        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            var admin = services.adminUserRepo.FirstOrDefault(x => x.UserName == model.Email && x.Password == model.Password);

            if (services.adminUserRepo.Any(x => x.UserName == model.Email && x.Password == model.Password))
            {

                CookieManager cookieProvider = new CookieManager();
                cookieProvider.SetCookie("AdminUserCookie", admin.UserName, true);

                return Redirect("/Admin/AdminHome/Index");
             }

            return View();
        }
    }
}