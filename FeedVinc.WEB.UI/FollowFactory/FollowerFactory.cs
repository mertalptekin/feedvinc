using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.FollowFactory
{
    public class FollowerFactory : FollowerService
    {
        public FollowerFactory(UnitOfWork service) : base(service)
        {
        }

        public IFollow CreateObjectInstance(string followerType)
        {
            if (followerType == "project")
                return new ProjectFollowModel(_service);
            else if (followerType == "community")
                return new CommunityFollowModel(_service);

            return new UserFollowModel(_service);

        }
    }
}