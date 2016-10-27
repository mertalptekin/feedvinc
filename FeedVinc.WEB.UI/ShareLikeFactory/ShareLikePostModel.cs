using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.ShareLikeFactory
{
    public class ShareLikePostModel
    {
        public long LikedId { get; set; }
        public long UserId { get; set; }
        public int ShareTypeID { get; set; }
        public string ShareType { get; set; }
        public long PostShareID { get; set; }

    }
}