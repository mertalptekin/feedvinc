using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Project;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class ProjectTaskUIController : BaseUIController
    {

        public ActionResult Home(string projectname,string projectCode)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            int projectLevel = int.Parse(project.ProjectLevel);
            var TotalTaskCount = services.projectTaskRepo.Count(x => x.ProjectTaskTypeID == project.ProjectPhaseID);
            var userCompletedTaskCount = services.projectTaskDetailRepo.Count(x => x.ProjectID == project.ID && x.IsCompleted == true && x.TaskTypeID==project.ProjectPhaseID);

            if (TotalTaskCount==userCompletedTaskCount && (TotalTaskCount!=0 || userCompletedTaskCount!=0))
            {
                if (project.ProjectPhaseID<=4)
                {
                    project.ProjectLevel = (projectLevel + 25).ToString();
                    project.ProjectPhaseID = project.ProjectPhaseID + 1;
                    
                }

                services.Commit();
            }

            ProjectTaskHomeVM vm = new ProjectTaskHomeVM();
            vm.ProjectPhaseID = project.ProjectPhaseID;
            vm.ProjectStepLink = "/project-task-step/" + projectname + "/" + projectCode;
            vm.CompletedTaskCount = userCompletedTaskCount; 
            vm.TotalTaskCount = TotalTaskCount;
            vm.ProjectLevel = project.ProjectLevel;

            return View(vm);
        }

        // GET: ProjectTaskUI
        public ActionResult Index(string projectname, string projectCode)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            ViewBag.ProjectPhaseID = project.ProjectPhaseID;
            ViewBag.BackToTaskStep = "/project-task/" + projectname + "/" + projectCode;
            ViewBag.ProjectSlug = project.ProjectSlugify;
            ViewBag.ProjectCode = project.ProjectCode;


            var projectTask = services.projectTaskRepo.Where(x => x.ProjectTaskTypeID == project.ProjectPhaseID).Select(a => new ProjectTaskVM
            {

                Title = LanguageService.getCurrentLanguage == "tr-TR" ? a.NameTR:a.NameEN,
                ProjectTaskID = a.ID


            }).ToList();

            projectTask.ForEach(a => a.IsExist = services.projectTaskDetailRepo.Any(y => y.ProjectTaskID == a.ProjectTaskID && y.ProjectID == project.ID));

            return View(projectTask);
        }

        public ActionResult Detail(string projectname,string projectCode, int projectTaskID)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);
            string _currentLang = LanguageService.getCurrentLanguage;

            ViewBag.BackToTaskStep = "/project-task-step/" + projectname + "/" + projectCode;

            var projectTask = services.projectTaskRepo.Where(x => x.ID == projectTaskID).Select(a => new ProjectTaskDetailVM
            {
                TaskTitle = _currentLang =="tr-TR" ? a.NameTR:a.NameEN,
                TaskDescription = _currentLang == "tr-TR" ? a.DescriptionTR:a.DescriptionEN,
                TaskLogo = a.TaskLogo,
                ProjectID = project.ID,
                TaskID = a.ID,
                HasHyperLink = a.HasHyperLink,
                HasTextInput = a.HasTextInput,
                HyperLink = a.HyperLink,
                TaskTypeID = project.ProjectPhaseID
               

            }).FirstOrDefault();


            //görev linke tıklamaya veya bilgi görüntüleme göreviyse

            if (projectTask.HasHyperLink==false || projectTask.HasTextInput==false || projectTask.HasHyperLink==true)
            {
                var projectTaskDetail = services.projectTaskDetailRepo.FirstOrDefault(y => y.ProjectTaskID == projectTask.TaskID && y.ProjectID == project.ID);

                if (projectTaskDetail == null)
                {
                    ProjectTaskDetail entity = new ProjectTaskDetail();
                    entity.ProjectID = project.ID;
                    entity.ProjectTaskID = projectTask.TaskID;
                    entity.Answer = "";
                    entity.IsCompleted = true;
                    entity.TaskTypeID = project.ProjectPhaseID; 

                    services.projectTaskDetailRepo.Add(entity);
                    services.Commit();

                }
            }

            projectTask.Answer = services.projectTaskDetailRepo.FirstOrDefault(c => c.ProjectTaskID == projectTaskID && c.ProjectID == project.ID && c.IsCompleted == true)==null ? "": services.projectTaskDetailRepo.FirstOrDefault(c => c.ProjectTaskID == projectTaskID && c.ProjectID == project.ID && c.IsCompleted == true).Answer;


            return View(projectTask);

        }

        [HttpPost]
        public JsonResult AnswerToQuestion(TaskPostVM model)
        {
            if (ModelState.IsValid)
            {

                var projectTask = services.projectTaskDetailRepo.FirstOrDefault(y => y.ProjectTaskID == model.TaskID && y.ProjectID == model.ProjectID);

                if (projectTask==null)
                {
                    ProjectTaskDetail entity = new ProjectTaskDetail();
                    entity.ProjectID = model.ProjectID;
                    entity.ProjectTaskID = model.TaskID;
                    entity.Answer = model.TaskAnswer;
                    entity.IsCompleted = true;
                    entity.TaskTypeID = model.TaskTypeID;

                    services.projectTaskDetailRepo.Add(entity);
                    services.Commit();

                    return Json(new { success = "OK", IsValid = true });
                }
                else
                {
                    projectTask.Answer = model.TaskAnswer;
                    services.Commit();

                    return Json(new { success = "OK", IsValid = true });

                }

            }

            var errorList = ModelState.Values.
               SelectMany(m => m.Errors).
               Select(e => e.ErrorMessage).
               ToList();

            return Json(new { error = errorList, IsValid = false });
        }

    }
}