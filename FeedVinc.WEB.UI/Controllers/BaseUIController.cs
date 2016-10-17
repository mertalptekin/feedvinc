using FeedVinc.BLL.Services;
using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
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

                switch (model.ShareTypeID)
                {
                    case 1:
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
                        break;
                    case 3:
                        model.ShareTitle = SiteLanguage._STORY_TELLING;
                        ProjectShare Projectshare = new ProjectShare
                        {
                            Location = model.Location,
                            Content = model.Post,
                            ShareTypeID = model.ShareTypeID,
                            IsActive = true,
                            SharePath = model.MediaPath,
                            MediaType = (byte)model.MediaTypeID,
                            ShareDate = DateTime.Now,
                            ProjectID = 1
                           
                        };
                        services.projectShareRepo.Add(Projectshare);
                        break;
                    default:
                        break;
                }

                int ID = services.Commit();


                SharePostDTO dto = new SharePostDTO
                {
                    FeedID = ID,
                    Post = model.Post,
                    Location = model.Location,
                    MediaPath = model.MediaPath,
                    MediaTypeID = model.MediaTypeID,
                    ShareTypeID = model.ShareTypeID,
                    ShareTitle = SiteLanguage.Around_Me,
                    User = _currentUser,
                    ProjectShare = services.projectRepo.Where(x=> x.ID==ID).Select(a=> new ProjectSharePostDTO {

                        ProjectName = a.ProjectName,
                        ProjectProfilePath = a.ProjectProfileLogo,
                        ProjectSlugify = a.ProjectSlugify,
                        ProjectID = a.ID

                    }).FirstOrDefault(),
                    PrettyDate = DateTimeService.GetPrettyDate(DateTime.Now,LanguageService.getCurrentLanguage),
                    Validation = new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Shared_your_Post }
                };

                return PartialView("~/Views/HomeUI/FeedPartial/_feed_around_me.cshtml", dto);

            }


            return Json(new ValidationDTO { IsValid=false,ErrorMessage=SiteLanguage.Post_Validation });
            
        }

        public IEnumerable<SelectListItem> GetProjectCategoryDropDown(byte? selectedCategoryID=0)
        {
            var model = services.projectCategoryRepo.Where(x=> x.Lang==LanguageService.getCurrentLanguage).Select(a => new SelectListItem { Text = a.CategoryName, Value = a.ID.ToString(), Selected = a.ID == selectedCategoryID ? true: false }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_Category_Validation, Value = "0", Selected = selectedCategoryID!=null ? true:false });

            return model.OrderBy(x => x.Value); 
        }

        public IEnumerable<SelectListItem> GetCountryDropDown(int? selectedCountryID=0)
        {
            var model = services.countryRepo.ToList().Select(a => new SelectListItem { Text = a.CountryName, Value = a.ID.ToString(), Selected = (a.ID == selectedCountryID ? true : false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_County_Validation, Value = "0", Selected = selectedCountryID==null ? true: false });

            return model.OrderBy(x => x.Value);
        }

        public IEnumerable<SelectListItem> GetCityDropDown(int? countryID,int? selectedCityID=0)
        {
            var model = services.cityRepo.Where(x=> x.CountryID==countryID).Select(a => new SelectListItem { Text = a.CityName, Value = a.ID.ToString(), Selected = (a.ID==(int)selectedCityID  ? true:false) }).ToList();

            model.Add(new SelectListItem { Text = SiteLanguage.Project_City_Validation, Value = "0", Selected = (selectedCityID==null ? true:false) });

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