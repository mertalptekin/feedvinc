using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
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

        public IEnumerable<SelectListItem> GetProjectStatusEN(byte? selectedProjectStatus = null)
        {
            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Status_Validation,
                     Value="0",
                     Selected = (selectedProjectStatus==0 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Newer",
                    Value="1",
                    Selected = (selectedProjectStatus==1 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Development of Phase",
                    Value="2",
                    Selected = (selectedProjectStatus==2 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Ready for Publication",
                    Value="3",
                    Selected = (selectedProjectStatus==3 ? true: false)
                }

            };

            return model;
        }

        public IEnumerable<SelectListItem> GetProjectStatusTR(byte? selectedProjectStatus=null)
        {

            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Status_Validation,
                     Value="0",
                     Selected = (selectedProjectStatus==0 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Yeni Başlandı",
                    Value="1",
                    Selected = (selectedProjectStatus==1 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Geliştirme Aşamasında",
                    Value="2",
                    Selected = (selectedProjectStatus==2 ? true: false)

                },
                new SelectListItem
                {
                    Text ="Yayında Hazır",
                    Value="3",
                    Selected = (selectedProjectStatus==3 ? true: false)
                }

            };

            return model;

        }

        public IEnumerable<SelectListItem> GetInvestmentStatusEN(byte? selectedProjectInvestmentStatus = null)
        {
            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Investment_Status_Validation,
                     Value="0",
                     Selected = (selectedProjectInvestmentStatus==0 ? true: false)
                },
                new SelectListItem
                {
                    Text ="not invested.",
                    Value="1",
                    Selected = (selectedProjectInvestmentStatus==1 ? true: false)
                },
                new SelectListItem
                {
                    Text ="1st lap invested ",
                    Value="2",
                    Selected = (selectedProjectInvestmentStatus==2 ? true: false)
                },
                new SelectListItem
                {
                    Text ="2nd lap invested",
                    Value="3",
                    Selected = (selectedProjectInvestmentStatus==3 ? true: false)
                }

            };

            return model;

        }

        public IEnumerable<SelectListItem> GetInvestmentStatusTR(byte? selectedProjectInvestmentStatus = null)
        {

            var model = new List<SelectListItem>
            {
                new SelectListItem
                {
                     Text = SiteLanguage.Project_Investment_Status_Validation,
                     Value="0",
                     Selected = (selectedProjectInvestmentStatus==0 ? true: false)
                },
                new SelectListItem
                {
                    Text ="Yatırım Almadı",
                    Value="1",
                    Selected = (selectedProjectInvestmentStatus==1 ? true: false)
                },
                new SelectListItem
                {
                    Text ="1.Tur Yatırımı Aldı",
                    Value="2",
                    Selected = (selectedProjectInvestmentStatus==2 ? true: false)
                },
                new SelectListItem
                {
                    Text ="2.Tur Yatırımı Aldı",
                    Value="3",
                    Selected = (selectedProjectInvestmentStatus==3 ? true: false)
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

        [HttpGet]
        public ActionResult ProjectEdit(string projectname, string projectCode)
        {
 
            var model = services.projectRepo.Where(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode).Select(y => new ProjectPostVM
            {

                ID = (int)y.ID,
                CountryID = (int)y.CountyID,
                CityID = (int)y.CityID,
                ProjectName = y.ProjectName,
                ProjectProfileLogo = y.ProjectProfileLogo,
                ProjectStatus = (byte)y.ProjectStatus,
                ProjectInvestmentStatus = (byte)y.InvestmentStatus,
                About = y.About,
                AndroidLink = y.AndroidLink,
                AppleLink = y.AppleLink,
                CategoryID = y.ProjectCategoryID,
                WebLink = y.WebLink,
                ProjectTags = y.ProjectTags,
                SalesPitch = y.SalesPitch,
                MenuID = 1

            }).FirstOrDefault();

            ViewData["Category"] = GetProjectCategoryDropDown(model.CategoryID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);
            ViewData["City"] = GetCityDropDown(1,model.CityID);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR(model.ProjectStatus) : GetProjectStatusEN(model.ProjectStatus);
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR(model.ProjectInvestmentStatus) : GetInvestmentStatusEN(model.ProjectInvestmentStatus);

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult ProjectEdit(ProjectPostVM model)
        {

            ViewData["Category"] = GetProjectCategoryDropDown(model.CategoryID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);
            ViewData["City"] = GetCityDropDown(1, model.CityID);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR(model.ProjectStatus) : GetProjectStatusEN(model.ProjectStatus);
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR(model.ProjectInvestmentStatus) : GetInvestmentStatusEN(model.ProjectInvestmentStatus);

            var entity = services.projectRepo.FirstOrDefault(x => x.ID == model.ID);

            if (model.ProjectPhoto==null)
                ModelState.Remove("ProjectPhoto");
            
            if (ModelState.IsValid)
            {
                entity.ProjectName = model.ProjectName;
                entity.SalesPitch = model.SalesPitch;
                entity.ProjectProfileLogo = model.ProjectProfileLogo == null ? MediaManagerService.Save(new Models.DTO.MediaFormatDTO { Media = model.ProjectPhoto, MediaType = 0 }) : model.ProjectProfileLogo;
                entity.ProjectCategoryID = model.CategoryID;
                entity.CountyID = model.CountryID;
                entity.CityID = model.CityID;
                entity.ProjectStatus = model.ProjectStatus;
                entity.InvestmentStatus = model.ProjectInvestmentStatus;
                entity.IsInvested = model.ProjectInvestmentStatus == 1 ? false : true;
                entity.InvestmentDate = DateTime.Now;
                entity.WebLink = model.WebLink;
                entity.AppleLink = model.AppleLink;
                entity.AndroidLink = model.AndroidLink;
                entity.About = model.About;
                entity.ProjectTags = entity.ProjectTags;
                model.MenuID = 1;

                services.Commit();
                ViewBag.Success = SiteLanguage.Project_Success;
                ViewBag.IsSuccess = true;

                return View(model);
            }

            return View(model);
            
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
            ViewData["City"] = GetCityDropDown(1);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR() : GetProjectStatusEN();
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR() : GetInvestmentStatusEN();

            if (ModelState.IsValid)
            {

                var entity = new Project
                {
                    ProjectName = model.ProjectName,
                    SalesPitch = model.SalesPitch,
                    ProjectProfileLogo = MediaManagerService.Save(new Models.DTO.MediaFormatDTO { Media = model.ProjectPhoto, MediaType = 0 }),
                    ProjectCategoryID = model.CategoryID,
                    CountyID = model.CountryID,
                    CityID = model.CityID,
                    InvestmentStatus = model.ProjectInvestmentStatus,
                    ProjectStatus = model.ProjectStatus,
                    IsInvested = model.ProjectInvestmentStatus == 1 ? false : true,
                    ProjectSlugify = model.ProjectName.SlugText(),
                    IsActive = true,
                    UserID = UserManagerService.CurrentUser.ID,
                    InvestmentDate = DateTime.Now,
                    WebLink = model.WebLink,
                    AndroidLink = model.AndroidLink,
                    AppleLink = model.AppleLink,
                    CreateDate = DateTime.Now,
                    About = model.About,
                    ProjectTags = model.ProjectTags,
                    ProjectCode = RandomCodeGenerator.Generate()

                };

                services.projectRepo.Add(entity);
                services.Commit();
                ViewBag.Success = SiteLanguage.Project_Success;
                ViewBag.IsSuccess = true;

                return View(model);
            }

            return View(model);
        }


    }
}