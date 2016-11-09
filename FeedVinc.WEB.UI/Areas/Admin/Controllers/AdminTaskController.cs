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
    public class AdminTaskController : AdminBaseController
    {
        // GET: Admin/AdminTask
        public ActionResult Index()
        {
            var model = services.projectTaskRepo
            .ToList()
            .Select(a => new AdminProjectTaskVM
            {
                TaskNameTR = a.NameTR,
                TaskDescriptionTR = a.DescriptionTR,
                TaskNameEN = a.NameEN,
                TaskDescriptionEN = a.DescriptionEN,
                IsDynamic = a.IsDynamic,
                TaskTypeID = a.ProjectTaskTypeID,
                ID = a.ID,
                IsActive = a.IsActive
            })
            .ToList();

            model.ForEach(a => a.TaskTypeName = services.projectTaskTypeRepo.FirstOrDefault(c => c.ID == a.TaskTypeID).Name);

            return View(model);

        }

        public ActionResult Add()
        {
            ViewData["projectTaskTypeDropDown"] = GetProjectTaskType;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AdminTaskPostVM model)
        {
            ViewData["projectTaskTypeDropDown"] = GetProjectTaskType;

            if (ModelState.IsValid)
            {
                var entity = new ProjectTask
                {
                    NameEN = model.NameEN,
                    NameTR = model.NameTR,
                    DescriptionEN = model.DescriptionEN,
                    DescriptionTR = model.DescriptionTR,
                    HasHyperLink = model.HasHyperLink,
                    HasTextInput = model.HasTextInput,
                    IsDynamic = model.IsDynamic,
                    IsActive = true,
                    TaskLogo = MediaManagerService.Save(new MediaFormatDTO { Media = model.TaskLogoFile, MediaType = 0 }),
                    ProjectTaskTypeID = model.ProjectTaskTypeID,
                    HyperLink = model.HyperLink
                };

                services.projectTaskRepo.Add(entity);
                services.Commit();

                ViewBag.IsSuccess = true;
                ViewBag.Message = "Görev Tanımlama tamamlandı";

                return View(model);
            }

            return View(model);
        }
    }
}