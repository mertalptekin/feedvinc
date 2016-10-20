using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels
{
    public class CommunityAdminAddVM
    {
        public long CommunityID { get; set; }
        public long OwnerID { get; set; }
        public string AddMemberEmail { get; set; }
    }
}