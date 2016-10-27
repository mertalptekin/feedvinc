using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class FollowManager
    {
        IFollow _followDepedency;

        public FollowManager(IFollow followDepedency)
        {
            _followDepedency = followDepedency;
        }

        public NotificationFollowVM Follow(long follower,long followed)
        {
           return _followDepedency.Follow(follower, followed);
        }

    }
}