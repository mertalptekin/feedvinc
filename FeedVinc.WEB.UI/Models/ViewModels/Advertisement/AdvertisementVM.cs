using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Models.ViewModels.Advertisement
{
    public class AdvertisementVM
    {
        public int AddsID { get; set; }
        public AdvertisementProjectVM ProjectAdds { get; set; }
        public string Description { get; set; }
        public string DeadLine { get; set; }
        public DateTime? PostDate { get; set; }
        public IEnumerable<SelectListItem> AdvertisementCategoryList { get; set; }
        public long ProjectID { get; set; }

    }
}