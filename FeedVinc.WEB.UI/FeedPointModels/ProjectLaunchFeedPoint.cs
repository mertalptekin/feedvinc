using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public class ProjectLaunchFeedPoint : FeedPoint
    {
        public ProjectLaunchFeedPoint(long projectID) : base(projectID)
        {
        }

        public override void GetFeedPoint()
        {
            _currentFeedPoint = _currentFeedPoint + ((_services.projectLaunchRepo.Count(x => x.ProjectID == _currentProjectID)) * 100);
        }
    }
}