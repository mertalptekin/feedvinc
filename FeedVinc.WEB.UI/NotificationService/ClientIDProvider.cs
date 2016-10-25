using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.NotificationService
{
    public class ClientIDProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            return request.QueryString["userID"];
        }
    }
}