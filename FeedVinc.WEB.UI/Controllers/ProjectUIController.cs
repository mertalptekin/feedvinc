using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Project;
using FeedVinc.WEB.UI.Models.ViewModels.Project.Profile;
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

        public IEnumerable<SelectListItem> GetProjectStatusTR(byte? selectedProjectStatus = null)
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

            var model = new ProjectProfileWrapperVM();

            model.ProjectProfile = services.projectRepo.Where(x => x.ProjectName == projectname && x.ProjectCode == projectCode).
                Select(a => new ProjectProfileVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectID = a.ID,
                    SalesPitch = a.SalesPitch,
                    About = a.About,
                    AndroidLink = a.AndroidLink,
                    AppleLink = a.AppleLink,
                    FeedPoint = "0",
                    Weblink = a.WebLink,
                    FollowerCount = 0,
                    ProjectProfilePhoto = a.ProjectProfileLogo,
                    ProjectLevel = "0",
                    ProjectCode = a.ProjectCode,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID,
                    ProjectOwnerID = a.UserID,
                    InvestedText = LanguageService.getCurrentLanguage == "tr TR" ? GetInvestmentStatusTR((byte)a.InvestmentStatus).Where(x=> x.Selected==true).FirstOrDefault().Text : GetInvestmentStatusEN((byte)a.InvestmentStatus).Where(c=> c.Selected==true).FirstOrDefault().Text

                }).
                FirstOrDefault();

            model.ProjectProfile.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(x => x.ID == model.ProjectProfile.CategoryID).CategoryName;

            model.ProjectProfile.CountryName = services.countryRepo.FirstOrDefault(a => a.ID == model.ProjectProfile.CountryID).CountryName;

            model.ProjectProfile.CityName = services.cityRepo.FirstOrDefault(a => a.ID == model.ProjectProfile.CityID).CityName;

            model.ProjectProfile.Owner = services.appUserRepo.
                Where(x => x.ID == model.ProjectProfile.ProjectOwnerID).
                Select(c => new ProjectProfileOwner
                {
                    ProjectOwnerID = c.ID,
                    ProjectOwnerPhotoPath = c.ProfilePhoto,
                    ProjectOwnerSlugify = c.UserSlugify,
                    ProjectOwnerName = c.Name + " " + c.SurName
                })
                .FirstOrDefault();


            var projectShareModel = new ProjectShareVM();
            projectShareModel.ProfilePath = model.ProjectProfile.ProjectProfilePhoto;
            projectShareModel.OwnerID = model.ProjectProfile.ProjectOwnerID;
            projectShareModel.ProjectName = model.ProjectProfile.ProjectName;

            model.ProjectFeeds = services.projectShareRepo.
                Where(x => x.ProjectID == model.ProjectProfile.ProjectID).
                Select(a => new ShareVM
                {
                    Location = a.Location,
                    CommentCount = 0,
                    LikeCount = 0,
                    ShareCount = 0,
                    ProjectID = a.ProjectID,
                    Post = a.Content,
                    MediaTypeID = a.MediaType,
                    PostMediaPath =a.SharePath,
                    PostDate = a.ShareDate,
                    PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate,LanguageService.getCurrentLanguage),
                    ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                    Project = projectShareModel

                })
                .OrderByDescending(x=> x.PostDate)
                .ToList();

            model.ProjectPhotos = services.projectPhotoRepo
                .Where(x => x.ProjectID == model.ProjectProfile.ProjectID)
                .Select(a => new ProjectProfilePhotoVM
                {
                    PhotoPath = a.PhotoPath,
                    ProjectCode = model.ProjectProfile.ProjectCode,
                    ProjectSlugify = model.ProjectProfile.ProjectSlugify
                }).
                ToList();

            model.ProjectVideos = services.projectVideoRepo
                .Where(x => x.ProjectID == model.ProjectProfile.ProjectID)
                .Select(a => new ProjectProfileVideoVM
                {
                    VideoPath = a.VideoPath,
                    ProjectCode = model.ProjectProfile.ProjectCode,
                    ProjectSlugify = model.ProjectProfile.ProjectSlugify

                })
                .ToList();

            var projectTeamIDs = services.projectTeamRepo
                .Where(x => x.ProjectID == model.ProjectProfile.ProjectID)
                .Select(x => x.UserID);

            model.ProjectTeams = services.appUserRepo.Where(x => projectTeamIDs.Contains(x.ID) && x.ID == model.ProjectProfile.ProjectOwnerID)
                .Select(a => new ProjectProfileTeamVM
                {
                    Title = a.JobTitle,
                    UserName = a.Name + " " + a.SurName,
                    UserProfilePhoto = a.ProfilePhoto,
                    UserSlugify = a.UserSlugify,
                    UserID = a.ID
                })
                .ToList();

            model.ProjectTeams.ForEach(a => a.ProjectNames = services.projectRepo.Where(c => c.UserID == a.UserID).Select(f => f.ProjectName).ToList());


            return View(model);
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
                    ProjectProfilePhoto = a.ProjectProfileLogo,
                    ProjectCategoryID = a.ProjectCategoryID

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
                ProjectMenu = new ProjectMenuVM { MenuID = 1, ProjectCode = projectCode, ProjectSlugify = y.ProjectSlugify }

            }).FirstOrDefault();

            ViewData["Category"] = GetProjectCategoryDropDown(model.CategoryID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);
            ViewData["City"] = GetCityDropDown(1, model.CityID);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR(model.ProjectStatus) : GetProjectStatusEN(model.ProjectStatus);
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR(model.ProjectInvestmentStatus) : GetInvestmentStatusEN(model.ProjectInvestmentStatus);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProjectEdit(ProjectPostVM model)
        {

            ViewData["Category"] = GetProjectCategoryDropDown(model.CategoryID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);
            ViewData["City"] = GetCityDropDown(1, model.CityID);
            ViewData["ProjectStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetProjectStatusTR(model.ProjectStatus) : GetProjectStatusEN(model.ProjectStatus);
            ViewData["InvestmentStatus"] = LanguageService.getCurrentLanguage == "tr-TR" ? GetInvestmentStatusTR(model.ProjectInvestmentStatus) : GetInvestmentStatusEN(model.ProjectInvestmentStatus);

            var entity = services.projectRepo.FirstOrDefault(x => x.ID == model.ID);

            if (model.ProjectPhoto == null)
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

                services.Commit();
                ViewBag.Success = SiteLanguage.Project_Success;
                ViewBag.IsSuccess = true;

                model.ProjectMenu = new ProjectMenuVM { MenuID = 1, ProjectCode = entity.ProjectCode, ProjectSlugify = entity.ProjectSlugify };

                return View(model);
            }

            return View(model);

        }

        public ActionResult ProjectPhotoEdit(string projectname, string projectCode)
        {
            

            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            var projectPhotos = services.projectPhotoRepo.Where(x=> x.ProjectID==project.ID).Select(a=> new ProjectPhotoEditVM { ID=a.ID, PhotoPath=a.PhotoPath }).ToList();

            ViewBag.ProjectID = project.ID;

            var model = new ProjectPhotoVM {
                Menu = new ProjectMenuVM
                {
                    MenuID =3,
                    ProjectCode = project.ProjectCode, ProjectSlugify=project.ProjectSlugify
                } 
                ,
                ProjectPhotos = projectPhotos
            };

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public JsonResult ProjectPhotoEdit(ProjectPhotoPostVM model)
        {
            List<ProjectPhotoEditVM> pictures = new List<ProjectPhotoEditVM>();

            if (ModelState.IsValid)
            {
                foreach (var item in model.Files)
                {
                    ProjectPhoto entity = new ProjectPhoto();
                    entity.ProjectID = model.ProjectID;
                    entity.PhotoPath = MediaManagerService.Save(new MediaFormatDTO
                   { Media = item, MediaType = 0 });

                    services.projectPhotoRepo.Add(entity);
                    services.Commit();

                    pictures.Add(new Models.ViewModels.Project.ProjectPhotoEditVM { PhotoPath = entity.PhotoPath, ID = entity.ID });
                }
                return Json(new ValidationDTO { IsValid = true, Data = pictures, SuccessMessage = "OK" });
            }

            return Json(new ValidationDTO { IsValid=false, ErrorMessage= SiteLanguage.Multiple_Image_Upload_Validation, Data=null });
        }

        [HttpGet]
        public JsonResult ChangeProjectOwner(int OwnerID,int ProjectID)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ID == ProjectID);

            var ProjectTeam = new ProjectTeam();
            ProjectTeam.UserID = project.UserID;
            ProjectTeam.ProjectID = ProjectID;
            services.projectTeamRepo.Add(ProjectTeam);
            services.Commit();

            project.UserID = OwnerID;
            services.Commit();

            services.projectTeamRepo.Remove(x => x.ProjectID == ProjectID && x.UserID == OwnerID);
            services.Commit();


            return Json(new ValidationDTO { RedirectURL="/logout", IsValid=true, SuccessMessage=SiteLanguage.ProjectOwnerChange },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddTeamMember(ProjectTeamAddVM model)
        {
            var teamUserIDs = services.projectTeamRepo.Where(x => x.ProjectID == model.ProjectID).Select(a => a.UserID);

            var user = services.appUserRepo.Where(a => a.IsActive && a.Email==model.AddMemberEmail && a.ID != model.OwnerID && !teamUserIDs.Contains(a.ID)).Select(c => new ProjectTeamUserVM
            {
                UserID = c.ID,
                UserName = c.Name + " " + c.SurName,
                ProfilePhoto = c.ProfilePhoto,
                UserJobType = c.JobTitle,
                UserSlugify = c.UserSlugify

            }).FirstOrDefault();

            if (user != null)
            {
                var entity = new ProjectTeam { UserID = model.OwnerID, ProjectID = model.ProjectID, IsActive = true };
                services.projectTeamRepo.Add(entity);
                services.Commit();

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.ProjectTeamAdd_Success, Data = user });
            }
            else
            {
                return Json(new ValidationDTO { IsValid = false, ErrorMessage = SiteLanguage.ProjectTeamAdd_Error, Data = user });
            }    

        }

        [HttpGet]
        public ActionResult ProjectTeamEdit(string projectname, string projectCode)
        {
            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            var owner = services.appUserRepo.FirstOrDefault(x => x.ID == project.UserID);
            

            var projectTeamUserIDs = services.projectTeamRepo.Where(x => x.ProjectID == project.ID).Select(a => a.UserID);

            var teamUsers = services.appUserRepo.Where(x => projectTeamUserIDs.Contains(x.ID)).Select(a => new ProjectTeamUserVM
            {
                ProfilePhoto = a.ProfilePhoto,
                UserID = a.ID,
                ProjectID = project.ID,
                UserSlugify = a.UserSlugify,
                UserJobType = a.JobTitle,
                UserName = a.Name + " " + a.SurName

            }).ToList();

            var projectOwners = teamUsers.Select(a => new SelectListItem { Value = a.UserID.ToString(), Text = a.UserName, Selected = a.UserID == project.UserID ? true : false }).ToList();

            projectOwners.Add(new SelectListItem { Text = owner.Name + " " + owner.SurName, Value = owner.ID.ToString(), Selected = true });


            ViewData["ProjectOwnerDropDown"] = projectOwners;

            var model = new ProjectTeamVM
            {
                TeamUsers = teamUsers,
                ProjectID = project.ID,
                OwnerID = project.UserID,
                Menu = new ProjectMenuVM { MenuID = 2, ProjectSlugify = project.ProjectSlugify, ProjectCode = project.ProjectCode }
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ProjectTeamEdit()
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