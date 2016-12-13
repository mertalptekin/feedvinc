using FeedVinc.WEB.UI.UIServices;
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

            var shareTotalCount = _services.projectShareRepo.Where(x => x.ProjectID == _currentProjectID).GroupBy(a => a.ID).Select(a => a.Sum(c => c.ShareCount)).FirstOrDefault();

            _currentFeedPoint = shareTotalCount * 20;
        }
    }
}