using FeedVinc.WEB.UI.Models.ViewModels.Store;
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

            var userAppsIDs = new List<long>();

            foreach (var item in projects)
            {
                var data = services.projectAppRepo.FirstOrDefault(x => x.ProjectID == item.ProjectID);

                if (data!=null)
                {
                    userAppsIDs.Add(data.AppStoreID);
                }
                
            }

            var apps = services.appStoreRepo.ToList().Select(a => new StoreVM {

                AppName = lang == "tr-TR" ? a.AppNameTR : a.AppNameEn,
                AppDesc = lang == "tr-TR" ? a.InformationTR : a.InformationEN,
                AppLogo = a.AppIconPath,
                AppID = a.ID,
                CurrencyString = a.SalesPrice +  a.Currency,
                Currency = a.SalesPrice,
                IsFree = a.IsFree,
                isPurchased = userAppsIDs.Contains(a.ID) == true ? true : false

            }).ToList();


            var model = new StoreWrapper();
            model.Apps = apps;
            model.Projects = projects;

            return View(model);
        }
    }
}