using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FeedVinc.WEB.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("logout", "logout", new { controller = "AccountUI", action = "LogOut" });

            routes.MapRoute("my-project", "my-project", new { controller = "ProjectUI", action = "me" });

            routes.MapRoute("market", "market", new { controller = "MarketUI", action = "index" });

            routes.MapRoute("adds", "adds", new { controller = "AddsUI", action = "index" });

            routes.MapRoute("metrics", "metric", new { controller = "MetricUI", action = "index" });

            routes.MapRoute("speed-networkin", "speed-networkin", new { controller = "SpeedNetworkinUI", action = "index" });

            routes.MapRoute("forget-password", "forget-password", new { controller = "AccountUI", action = "ForgetPassword" });

            routes.MapRoute("user-profile", "profile/{username}", new { controller = "AppUserUI", action = "Profile", username = UrlParameter.Optional }); 

            routes.MapRoute("activate", "activate-account/{activationCode}", new { controller = "AccountUI", action = "UserActivation", activationCode=UrlParameter.Optional });

            routes.MapRoute("home", "home", new { controller = "HomeUI", action = "Feed" });

            routes.MapRoute("index", "index", new { controller = "HomeUI", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomeUI", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
