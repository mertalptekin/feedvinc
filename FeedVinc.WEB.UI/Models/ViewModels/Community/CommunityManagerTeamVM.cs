using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community
{
    public class CommunityManagerTeamVM
    {
        public long OwnerID { get; set; }
        public CommunityMenuVM Menu { get; set; }
        public long CommunityID { get; set; }

        public IEnumerable<CommunityManagerUserVM> CommunityAdmins { get; set; }


    }
}