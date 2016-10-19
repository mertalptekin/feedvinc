using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community.Profile
{
    public class CommunityMemberVM
    {
        public string MemberProfilePhoto { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public List<string> MemberProjectNames { get; set; }
        public bool IsAdmin { get; set; }
        public string MemberSlugify { get; set; }
        public string MemberCode { get; set; }
        public long MemberID { get; set; }
    }
}