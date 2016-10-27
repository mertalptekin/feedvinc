using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Models
{
    public class ShareFeedBack:MediaShare
    {

        public string TestLink { get; set; }
        public double? VoteCount { get; set; }

    }
}