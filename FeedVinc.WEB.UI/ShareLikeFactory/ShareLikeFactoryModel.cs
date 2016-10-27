using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.ShareLikeFactory
{
    public class ShareLikeFactoryModel
    {
        protected UnitOfWork _service;

        public ShareLikeFactoryModel(UnitOfWork service)
        {
            _service = service;
        }

        public IShareLike CreateObjectInstance(string shareType)
        {
            IShareLike model = null;

            switch (shareType)
            {
                case "user":
                    model = new UserShareLikeModel(_service);
                    break;
                case "project":
                    model = new ProjectShareLikeModel(_service);
                    break;
                case "community":
                    model = new CommunityShareLikeModel(_service);
                    break;
                case "idea":
                    model = new IdeaShareLikeModel(_service);
                    break;
            }

            return model;
        }
    }
}