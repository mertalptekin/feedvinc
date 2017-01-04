using FeedVinc.WEB.UI.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectProfileWrapperVM
    {
        public List<ProjectProfileTeamVM> ProjectTeams { get; set; }
        public List<ProjectProfilePhotoVM> ProjectPhotos { get; set; }
        public List<ProjectProfileVideoVM> ProjectVideos { get; set; }
        public ProjectProfileVM ProjectProfile { get; set; }
        public List<ShareVM> ProjectFeeds { get; set; }
        public List<ProjectStoreVM> ProjectStores { get; set; }

    }
}