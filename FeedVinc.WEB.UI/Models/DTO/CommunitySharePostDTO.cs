using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class CommunitySharePostDTO
    {
        public string CommunityName { get; set; }
        public string CommunitySlugify { get; set; }
        public string CommunityProfilePath { get; set; }
        public long CommunityID { get; set; }
    }
}