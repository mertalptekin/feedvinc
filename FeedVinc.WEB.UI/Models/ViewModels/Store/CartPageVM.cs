using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Store
{
    public class CartPageVM
    {
        public List<CartDetailVM> CartItems { get; set; }
        public string TotalPrice { get; set; }

    }
}