using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Areas.Admin.Models;
using FeedVinc.WEB.UI.Areas.Admin.Services;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    [AuthenticationControl]
    public class AdminAppController : AdminBaseController
    {
        // GET: Admin/AdminApp
        public ActionResult Index()
        {
            var model = services.appStoreRepo.ToList().Select(a => new AdminAppListVM
            {
                AppNameEn = a.AppNameEn,
                AppNameTR = a.AppNameTR,
                InformationEN = a.InformationEN,
                InformationTR = a.InformationTR,
                SalesPrice = a.SalesPrice + " " + a.Currency,
                IsFree = a.IsFree,
                ID = a.ID,
                IsActive = a.IsActive

            }).ToList();

            return View(model);
        }

        
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Add(AdminAppPostVM model)
        {
            if (ModelState.IsValid)
            {
                var app = new ApplicationStore
                {
                    AppNameTR = model.AppNameTR,
                    AppNameEn = model.AppNameEn,
                    AppIconPath = MediaManagerService.Save(new MediaFormatDTO { Media = model.AppFotoFile, MediaType = 0 }),
                    Currency = model.Currency,
                    IsActive = true,
                    IsFree = model.IsFree,
                    SalesPrice = (decimal)model.SalesPrice,
                    InformationTR = model.InformationTR,
                    InformationEN = model.InformationEN
                };

                services.appStoreRepo.Add(app);
                services.Commit();


                ViewBag.IsSuccess = true;
                ViewBag.Message = "Uygulama Oluşturuldu !";

                return View(model);
            }

            return View(model);
        }
    }
}