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
                            MediaType = model.MediaTypeID
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
                            MediaType = (byte)model.MediaTypeID
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
                    PrettyDate = DateTimeService.GetPrettyDate(DateTime.Now,LanguageService.getCurrentLanguage),
                    Validation = new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Shared_your_Post }
                };

                return PartialView("~/Views/HomeUI/FeedPartial/_feed_around_me.cshtml", dto);

            }


            return Json(new ValidationDTO { IsValid=false,ErrorMessage=SiteLanguage.Post_Validation });
            
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