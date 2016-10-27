using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.ShareFactory.Models;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class ShareBaseFactory:ShareServiceModel
    {
        public ShareBaseFactory(UnitOfWork service) : base(service)
        {
        }

        public IShare GetObjectInstance(int shareType)
        {
            IShare model = null;

            switch ((int)shareType)
            {
                case (int)ShareType.AroundMe:
                    model = new UserShareFactory(_service);
                    break;
                case (int)ShareType.Idea:
                    model = new IdeaShareFactory(_service);
                    break;
                case (int)ShareType.StoryTelling:
                    model = new ProjectShareFactory(_service);
                    break;
                case (int)ShareType.FeedBack:
                    model = new FeedBackShareFactory(_service);
                    break;
                case (int)ShareType.Launch:
                    model = new LaunchShareFactory(_service);
                    break;
                case (int)ShareType.Community:
                    model = new CommunityShareFactory(_service);
                    break;
            }

            return model;
        }
    }
}