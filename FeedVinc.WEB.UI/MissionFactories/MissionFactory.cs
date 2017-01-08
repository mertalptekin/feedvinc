using FeedVinc.BLL.Services;
using FeedVinc.WEB.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.MissionFactories
{
    public class MissionFactory
    {
        protected UnitOfWork _services;

        public MissionFactory(UnitOfWork services)
        {
            _services = services;
        }

        public IMissionService GetInstance(MissionAssignmentType type)
        {
            IMissionService connector = null;

            switch (type)
            {
                case MissionAssignmentType.Proje_Kategorisine_Göre:
                    connector = new ProjectCategoryFactory(_services);
                    break;
                case MissionAssignmentType.Proje_Aşamasına_Göre:
                    connector = new ProjectPhaseFactory(_services);
                    break;
                case MissionAssignmentType.Yatırımı_Aşamasına_Göre:
                    connector = new ProjectInvestmentStatusFactory(_services);
                    break;
                case MissionAssignmentType.Takipçi_Sayısına_Göre:
                    connector = new ProjectFollowFactory(_services);
                    break;
                case MissionAssignmentType.FeedPuanına_Göre:
                    connector = new ProjectFeedPointFactory(_services);
                    break;
                default:
                    break;
            }

            return connector;
        }

    }
}