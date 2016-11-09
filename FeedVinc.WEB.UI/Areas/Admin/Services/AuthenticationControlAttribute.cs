using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Services
{
    //ActionFilterAttribute bir actiona girmeden önce filtreleme yapar
    public class AuthenticationControlAttribute:ActionFilterAttribute
    {
        //actiona istek gittiği anda buradaki method çalışıcak
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            CookieManager manager = new CookieManager();
            var cookie = manager.GetCookie("AdminUserCookie");

            if (cookie==null)
            {
                filterContext.Result = new RedirectResult("/Admin/AdminUser/Login");
            }

        }
    }
}