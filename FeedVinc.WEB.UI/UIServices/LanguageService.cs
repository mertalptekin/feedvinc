using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class LanguageService
    {
        public static string getCurrentLanguage
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture.Name;
            }
        }
    }
}