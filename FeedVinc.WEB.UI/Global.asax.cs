using FeedVinc.WEB.UI.App_Start;
using FeedVinc.WEB.UI.UIServices;
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
            RunHttpClient();
        }

        protected void Session_Start()
        {
            Session["FeedPointLastSyc"] = DateTime.Now;
        }

        public void RunHttpClient()
        {

            client.BaseAddress = new Uri("http://feedvincapi.workstudyo.com/");
            //client.BaseAddress = new Uri("http://localhost:60029/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void Application_BeginRequest()
        {
            LanguageConfig.RegisterLanguage();
            FeedPointSync();
        }


        public void FeedPointSync()
        {
            var syncDate = ((DateTime)Session["FeedPointLastSyc"]).AddHours(5);
            var _now = DateTime.Now;

            if (_now>=syncDate)
            {
                Session["FeedPointLastSyc"] = _now;
                //FeedPoint Manager Devreye Girer
                //aktif olan kullanıcının proje puanını günceller

                FeedPointManager manager = new FeedPointManager();
                manager.UpdateFeedPoint();
            }
        }

    }
}
