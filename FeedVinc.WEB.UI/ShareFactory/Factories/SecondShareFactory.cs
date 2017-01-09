using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Notification;
using FeedVinc.WEB.UI.ShareFactory.Models;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.Models.ViewModels.Home;

namespace FeedVinc.WEB.UI.ShareFactory.Factories
{
    public class SecondShareFactory: ShareServiceModel
    {
        UnitOfWork _service;

        public SecondShareFactory(UnitOfWork service):base(service)
        {
            _service = service;
        }

        public ISharePost GetObjectInstance(int shareType)
        {
            ISharePost model = null;

            switch ((int)shareType)
            {
                case (int)ShareType.AroundMe:
                    model = new AppUserSecondShareFactory(_service);
                    break;
                case (int)ShareType.Idea:
                    model = new IdeaSecondShareFactory(_service);
                    break;
                case (int)ShareType.StoryTelling:
                    model = new ProjectSecondShareFactory(_service);
                    break;
                case (int)ShareType.FeedBack:
                    model = new FeedBackSecondShareFactory(_service);
                    break;
                case (int)ShareType.Launch:
                    model = new LaunchSecondShareFactory(_service);
                    break;
                case (int)ShareType.Community:
                    model = new CommunitySecondShareFactory(_service);
                    break;
                case (int)ShareType.InvestmentNews:
                    model = new InvestmentNewsSecondShareFactory(_service);
                    break;
                default:
                    break;
            }

            return model;
        }


    }
}