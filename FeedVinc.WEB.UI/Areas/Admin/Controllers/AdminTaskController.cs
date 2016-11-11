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

        public ActionResult Edit(int id)
        {
            ViewData["projectTaskTypeDropDown"] = GetProjectTaskType;

            var model = services.projectTaskRepo
            .Where(x => x.ID == id)
            .Select(a => new AdminTaskPostVM
            {
                NameTR = a.NameTR,
                DescriptionTR = a.DescriptionTR,
                NameEN = a.NameEN,
                DescriptionEN = a.DescriptionEN,
                IsDynamic = a.IsDynamic,
                ProjectTaskTypeID = a.ProjectTaskTypeID,
                ID = a.ID,
                IsActive = a.IsActive,
                TaskLogo = a.TaskLogo,
                HyperLink = a.HyperLink,
                HasHyperLink = a.HasHyperLink,
                HasTextInput = a.HasTextInput
            })
            .FirstOrDefault();


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminTaskPostVM model)
        {
            ViewData["projectTaskTypeDropDown"] = GetProjectTaskType;

            if (model.TaskLogo != null)
            {
                if (ModelState.ContainsKey("TaskLogoFile"))
                    ModelState["TaskLogoFile"].Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                var entity = services.projectTaskRepo.FirstOrDefault(a => a.ID == model.ID);

                entity.NameEN = model.NameEN;
                entity.NameTR = model.NameTR;
                entity.DescriptionEN = model.DescriptionEN;
                entity.DescriptionTR = model.DescriptionTR;
                entity.HasHyperLink = model.HasHyperLink;
                entity.HasTextInput = model.HasTextInput;
                entity.IsDynamic = model.IsDynamic;
                entity.IsActive = true;
                entity.TaskLogo = model.TaskLogo == null ? MediaManagerService.Save(new MediaFormatDTO { Media = model.TaskLogoFile, MediaType = 0 }) : model.TaskLogo;
                entity.ProjectTaskTypeID = model.ProjectTaskTypeID;
                entity.HyperLink = model.HyperLink;
                entity.HasTextInput = model.HasTextInput;
                entity.HasHyperLink = model.HasHyperLink;

                services.Commit();

                ViewBag.IsSuccess = true;
                ViewBag.Message = "Görev Düzenlendi";

                return View(model);
            }

            return View(model);
        }

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