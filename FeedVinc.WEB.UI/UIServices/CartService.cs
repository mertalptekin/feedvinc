using FeedVinc.WEB.UI.Models.ViewModels.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class CartService
    {

        public List<AppCartVM> CurrentCart
        {
            get
            {
                if (HttpContext.Current.Session["Cart"]==null)
                {
                    return new List<AppCartVM>();
                }

                return (List<AppCartVM>)HttpContext.Current.Session["Cart"];
            }
        }


        public bool AddToChart(AppCartVM model)
        {

           bool isExist = CurrentCart.Any(x => x.AppID == model.AppID && x.ProjectID == model.ProjectID);


            if (!isExist)
            {
                HttpContext.Current.Session["Cart"] = CurrentCart;
                CurrentCart.Add(model);
                return true;
            }

            return false;
        }

        public void DeleteToCart(int AppID)
        {
            var data = CurrentCart.FirstOrDefault(x => x.AppID == AppID);
            CurrentCart.Remove(data);
        }
    }
}