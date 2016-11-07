using FeedVinc.WEB.UI.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels
{
    public class CommentWrapper
    {
        public List<ShareCommentVM> ShareComments { get; set; }
        public long PreviousPagerCount { get; set; }

    }
}