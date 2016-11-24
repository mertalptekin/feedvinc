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



        public void RunHttpClient()
        {

            //client.BaseAddress = new Uri("http://feedvincapi.workstudyo.com/");
            client.BaseAddress = new Uri("http://localhost:60029/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void Application_BeginRequest()
        {

            LanguageConfig.RegisterLanguage();
            FeedPointINIT();
            
        }

        public void FeedPointINIT()
        {
            if (Application["FeedPointLastSyc"] == null)
            {
                Application["FeedPointLastSyc"] = DateTime.Now;
            }

            var syncDate = ((DateTime)Application["FeedPointLastSyc"]).AddMinutes(2);
            var _now = DateTime.Now;

            if (_now >= syncDate)
            {
                Application["FeedPointLastSyc"] = _now;
                FeedPointSync();
            };
        }

        public void FeedPointSync()
        {

            //FeedPoint Manager Devreye Girer
            //aktif olan kullanıcının proje puanını günceller

            FeedPointManager manager = new FeedPointManager();
            manager.UpdateFeedPoint();
        }
    }

}

