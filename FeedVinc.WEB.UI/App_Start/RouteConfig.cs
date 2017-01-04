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


            routes.MapRoute("share-notification", "post", new { controller = "NotificationUI", action = "Post" });

            routes.MapRoute("follow-notification", "follow-notify", new { controller = "NotificationUI", action = "Follow" });

            #region Market

            routes.MapRoute("market-filter", "market/filter/{uri}", new { controller = "MarketUI", action = "Filter",uri=UrlParameter.Optional });

            #endregion

            #region InvestedLetters

            routes.MapRoute("investment-letters", "investment-letter/{id}", new { controller = "InvestmentLetter", action = "InvestmentLetterDetail", id = UrlParameter.Optional });

            routes.MapRoute("show-investment-letters", "investment-letters", new { controller = "InvestmentLetter", action = "ShowInvestmentLetter" });

            routes.MapRoute("investment-letter-app", "investment-letter/{projectname}/{projectCode}/{appid}", new { controller = "InvestmentLetter", action = "GetInvestmentLetter", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional, appid = UrlParameter.Optional });

            #endregion

            #region InvestedNews

            routes.MapRoute("investedNews", "invested-news", new { controller = "InvestmentNews", action = "Index" });

            #endregion

            #region Trend

            routes.MapRoute("trending", "trending", new { controller = "TrendUI", action = "Index",tag=UrlParameter.Optional });


            #endregion

            #region Adds

            routes.MapRoute("new-ads", "create-new-ads", new { controller = "AdvertisementUI", action = "AddvertisementAdd" });

            #endregion

            #region Event

            routes.MapRoute("events", "events", new { controller = "EventUI", Action = "Index" });

            routes.MapRoute("new-event", "create-new-event", new { controller = "EventUI", action = "EventAdd" });

            #endregion


            #region Community

            routes.MapRoute("community-profile", "community-profile/{communityName}/{communityCode}", new { controller = "CommunityUI", Action = "CommunityProfile" });

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

            routes.MapRoute("adds", "ads", new { controller = "AdvertisementUI", action = "index" });

            routes.MapRoute("metrics", "metric", new { controller = "MetricUI", action = "index" });

            routes.MapRoute("speed-networkin", "speed-networkin", new { controller = "SpeedNetworkinUI", action = "index" });

            #endregion

            #region UserProfile

            routes.MapRoute("user-edit", "profile/edit", new { controller = "AppUserUI", Action = "Edit", username = UrlParameter.Optional });

            routes.MapRoute("user-profile", "profile/{username}/{userCode}", new { controller = "AppUserUI", action = "Profile", username = UrlParameter.Optional });

            routes.MapRoute("email-settings", "email-settings", new { controller = "AppUserUI", action = "EmailSettings" });

            routes.MapRoute("account-settings", "account-settings", new { controller = "AppUserUI", action = "AccountSettings" });

            routes.MapRoute("message-privacy", "message-privacy", new { controller = "AppUserUI", action = "MessagePrivacy" });

           
            #endregion

            #region Home

            routes.MapRoute("home", "home", new { controller = "HomeUI", action = "Feed" });

            routes.MapRoute("index", "index", new { controller = "HomeUI", action = "Index" });

            #endregion


            #region SpeedNetworking

            routes.MapRoute("speed-net-investor", "speed-networking", new { controller = "SpeedNetworkingUI", Action = "InvestorSpeedNetworking" });


            routes.MapRoute("speed-net-info", "speed-networking/{projectname}/{projectCode}/{appid}", new { controller = "SpeedNetworkingUI", Action = "Home", projectname = UrlParameter.Optional, appid = UrlParameter.Optional });

            routes.MapRoute("speed-net-video", "speed-networking/{projectname}/{projectCode}/select-video", new { controller = "SpeedNetworkingUI", Action = "Index", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("speed-net-deploy", "speed-networking/{projectname}/{projectCode}/deployment", new { controller = "SpeedNetworkingUI", Action = "Deployment", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            #endregion


            #region Store

            routes.MapRoute("store", "store", new { controller = "StoreUI", Action = "Index", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("purchase", "purchase-screen", new { controller = "StoreUI", Action = "PurchaseItem" });


            routes.MapRoute("cart", "cart-detail", new { controller = "StoreUI", Action = "CartItem" });

            routes.MapRoute("order-confirm", "order-confirm", new { controller = "StoreUI", Action = "OrderConfirm" });

            #endregion


            #region Project

            routes.MapRoute("proje-profile", "project-profile/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectProfile", projectname = UrlParameter.Optional,projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-add", "project/add", new { controller = "ProjectUI", Action = "ProjectAdd" });

            routes.MapRoute("proje-edit", "project-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-team-edit", "project-team-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectTeamEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-photo-edit", "project-photo-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectPhotoEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("proje-video-edit", "project-video-edit/{projectname}/{projectCode}", new { controller = "ProjectUI", Action = "ProjectVideoEdit", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("project-task-home", "project-task/{projectname}/{projectCode}", new { controller = "ProjectTaskUI", Action = "Home" });

            routes.MapRoute("project-task-step", "project-task-step/{projectname}/{projectCode}", new { controller = "ProjectTaskUI", Action = "Index" , projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional });

            routes.MapRoute("project-task-detail", "project-task-detail/{projectname}/{projectCode}/{projectTaskID}", new { controller = "ProjectTaskUI", Action = "Detail", projectname = UrlParameter.Optional, projectCode = UrlParameter.Optional , projectTaskID = UrlParameter.Optional });

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomeUI", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
