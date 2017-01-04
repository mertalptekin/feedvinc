using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Store;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class StoreUIController : BaseUIController
    {
        // GET: Store
        public ActionResult Index()
        {

            var currentUserID = UserManagerService.CurrentUser.ID;
            var lang = LanguageService.getCurrentLanguage;

            var projects = services.projectRepo.Where(x => x.UserID == currentUserID).Select(a => new ProjectSelectionVM
            {
                ProjectID = a.ID,
                ProjectName = a.ProjectName,
                ProjectLogo = a.ProjectProfileLogo

            }).ToList();

            var apps = services.appStoreRepo.Where(x=> x.UserTypeID ==_currentUser.UserTypeID).Select(a => new StoreVM {

                AppName = lang == "tr-TR" ? a.AppNameTR : a.AppNameEn,
                AppDesc = lang == "tr-TR" ? a.InformationTR : a.InformationEN,
                AppLogo = a.AppIconPath,
                AppID = a.ID,
                CurrencyString = a.SalesPrice + " "  + a.Currency,
                Currency = a.SalesPrice,
                IsFree = a.IsFree,

            }).ToList();

            var model = new StoreWrapper();
            model.Apps = apps;
            model.Projects = projects;

            return View(model);
        }


        public ActionResult CartItem()
        {
            CartService cs = new CartService();
            string lang = LanguageService.getCurrentLanguage;
            var appIDs = cs.CurrentCart.Select(a => a.AppID);

            CartPageVM model = new CartPageVM();
            model.CartItems = services.appStoreRepo.Where(x => appIDs.Contains(x.ID)).Select(a => new CartDetailVM
            {
                ID = a.ID,
                AppName = lang == "tr-TR" ? a.AppNameTR : a.AppNameEn,
                CurrencyString = a.SalesPrice + a.Currency,
                SalesPrice = a.SalesPrice

            }).ToList();
            model.TotalPrice = model.CartItems.Sum(a => a.SalesPrice) + "$";


            return View(model);

        }


        public ActionResult DeleteToCart(int id)
        {
            CartService cs = new CartService();
            cs.DeleteToCart(id);

            return Redirect("/cart-detail");
        }


        public ActionResult PurchaseItem()
        {
            CartService cs = new CartService();
            string lang = LanguageService.getCurrentLanguage;

            var appIDs = cs.CurrentCart.Select(a => a.AppID);
    
            PurchaseScreenVM model = new PurchaseScreenVM();
            model.Customer = new CustomerInfoVM();
            model.CartItems = services.appStoreRepo.Where(x => appIDs.Contains(x.ID)).Select(a => new CartDetailVM
            {
                AppName = lang == "tr-TR" ? a.AppNameTR:a.AppNameEn,
                CurrencyString = a.SalesPrice + a.Currency,
                SalesPrice = a.SalesPrice

            }).ToList();
            model.TotalPrice = model.CartItems.Sum(a => a.SalesPrice) + "$";


            return View(model);
        }

        [HttpPost]
        public JsonResult AddToCart(AppCartVM model)
        {
            CartService cartService = new CartService();
            var response = cartService.AddToChart(model);

             response = services.projectAppRepo.Any(x => x.AppStoreID == model.AppID && x.ProjectID == model.ProjectID)== true ? false:true;

            return Json(response == false ? SiteLanguage.AppIsExist : SiteLanguage.AppAddedToCart);
        }

        [HttpPost]
        public ActionResult Pay()
        {
            CartService cart = new CartService();
            var apps =  cart.CurrentCart;


            foreach (var item in apps)
            {
                ProjectApp app = new ProjectApp();
                app.ActivationDate = DateTime.Now;
                app.ExpireDate = DateTime.Now.AddYears(1);
                app.AppStoreID = (byte)item.AppID;
                app.ProjectID = item.ProjectID;

                services.projectAppRepo.Add(app);
                services.Commit();
            }

            return Redirect("/order-confirm");
        }

        public ActionResult OrderConfirm()
        {
            return View();
        }

        public JsonResult GetApp()
        {
            CartService service = new CartService();

            if (service.CurrentCart.Count==0)
            {
                return Json(new { response = SiteLanguage.AppIsNotExist, IsValid = false },JsonRequestBehavior.AllowGet);
            }

            return Json(new { response = SiteLanguage.RedirectToPurchaseScreen, IsValid = true },JsonRequestBehavior.AllowGet);

        }
    }
}