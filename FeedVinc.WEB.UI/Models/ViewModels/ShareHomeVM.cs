using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels
{
    public class ShareHomeVM
    {
        public string ShareTypeText { get; set; }
        public string ShareTitle { get; set; }
        public string ShareLogo { get; set; }
        public string ShareURL { get; set; }
        public int MediaType { get; set; }
        public int ShareCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

    }
}