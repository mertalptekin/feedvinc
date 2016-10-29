using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class ShareCommentVM
    {
        public string PrettyDate { get; set; }
        public string CommentText { get; set; }
        public long CommentUserID { get; set; }

        public ShareCommentUserVM CommentUser { get; set; }


    }
}