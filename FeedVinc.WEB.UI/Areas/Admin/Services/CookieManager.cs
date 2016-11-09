
using FeedVinc.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Services
{
    public class CookieManager : IAuthenticate
    {
        public void DeleteCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName];
            cookie.Expires = DateTime.Now.AddDays(-30);
        }

        public HttpCookie GetCookie(string cookieName)
        {
            var cookie = HttpContext.Current.Request.Cookies[cookieName];
            

            return cookie;
        }

        public void SetCookie(string cookieName, string cookieValue, bool ispersistant)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Expires = ispersistant == true ? DateTime.Now.AddMonths(1) : DateTime.Now.AddDays(1);
            cookie.Value = cookieValue.Encrypte();
            HttpContext.Current.Response.Cookies.Add(cookie);
 
        }
    }
}