using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public class ShareCommentFactoryModel
    {
       protected UnitOfWork _service;

        public ShareCommentFactoryModel(UnitOfWork service)
        {
            _service = service;
        }

        public IShareComment CreateObjectInstance(string shareType)
        {
            IShareComment model = null;

            switch (shareType)
            {
                case "user":
                    model = new UserShareCommentModel(_service);
                    break;
                case "project":
                    model = new ProjectShareCommentModel(_service);
                    break;
                case "community":
                    model = new CommunityShareCommentModel(_service);
                    break;
                case "idea":
                    model = new IdeaShareCommentModel(_service);
                    break;
            }

            return model;
        }

    }
}