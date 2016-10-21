using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Models.ViewModels.Filter
{
    public class FilterVM
    {
        public IEnumerable<SelectListItem> ProjectCategory { get; set; }
        public IEnumerable<SelectListItem> Country { get; set; }
        public IEnumerable<SelectListItem> City { get; set; }

    }
}