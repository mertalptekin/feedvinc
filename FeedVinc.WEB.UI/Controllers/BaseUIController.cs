using FeedVinc.BLL.Services;
using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Filter;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using app = FeedVinc.WEB.UI.MvcApplication;

namespace FeedVinc.WEB.UI.Controllers
{
    [LoginControl]
    public class BaseUIController : Controller
    {
        protected UnitOfWork services;
        protected UserVM _currentUser;


        public BaseUIController()
        {
            services = new UnitOfWork();
            _currentUser = UserManagerService.CurrentUser;

        }

        [HttpPost]
        public ActionResult Share(SharePostVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.MediaPhoto != null || model.MediaVideo != null)
                {
                    MediaFormatDTO mediaDTO = new MediaFormatDTO
                    {
                        MediaType = model.MediaPhoto != null ? 0 : 1,
                        Media = model.MediaPhoto != null ? model.MediaPhoto : model.MediaVideo,
                    };

                    model.MediaPath = MediaManagerService.Save(mediaDTO);
                    model.MediaTypeID = mediaDTO.MediaType;

                }

                model.ShareTitle = SiteLanguage._AROUNDME;
                ApplicationUserShare Usershare = new ApplicationUserShare
                {
                    Location = model.Location,
                    Content = model.Post,
                    ShareTypeID = model.ShareTypeID,
                    IsActive = true,
                    SharePath = model.MediaPath,
                    MediaType = model.MediaTypeID,
                    ShareDate = DateTime.Now,
                    UserID = UserManagerService.CurrentUser.ID
                };
                services.appUserShareRepo.Add(Usershare);
                int ID = services.Commit();


                SharePostDTO dto = new SharePostDTO
                {
                    FeedID = Usershare.ID,
                    Post = model.Post,
                    Location = model.Location,
                    MediaPath = model.MediaPath,
                    MediaTypeID = model.MediaTypeID,
                    ShareTypeID = model.ShareTypeID,
                    ShareTitle = SiteLanguage.Around_Me,
                    User = _currentUser,
                    ProjectShare = services.projectRepo.Where(x => x.ID == ID).Select(a => new ProjectSharePostDTO
                    {

                        ProjectName = a.ProjectName,
                        ProjectProfilePath = a.ProjectProfileLogo,
                        ProjectSlugify = a.ProjectSlugify,
                        ProjectID = a.ID

                    }).FirstOrDefault(),
                    PrettyDate = DateTimeService.GetPrettyDate(DateTime.Now, LanguageService.getCurrentLanguage),
                    Validation = new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Shared_your_Post }
                };

