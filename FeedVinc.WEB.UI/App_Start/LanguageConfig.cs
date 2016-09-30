using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace FeedVinc.WEB.UI.App_Start
{
    public class LanguageConfig
    {
        public static void RegisterLanguage()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["LanguageInfo"];

            if (cookie != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

                cookie = new HttpCookie("LanguageInfo");
                cookie.Value = "en-US";
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
    }
}