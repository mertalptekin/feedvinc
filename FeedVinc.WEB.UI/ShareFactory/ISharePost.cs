using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.ShareFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.ShareFactory
{
    public interface ISharePost
    {
        NotificationShareVM Post(string userID,ShareBaseModel model);
    }
}
