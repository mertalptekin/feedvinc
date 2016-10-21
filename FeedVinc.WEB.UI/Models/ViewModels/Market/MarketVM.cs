using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Market
{
    public class MarketVM
    {
        public long FeedPoint { get; set; }
        public int FollowerCount { get; set; }
        public int TeamSize { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectSlug { get; set; }
        public string SalesPicth { get; set; }
        public string InvestmentStatus { get; set; }
        public int ProjectLevel { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProjectProfilePhoto { get; set; }


    }
}