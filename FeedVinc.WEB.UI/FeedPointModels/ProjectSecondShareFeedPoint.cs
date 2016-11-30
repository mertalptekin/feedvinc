using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public class ProjectSecondShareFeedPoint : FeedPoint
    {
        public ProjectSecondShareFeedPoint(long projectID) : base(projectID)
        {
        }

        public override void GetFeedPoint()
        {
            //_currentFeedPoint = _currentFeedPoint + ((_services.appUserShareRepo.Count(x => x.ProjectID == _currentProjectID)) * 20);
        }
    }
}