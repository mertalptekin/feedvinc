using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community.Profile
{
    public class CommunityProjectVM
    {
        public long ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectProfilePhoto { get; set; }
        public string SalesPitch { get; set; }
        public byte ProjectCategoryID { get; set; }


    }
}