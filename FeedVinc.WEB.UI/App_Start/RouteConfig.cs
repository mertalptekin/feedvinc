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

            #region Event

            routes.MapRoute("events", "events", new { controller = "EventUI", Action = "Index" });

            routes.MapRoute("new-event", "create-new-event", new { controller = "EventUI", action = "EventAdd" });

            #endregion


            #region Community

            routes.MapRoute("community-profile", "community-profile/{communityName}/{communityCode}", new { controller = "CommunityUI", Action = "CommunityProfile", communityName = UrlParameter.Optional, communityCode = UrlParameter.Optional });

            routes.MapRoute("community-list", "community/all", new { controller = "CommunityUI", action = "CommunityList" });

            routes.MapRoute("community-add", "community/add", new { controller = "CommunityUI", action = "CommunityAdd" });

            routes.MapRoute("community-edit", "community-edit/{communityName}/{communityCode}", new { controller = "CommunityUI", action = "CommunityEdit", communityName = UrlParameter.Optional, communityCode = UrlParameter.Optional });

            routes.MapRoute("community-manage-edit", "community-manager-edit/{communityName}/{communityCode}", new { controller = "CommunityUI", action = "CommunityManagerEdit", communityName = UrlParameter.Optional, communityCode = UrlParameter.Optional });

            #endregion


            #region Account


            routes.MapRoute("logout", "logout", new { controller = "AccountUI", action = "LogOut" });

            routes.MapRoute("forget-password", "forget-password", new { controller = "AccountUI", action = "ForgetPassword" });

            routes.MapRoute("activate", "activate-account/{activationCode}", new { controller = "AccountUI", action = "UserActivation", activationCode = UrlParameter.Optional });

            #endregion

            #region Navigation

            routes.MapRoute("my-project", "my-projects", new { controller = "ProjectUI", action = "Me" });

            routes.MapRoute("market", "market", new { controller = "MarketUI", action = "index" });

            routes.MapRoute("adds", "adds", new { controller = "AddsUI", action = "index" });

            routes.MapRoute("metrics", "metric", new { controller = "MetricUI", action = "index" });

            routes.MapRoute("speed-networkin", "speed-networkin", new { controller = "SpeedNetworkinUI", action = "index" });

            #endregion

            #region UserProfile

            routes.MapRoute("user-edit", "profile/edit", new { controller = "AppUserUI", Action = "Edit", username = UrlParameter.Optional });

            routes.MapRoute("user-profile", "profile/{username}/{userCode}", new { controller = "AppUserUI", action = "Profile", username = UrlParameter.Optional });

            routes.MapRoute("email-settings", "email-settings", new { controller = "AppUserUI", action = "EmailSettings" });

            routes.MapRoute("account-settings", "account-settings", new { controller = "AppUserUI", action = "AccountSettings" });

            #endregion

            #region Home

            routes.MapRoute("home", "home", new { controller = "HomeUI", action = "Feed" });

            routes.MapRoute("index", "index", new { controller = "HomeUI", action = "Index" });

            #endregion

            #region Project

            routes.MapRoute("proje-profile", "project-profile/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectProfile", projectname = UrlParameter.Optional,projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-add", "project/add", new { controller = "ProjectUI", Action = "ProjectAdd" });

            routes.MapRoute("proje-edit", "project-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-team-edit", "project-team-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectTeamEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-photo-edit", "project-photo-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectPhotoEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-video-edit", "project-video-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectVideoEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });


            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomeUI", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
