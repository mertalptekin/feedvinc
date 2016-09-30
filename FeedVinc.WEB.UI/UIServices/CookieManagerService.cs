using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class CookieManagerService
    {
        

        public static HttpCookie GetCookie(string name)
        {
            return HttpContext.Current.Request.Cookies[name];
        }

        public static void DeleteCookie(string name)
        {
            HttpContext.Current.Response.Cookies[name].Expires = DateTime.Now.AddDays(-30);
        }

        public static void SetCookie(string name,string value)
        {
            HttpCookie AppUserCookie = new HttpCookie("AppUser");
            AppUserCookie.Expires = DateTime.Now.AddDays(30);
            AppUserCookie.Value = value;
            HttpContext.Current.Response.Cookies.Add(AppUserCookie);
        }
    }
}