using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.ShareCommentFactory
{
    public class ShareCommentPostModel
    {
        public long CommentShareID { get; set; }
        public string CommentText { get; set; }
        public long CommentUserID { get; set; }
        public int ShareTypeID { get; set; }
        public string ShareType { get; set; }


    }
}