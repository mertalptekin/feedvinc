using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Financial
{
    public class FinancialProjectVM
    {
        public string ProjectName { get; set; }
        public string SalesPitch { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string ProjectProfile { get; set; }
        public string ProjectSlugify { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectCategoryName { get; set; }
        public int CategoryID { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public long ID { get; set; }


    }
}