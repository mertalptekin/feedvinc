using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Models
{
    public class ShareNormal:MediaShare
    {

        public long CommentCount { get; set; }
        public long LikeCount { get; set; }

    }
}