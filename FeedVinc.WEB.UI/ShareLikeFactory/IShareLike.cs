using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.ShareLikeFactory
{
    public interface IShareLike
    {
        NotificationShareVM NotifyLike(ShareLikePostModel model, List<string> notifyUserIds);
    }
}
