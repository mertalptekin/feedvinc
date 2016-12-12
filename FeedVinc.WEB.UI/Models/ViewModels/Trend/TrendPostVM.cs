using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Trend
{
    public class TrendPostVM
    {
        public TrendUserVM User { get; set; }
        public string Content { get; set; }

        public string PrettyDate { get; set; }

        public string Type { get; set; }
        public long LikeCount { get; set; }
        public long CommentCount { get; set; }
        public long ShareCount { get; set; }

        public long ShareID { get; set; }

        public long UserID { get; set; }

        public bool LikedCurrentUser { get; set; }



    }
}