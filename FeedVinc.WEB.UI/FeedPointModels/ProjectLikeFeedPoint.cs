using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public class ProjectLikeFeedPoint : FeedPoint
    {
        public ProjectLikeFeedPoint(long projectID) : base(projectID)
        {
        }

        public override void GetFeedPoint()
        {
            _currentFeedPoint = ((_services.projectLikeRepo.Count(x => x.ProjectID == _currentProjectID)) * 2);
        }
    }
}