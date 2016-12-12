using FeedVinc.WEB.UI.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Trend
{
    public class TrendWrapperVM
    {
        public List<TrendHomeVM> HashTags { get; set; }
        public List<TrendPostVM> TrendPosts { get; set; }

    }
}