﻿using FeedVinc.BLL.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Attributes;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    [LoginControl]
    public class BaseUIController : Controller
    {
        protected UnitOfWork services;

        public BaseUIController()
        {
            services = new UnitOfWork();
        }

        [HttpPost]
        public JsonResult Share(SharePostVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.MediaPhoto != null || model.MediaVideo != null)
                {
                    MediaFormatDTO mediaDTO = new MediaFormatDTO
                    {
                        MediaType = model.MediaPhoto != null ? 0 : 1,
                        Media = model.MediaPhoto != null ? model.MediaPhoto : model.MediaVideo,
                        FolderName = UserManagerService.CurrentUser.SurName + " " + UserManagerService.CurrentUser.Name + RandomCodeGenerator.Generate()
                    };

                    model.MediaPath = MediaManagerService.Save(mediaDTO);

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
                        break;
                    default:
                        break;
                }

                services.Commit();

                SharePostDTO dto = new SharePostDTO
                {
                    Post = model.Post,
                    Location = model.Location,
                    MediaPath = model.MediaPath,
                    MediaTypeID = model.MediaTypeID,
                    ShareTypeID = model.ShareTypeID,
                    ShareTitle = SiteLanguage.Around_Me,
                    Validation = new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Shared_your_Post }
                };

                return Json(dto);

            }


            return Json(new ValidationDTO { IsValid=false,ErrorMessage=SiteLanguage.Post_Validation });
            
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