using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Models
{
    public class ShareLaunch:MediaShare
    {

        public string AndroidLink { get; set; }
        public string IPhoneLink { get; set; }
        public string WebLink { get; set; }
        public string Version { get; set; }

    }
}