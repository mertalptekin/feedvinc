using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Invested
{
    public class InvestedNewsVM
    {
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public string ProjectCode { get; set; }
        public string SalesPitch { get; set; }
        public string PrettyDate { get; set; }
        public long LikeCount { get; set; }
        public long ShareCount { get; set; }
        public long CommentCount { get; set; }
        public string ProfilePhoto { get; set; }

        public long ShareID { get; set; }
        public bool LikedCurrentUser { get; set; }
        public long UserID { get; set; }



    }
}