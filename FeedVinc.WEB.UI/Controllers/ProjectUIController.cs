using FeedVinc.WEB.UI.Models.ViewModels.Project;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class ProjectUIController : BaseUIController
    {

        public IEnumerable<SelectListItem> GetProjectStatusEN()
        {
            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Status_Validation,
                     Value="0"
                },
                new SelectListItem
                {
                    Text ="Newer",
                    Value="1",
                },
                new SelectListItem
                {
                    Text ="Development of Phase",
                    Value="2"
                },
                new SelectListItem
                {
                    Text ="Ready for Publication",
                    Value="3"
                }

            };

            return model;
        }

        public IEnumerable<SelectListItem> GetProjectStatusTR()
        {

            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Status_Validation,
                     Value="0"
                },
                new SelectListItem
                {
                    Text ="Yeni Başlandı",
                    Value="1",
                },
                new SelectListItem
                {
                    Text ="Geliştirme Aşamasında",
                    Value="2"
                },
                new SelectListItem
                {
                    Text ="Yayında Hazır",
                    Value="3"
                }

            };

            return model;

        }


        public IEnumerable<SelectListItem> GetInvestmentStatusEN()
        {
            //< option value = "1" > Yatırım Almadı </ option >

            //                         < option value = "2" > 1.Tur Yatırımı Aldı</ option >

            //                             < option value = "3" > 2.Tur Yatırımı Aldı</ option >

            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Investment_Status_Validation,
                     Value="0"
                },
                new SelectListItem
                {
                    Text ="not invested.",
                    Value="1",
                },
                new SelectListItem
                {
                    Text ="1st lap invested ",
                    Value="2"
                },
                new SelectListItem
                {
                    Text ="2nd lap invested",
                    Value="3"
                }

            };

            return model;

        }

        public IEnumerable<SelectListItem> GetInvestmentStatusTR()
        {
            //< option value = "1" > Yatırım Almadı </ option >

            //                         < option value = "2" > 1.Tur Yatırımı Aldı</ option >

            //                             < option value = "3" > 2.Tur Yatırımı Aldı</ option >

            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Investment_Status_Validation,
                     Value="0"
                },
                new SelectListItem
                {
                    Text ="Yatırım Almadı",
                    Value="1",
                },
                new SelectListItem
                {
                    Text ="1.Tur Yatırımı Aldı",
                    Value="2"
                },
                new SelectListItem
                {
                    Text ="2.Tur Yatırımı Aldı",
                    Value="3"
                }

            };

            return model;

        }

        // GET: ProjectUI
        public ActionResult ProjectProfile(string projectname, string projectCode)
        {
            return View();
        }

        public ActionResult Me()
        {
            var model = new MyProjectVM();

            var projects = services.projectRepo.
                Where(x => x.UserID == UserManagerService.CurrentUser.ID).
                Select(a => new ProjectDetailVM
                {

                    ProjectName = a.ProjectName,
                    ProjectID = a.ID,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfilePhoto = a.ProjectProfileLogo

                }).OrderBy(x => x.ProjectName).ToList();

            projects.ForEach(a => a.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(x => x.ID == a.ProjectCategoryID).CategoryName);

            model.Projects = projects;

            return View(model);
        }

        public ActionResult ProjectEdit(string projectname, string projectCode)
        {
            ViewBag.MenuID = 1;

            return View();
        }

        public ActionResult ProjectTeamEdit(string projectname, string projectCode)
        {
            ViewBag.MenuID = 2;

            return View();
        }

        public ActionResult ProjectPhotoEdit(string projectname, string projectCode)
        {
            ViewBag.MenuID = 3;

            return View();
        }

        [HttpPost]
        public JsonResult ProjectPhotoEdit()
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult ProjectTeamEdit()
        {
            return Json(null);
        }

        [HttpPost]
        public JsonResult ProjectEdit()
        {
            return Json(null);
        }


        public ActionResult ProjectAdd()
        {
            ViewData["Category"] = GetProjectCategoryDropDown();
            ViewData["Country"] = GetCountryDropDown();
            ViewData["City"] = GetCityDropDown(1);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR() : GetProjectStatusEN();
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR() : GetInvestmentStatusEN();

            return View(new ProjectPostVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectAdd(ProjectPostVM model)
        {

            ViewData["Category"] = GetProjectCategoryDropDown();
            ViewData["Country"] = GetCountryDropDown();
            ViewBag["City"] = GetCityDropDown(1);
            ViewBag["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR() : GetProjectStatusEN();
            ViewBag["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR() : GetInvestmentStatusEN();

            if (ModelState.IsValid)
            {
                return View();
            }

            return View(model);
        }


    }
}