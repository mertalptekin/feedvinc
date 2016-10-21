using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Project;
using FeedVinc.WEB.UI.Models.ViewModels.UserProfile;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AppUserUIController : BaseUIController
    {

        public ActionResult AccountSettings()
        {
            ViewBag.MenuID = 3;

            return View();
        }

        [HttpPost]
        public JsonResult AccountSettings(EditPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var entity = services.appUserRepo.FirstOrDefault(x => x.ID == UserManagerService.CurrentUser.ID);
                entity.Password = model.Password;
                services.Commit();

                return Json(new ValidationDTO { RedirectURL = "/logout", IsValid = true, SuccessMessage = SiteLanguage.ProfileSettingsSuccess });
            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => new ValidationDTO { IsValid = false, ErrorMessage = e.ErrorMessage })
                                 .ToList();
            return Json(errorList);
        }

        public ActionResult EmailSettings()
        {
            ViewBag.MenuID = 2;

            var model = services.appUserRepo.Where(x => x.ID == UserManagerService.CurrentUser.ID).Select(a => new EmailSettingsVM
            {
                AccountEmailNotificationEnabled = a.AccountInformationEnabled,
                EmailNotificationEnabled = a.EmailInformationEnabled,
                Email = a.Email

            }).FirstOrDefault();

            return View(model);
        }

        [HttpPost]
        public JsonResult EmailSettings(EmailSettingsVM model)
        {

            if (ModelState.IsValid)
            {
                var entity = services.appUserRepo.FirstOrDefault(x => x.ID == UserManagerService.CurrentUser.ID);
                string oldEmail = entity.Email;

                if (entity.Email == model.Email)
                {
                    entity.EmailInformationEnabled = model.EmailNotificationEnabled;
                    entity.AccountInformationEnabled = model.AccountEmailNotificationEnabled;
                    services.Commit();

                    return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.EmailSettingsSuccess });
                }

                entity.IsActive = false;
                entity.Email = model.Email;
                entity.OldEmail = oldEmail;
                services.Commit();

                string subject = "FeedVinc | " + SiteLanguage.Email_Change;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Template/activate.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{HELLO}", SiteLanguage.Email_Hello);
                body = body.Replace("{WELCOME}", "");
                body = body.Replace("{CONTENT}", SiteLanguage.Email_Activation_Body);
                body = body.Replace("{WARNING}", SiteLanguage.Email_Activation_Warning);
                body = body.Replace("{URL}", "http://feedvinc.workstudyo.com/activate-account/" + entity.UserGUID);
                body = body.Replace("{NAME}", entity.Name + " " + entity.SurName);
                body = body.Replace("{LINK}", SiteLanguage.Activate_Link);

                List<MailAddress> toList = new List<MailAddress>();
                toList.Add(new MailAddress(model.Email, entity.Name, System.Text.Encoding.UTF8));
                toList.Add(new MailAddress(oldEmail, entity.Name, System.Text.Encoding.UTF8));

                string logoPath = Server.MapPath(@"~/Content/Template/FeedVinc_Logo.png");

                EmailService.SendMail(toList, null, null, subject, body, logoPath);

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.EmailResetMessage, RedirectURL = "/logout" });
            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => new ValidationDTO { IsValid = false, ErrorMessage = e.ErrorMessage })
                                 .ToList();

            return Json(errorList);

        }

        [HttpGet]
        public ActionResult Edit()
        {
            ViewBag.MenuID = 1;

            var user = services.appUserRepo.Where(x => x.ID == UserManagerService.CurrentUser.ID).Select(a => new EditUserProfile
            {

                UserID = a.ID,
                UserTypeID = a.UserTypeID,
                FullName = a.Name + " " + a.SurName,
                About = a.About,
                BirthDate = a.BirthDate.ToString(),
                CityID = a.CityID,
                CountryID = a.CountryID,
                CompanyInformation = a.CompanyInformation,
                EmailIsShow = a.EmailInformationEnabled,
                PhoneNumber = a.PhoneNumber,
                ProfilePhotoPath = a.ProfilePhoto

            }).FirstOrDefault();

            return View(user);
        }


        [HttpPost]
        public JsonResult Edit(EditUserProfile model)
        {

            if (!UserManagerService.UserNameIsCorrectFormat(model.FullName))
            {
                ModelState.AddModelError("FullNameCorrectFormat", SiteLanguage.FullName_Pattern_Error);
                ModelState.AddModelError("FullName", SiteLanguage.FullName_Validation);
            }

            if (!UserManagerService.UserNameIsUnique(model.FullName, model.UserID))
                ModelState.AddModelError("UserNameIsNotValid", SiteLanguage.UserNameIsUniqueValidation);

            if (model.profile_picture == null && model.ProfilePhotoPath == null)
                ModelState.AddModelError("ProfilePhotoPath", SiteLanguage.Profile_picture_validation);

            if (ModelState.IsValid)
            {
                var entity = services.appUserRepo.FirstOrDefault(x => x.ID == model.UserID);
                entity.EmailInformationEnabled = model.EmailIsShow;
                entity.CompanyInformation = model.CompanyInformation;
                entity.About = model.About;
                entity.PhoneNumber = model.PhoneNumber;
                entity.ProfilePhoto = model.ProfilePhotoPath == null ? MediaManagerService.Save(new MediaFormatDTO { Media = model.profile_picture, MediaType = 0 }) : model.ProfilePhotoPath;
                entity.Name = model.FullName.Split(' ')[0];
                entity.SurName = model.FullName.Split(' ')[1];
                entity.UserSlugify = model.FullName.SlugText();
                entity.CityID = model.CityID;
                entity.CountryID = model.CountryID;
                entity.BirthDate = DateTime.Parse(model.BirthDate);

                services.Commit();

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.ProfileSettingsSuccess });
            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                .Select(e => new ValidationDTO { IsValid = false, ErrorMessage = e.ErrorMessage })
                                .ToList();

            return Json(errorList);
        }

        // GET: AppUserUI
        public ActionResult Profile(string username,string userCode)
        {
            UserProfileVM model = new UserProfileVM();
            model.User = services.appUserRepo.Where(x => x.UserSlugify == username).Select(a => new UserVM
            {
                FullName = a.Name + " " + a.SurName,
                About = a.About,
                BirthDate = a.BirthDate,
                AccountInformationEnabled = a.AccountInformationEnabled,
                Company = a.CompanyInformation,
                PhoneNumber = a.PhoneNumber,
                ProfilePhoto = a.ProfilePhoto,
                Description = a.UserInformation,
                UserTypeID = a.UserTypeID,
                ID = a.ID,
                CityID = a.CityID,
                CountryID = a.CountryID

            }).FirstOrDefault();

            model.User.UserTypeText = GetUserTypeString(model.User.UserTypeID);

            model.UserProjects = services.projectRepo.Where(x => x.UserID == model.User.ID).Select(a => new ProjectVM
            {
                ProjectName = a.ProjectName,
                ProjectProfileLogo = a.ProjectProfileLogo,
                ProjectSalesPitch = a.SalesPitch,
                ProjectID = a.ID,
                CategoryID = a.ProjectCategoryID

            }).OrderByDescending(x => x.CreateDate);

            model.UserProjects.ToList().ForEach(a => a.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(y => y.ID == a.CategoryID).CategoryName);


            model.UserShares = services.appUserShareRepo.Where(x => x.UserID == model.User.ID).Select(a => new ShareVM
            {
                User = model.User,
                Post = a.Content,
                PostDate = a.ShareDate,
                PostMediaPath = a.SharePath,
                ShareCount = 0,
                LikeCount = 0,
                CommentCount = 0,
                Location = a.Location,
                ProfilePhotoPath = model.User.ProfilePhoto,
                ShareTypeID = (byte)a.ShareTypeID,
                PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate, LanguageService.getCurrentLanguage),
                ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                MediaTypeID = a.MediaType

            }).ToList();

            return View(model);
        }
    }
}