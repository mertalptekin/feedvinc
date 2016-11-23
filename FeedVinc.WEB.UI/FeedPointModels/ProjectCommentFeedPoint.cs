using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.FeedPointModels
{
    public class ProjectCommentFeedPoint : FeedPoint
    {
        public ProjectCommentFeedPoint(long projectID) : base(projectID)
        {
        }

        public override void GetFeedPoint()
        {
            var projectShareIDs = _services.projectShareRepo.Where(a => a.ProjectID == _currentProjectID).Select(c => c.ID);

            _currentFeedPoint = _currentFeedPoint + ((_services.projectShareCommentRepo.Count(x => projectShareIDs.Contains(_currentProjectID)) * 15));
        }
    }
}