using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using FeedVinc.WEB.UI.NotificationService;

[assembly: OwinStartup(typeof(FeedVinc.WEB.UI.App_Start.Startup))]

namespace FeedVinc.WEB.UI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new ClientIDProvider());
        }
    }
}
