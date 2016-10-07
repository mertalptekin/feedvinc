using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class CommunityShareVM
    {
        public long CommunityID { get; set; }
        public string CommunityName { get; set; }
        public string ProfilePhotoLogo { get; set; }
        public long OwnerID { get; set; }


    }
}