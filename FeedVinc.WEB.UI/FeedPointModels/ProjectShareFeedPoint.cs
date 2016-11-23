using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public class ProjectShareFeedPoint : FeedPoint
    {
        public ProjectShareFeedPoint(long projectID) : base(projectID)
        {
        }

        public override void GetFeedPoint()
        {
            _currentFeedPoint = _currentFeedPoint + ((_services.projectShareRepo.Count(x => x.ProjectID == _currentProjectID)) * 15 ); 
        }
    }
}