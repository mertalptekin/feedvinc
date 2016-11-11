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

        [HttpPost]
        [ValidateAntiForgeryToken]
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


        public ActionResult Edit(int id)
        {
            var model = services.appStoreRepo.Where(x => x.ID == id).Select(c => new AdminAppPostVM
            {
                AppIconPath = c.AppIconPath,
                AppNameEn = c.AppNameEn,
                AppNameTR = c.AppNameTR,
                Currency = c.Currency,
                IsFree = c.IsFree,
                InformationEN = c.InformationEN,
                InformationTR = c.InformationTR,
                SalesPrice = c.SalesPrice,
                ID = c.ID

            }).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminAppPostVM model)
        {
            if (model.AppIconPath != null)
            {
                if (ModelState.ContainsKey("AppFotoFile"))
                    ModelState["AppFotoFile"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                var app = services.appStoreRepo.FirstOrDefault(x => x.ID == model.ID);

                app.AppNameTR = model.AppNameTR;
                app.AppNameEn = model.AppNameEn;
                app.AppIconPath = app.AppIconPath==null ? MediaManagerService.Save(new MediaFormatDTO { Media = model.AppFotoFile, MediaType = 0 }) : app.AppIconPath;
                app.Currency = model.Currency;
                app.IsActive = true;
                app.IsFree = model.IsFree;
                app.SalesPrice = (decimal)model.SalesPrice;
                app.InformationTR = model.InformationTR;
                app.InformationEN = model.InformationEN;

                services.Commit();

                ViewBag.IsSuccess = true;
                ViewBag.Message = "Uygulama Düzenledi !";

                return View(model);
            }

            return View(model);
        }
    }
}