using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Store
{
    public class StoreVM
    {
        public string AppLogo { get; set; }
        public string AppName { get; set; }
        public string AppDesc { get; set; }
        public int AppID { get; set; }
        public bool IsFree { get; set; }
        public string CurrencyString { get; set; }
        public decimal Currency { get; set; }

        public bool isPurchased { get; set; }

    }
}