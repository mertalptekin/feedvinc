using FeedVinc.WEB.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminProjectController : AdminBaseController
    {
        // GET: Admin/AdminProject
        public ActionResult Index()
        {

            var model = services.projectRepo.ToList().Select(a => new AdminProjectVM
            {
                ProjectName = a.ProjectName,
                ProjectPhaseID = a.ProjectPhaseID,
                ProjectLogo = a.ProjectProfileLogo,
                ProjectMissionLevel = a.ProjectLevel,
               ProjectOwnerID = a.UserID,
               ProjectCategoryID = a.ProjectCategoryID

            }).ToList();


            model.ForEach(a => a.ProjectOwner = services.appUserRepo.Where(y => y.ID == a.ProjectOwnerID).Select(z => new ProjectOwnerVM
            {
                FullName = z.Name + " " + z.SurName
            }).FirstOrDefault());

            model.ForEach(a => a.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(f => f.ID == a.ProjectCategoryID).CategoryName);

            model.ForEach(a => a.ProjectMissionName = services.projectTaskTypeRepo.FirstOrDefault(z => z.ID == a.ProjectPhaseID).Name);


            return View(model);
        }
    }
}