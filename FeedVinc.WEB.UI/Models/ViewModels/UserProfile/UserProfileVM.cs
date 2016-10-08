using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.UserProfile
{
    public class UserProfileVM
    {
        public UserVM User { get; set; }
        public IEnumerable<ShareVM> UserShares { get; set; }
        public IEnumerable<ProjectVM> UserProjects { get; set; }
    }
}