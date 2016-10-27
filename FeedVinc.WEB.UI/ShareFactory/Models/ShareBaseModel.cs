using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeedVinc.BLL.Services;

namespace FeedVinc.WEB.UI.ShareFactory.Models
{
    public enum ShareType
    {
        AroundMe = 1,
        Idea = 2,
        StoryTelling = 3,
        FeedBack = 4,
        Launch = 5,
        Community=6
    }


    public class ShareBaseModel
    {
        public string Post { get; set; }
        public string PostedBy { get; set; }
        public int ShareTypeID { get; set; }
        public long PostID { get; set; }
        public int ShareCount { get; set; }
        public string ShareTypeText { get; set; }
        public string PrettyDate { get; set; }
        public long OwnerID { get; set; }
        public string ShareProfileLink { get; set; }
        public string ShareProfilePhoto { get; set; }


    }
}