using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Store
{
    public class CartDetailVM
    {
        public int ID { get; set; }
        public string AppName { get; set; }
        public string CurrencyString { get; set; }
        public decimal SalesPrice { get; set; }

    }
}