                return PartialView("~/Views/HomeUI/FeedPartial/_feed_around_me.cshtml", dto);

            }


            return Json(new ValidationDTO { IsValid = false, ErrorMessage = SiteLanguage.Post_Validation });

        }

        public IEnumerable<SelectListItem> GetProjectCategoryDropDown(byte? selectedCategoryID = 0)
        {
            var model = services.projectCategoryRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage).Select(a => new SelectListItem { Text = a.CategoryName, Value = a.ID.ToString(), Selected = a.ID == selectedCategoryID ? true : false }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_Category_Validation, Value = "0", Selected = selectedCategoryID != null ? true : false });

            return model.OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetCountryDropDown(int? selectedCountryID = 0)
        {
            var model = services.countryRepo.ToList().Select(a => new SelectListItem { Text = a.CountryName, Value = a.ID.ToString(), Selected = (a.ID == selectedCountryID ? true : false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.County_Validation, Value = "0", Selected = selectedCountryID == null ? true : false });

            return model.OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetMyProjectsDropDown()
        {
            var projectIDs = services.projectRepo.
                Where(x => x.UserID == UserManagerService.CurrentUser.ID).
                Select(x => x.ID).
                ToList();

            var model = services.projectRepo.
                Where(x => projectIDs.Contains(x.ID)).
                Select(a => new SelectListItem
                {
                    Text = a.ProjectName,
                    Value = a.ID.ToString()
                }).
                OrderBy(x => x.Text).
                ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_Validation, Value = "0", Selected = true });

            return model;
        }

        public IEnumerable<SelectListItem> GetUserTypeDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = SiteLanguage.Developer,
                    Value = "1",
                    Selected = true
                },
                new SelectListItem
                {
                    Text = SiteLanguage.Financier,
                    Value = "2",
                    Selected = false
                },
                new SelectListItem
                {
                    Text = SiteLanguage.Entrepreneur,
                    Value="3",
                    Selected=false
                }
            };
        }

        public PartialViewResult GetFilterPartial()
        {
            var model = new FilterVM
            {
                ProjectCategory = GetProjectCategoryDropDown(),
                Country = GetCountryDropDown(),
                City = GetCityDropDown(1)
            };

            return PartialView("~/Views/Shared/Partial/_filters.cshtml", model);
        }

        public string GetInvestedStatus(int investedStatusID)
        {
            string output = "";

            switch (investedStatusID)
            {
                case 1:
                    output = SiteLanguage.ProjectInvested_2nd_Not_Invested;
                    break;
                case 2:
                    output = SiteLanguage.ProjectInvested_1st_lap;
                    break;
                case 3:
                    output = SiteLanguage.ProjectInvested_2nd_lap;
                    break;
            }

            return output;
        }

        public string GetProjectStatus(int projectStatusID)
        {
            string output = "";

            switch (projectStatusID)
            {
                case 1:
                    output = SiteLanguage.ProjectStatus_Newer;
                    break;
                case 2:
                    output = SiteLanguage.ProjectStatus_Development_of_Phase;
                    break;
                case 3:
                    output = SiteLanguage.ProjectStatus_Ready_for_Publication;
                    break;
            }

            return output;
        }

        public IEnumerable<SelectListItem> GetCityDropDown(int? countryID, int? selectedCityID = 0)
        {
            var model = services.cityRepo.Where(x => x.CountryID == countryID).Select(a => new SelectListItem { Text = a.CityName, Value = a.ID.ToString(), Selected = (a.ID == (int)selectedCityID ? true : false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.City_Validation, Value = "0", Selected = (selectedCityID == null ? true : false) });

            return model.OrderBy(x => x.Value);
        }


        public PartialViewResult GetNavbar()
        {
            IEnumerable<byte> appMenuIDs = services.appMenuDetailRepo.Where(x => x.UserTypeID == UserManagerService.CurrentUser.UserTypeID).Select(x => x.ApplicationMenuID).ToList();

            var model = services.appMenuRepo.Where(x => x.Lang == LanguageService.getCurrentLanguage && appMenuIDs.Contains(x.ID)).Select(a => new ApplicationUserMenuVM
            {
                MenuName = a.Name,
                RedirectURL = a.Url


            });

            return PartialView("~/Views/HomeUI/FeedPartial/_navbar.cshtml", model);
        }


        public string GetUserTypeString(byte userTypeID)
        {
            switch (userTypeID)
            {
                case 1:
                    return SiteLanguage.Developer;
                case 2:
                    return SiteLanguage.Financier;
                case 3:
                    return SiteLanguage.Entrepreneur;
                default:
                    return null;
            }
        }

        [HttpGet]
        public PartialViewResult GetInvestedProjects()
        {
            var model = services.projectRepo.Where(x => x.IsInvested == true).Select(z => new InvestedProjectVM
            {

                ProjectCode = z.ProjectCode,
                ProjectSlug = z.ProjectSlugify,
                ProjectName = z.ProjectName,
                ProjectProfilePhoto = z.ProjectProfileLogo,
                SalesPitch = z.SalesPitch,
                CreateDate = z.CreateDate

            }).OrderByDescending(x => x.CreateDate).Take(10).ToList();

            return PartialView("~/Views/Shared/Partial/_InvestedProject.cshtml", model);
        }

        [HttpGet]
        public PartialViewResult GetLastestLaunch()
        {
            var model = services.projectLaunchRepo.
                ToList().
                Select(f => new LastestLaunchVM
                {
                    Information = f.Information,
                    LaunchProfilePhoto = f.MediaPath,
                    PostDate = f.PostDate,
                    ProjectID = f.ProjectID

                }).OrderBy(x => x.PostDate).Take(10).ToList();

            model.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            model.ForEach(a => a.ProjectSlug = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectSlugify);

            model.ForEach(a => a.ProjectCode = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectCode);

            return PartialView("~/Views/Shared/Partial/_lastestLaunch.cshtml", model);
        }

        public string GetShareTypeTextByLanguage(byte shareTypeID)
        {
            switch (shareTypeID)
            {
                case 1:
                    return SiteLanguage._AROUNDME;
                case 2:
                    return SiteLanguage._IDEA;
                case 3:
                    return SiteLanguage._STORY_TELLING;
                case 4:
                    return SiteLanguage._FEEDBACK;
                case 5:
                    return SiteLanguage._LAUNCH;
                case 6:
                    return SiteLanguage._COMMUNITY;
                default:
                    return null;
            }
        }


        [OverrideActionFilters]
        public ActionResult ChangeLanguage(string language = "en-US")
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            HttpCookie cookie = new HttpCookie("LanguageInfo");
            cookie.Value = language;
            Response.Cookies.Add(cookie);

            return Redirect("/home");
        }

        protected override void Dispose(bool disposing)
        {
            services.Dispose(disposing);
        }
    }
}