using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public interface IShareComment
    {
        NotificationShareVM NotifyComment(ShareCommentPostModel model);
    }
}
