using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class UserSharePostDTO
    {
        public string UserProfileName { get; set; }
        public string UserProfileSlugify { get; set; }
        public string ProfilePath { get; set; }
        public long UserID { get; set; }
        public string UserCode { get; set; }

    }
}