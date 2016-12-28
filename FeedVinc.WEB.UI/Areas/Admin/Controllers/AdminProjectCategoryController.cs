using FeedVinc.DAL.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminProjectCategoryController : AdminBaseController
    {
        // GET: Admin/AdminProjectCategory
        public ActionResult Index()
        {
            List<ProjectCategory> model = services.projectCategoryRepo.ToList().ToList();

            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = services.projectCategoryRepo.FirstOrDefault(x => x.ID == id);

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectCategory model)
        {
            var data = services.projectCategoryRepo.FirstOrDefault(x => x.ID == model.ID);

            if (ModelState.IsValid)
            {
                data.CategoryName = model.CategoryName;
                data.Lang = model.Lang;
                int result = services.Commit();

                if (result > 0)
                {
                    ViewBag.Message = "Kategori Güncellendi";
                    ModelState.Clear();
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Add(ProjectCategory model)
        {
            if (ModelState.IsValid)
            {
                services.projectCategoryRepo.Add(model);
                int result = services.Commit();

                if (result > 0)
                {
                    ViewBag.Message = "Kategori Eklendi";
                    ModelState.Clear();
                    return View(model);
                }
            }

            return View(model);
        }
    }
}