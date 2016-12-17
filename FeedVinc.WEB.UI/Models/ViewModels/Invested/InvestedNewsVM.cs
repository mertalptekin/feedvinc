using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Invested
{
    public class InvestedNewsVM
    {
        public Project.ProjectVM Project { get; set; }
        public long LikeCount { get; set; }
        public long ShareCount { get; set; }
        public long CommentCount { get; set; }
        public long ProjectID { get; set; }
        public string PrettyDate { get; set; }

        public long ShareID { get; set; }
        public bool LikedCurrentUser { get; set; }

    }
}