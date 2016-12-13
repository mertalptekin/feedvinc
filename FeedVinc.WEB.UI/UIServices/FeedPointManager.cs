using FeedVinc.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.WEB.UI.FeedPointModels;

namespace FeedVinc.WEB.UI.UIServices
{
    public class FeedPointManager
    {
        private UnitOfWork _services;
        private List<long> _currentUserProjectIDs;

        public FeedPointManager()
        {
            _services = new UnitOfWork();
            _currentUserProjectIDs = _services.projectRepo.Where(x => x.UserID == UserManagerService.CurrentUser.ID).Select(c => c.ID).ToList();
        }


        public void UpdateFeedPoint()
        {
            foreach (var item in _currentUserProjectIDs)
            {

                FeedPoint obj1 = new ProjectShareFeedPoint(item);
                obj1.GetFeedPoint();
                FeedPoint obj2 = new ProjectLikeFeedPoint(item);
                obj2.GetFeedPoint();
                FeedPoint obj3 = new ProjectFollowFeedPoint(item);
                obj3.GetFeedPoint();
                FeedPoint obj4 = new ProjectCommentFeedPoint(item);
                obj4.GetFeedPoint();
                FeedPoint obj5 = new ProjectSecondShareFeedPoint(item);
                obj5.GetFeedPoint();


                var _totalPoint = obj1.CurrentPoint + obj2.CurrentPoint + obj3.CurrentPoint + obj4.CurrentPoint + obj5.CurrentPoint + 100;

                var entity = _services.projectRepo.FirstOrDefault(x => x.ID == item);
                entity.FeedPoint = entity.FeedPoint + _totalPoint;
                _services.Commit();

            }
        }
    }
}