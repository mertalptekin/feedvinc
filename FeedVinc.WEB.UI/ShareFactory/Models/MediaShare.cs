using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Models
{
    public class MediaShare: ShareBaseModel
    {
      
        public string MediaPath { get; set; }
        public int? MediaTypeID { get; set; }

    }
}