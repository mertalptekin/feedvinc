﻿using FeedVinc.WEB.UI.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FeedVinc.WEB.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
       public static HttpClient client = new HttpClient();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

     

        protected void Application_BeginRequest()
        {
            LanguageConfig.RegisterLanguage();
        }
    }
}
