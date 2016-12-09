using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Search
{
    public class SearchVM
    {
        public string Name { get; set; }
        public string Slugify { get; set; }
        public string Code { get; set; }
        public long ID { get; set; }
        public string cssType { get; set; }
        public int? CategoryID { get; set; }
        public int? CountryID { get; set; }
        public int? CityID { get; set; }
        public string Content { get; set; }

        public string ProfilePath { get; set; }

        public string CityName { get; set; }
        public string CountryName { get; set; }

        public string CategoryName { get; set; }
        public long ProjectID { get; set; }


    }
}