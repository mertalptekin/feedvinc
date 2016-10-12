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
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AppUserUIController : BaseUIController
    {


        [HttpGet]
        public ActionResult Edit(string username)
        {
            var user = services.appUserRepo.Where(x => x.UserSlugify == username).Select(a => new EditUserProfile
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

            if (model.profile_picture == null && model.ProfilePhotoPath==null)
                ModelState.AddModelError("ProfilePhotoPath", SiteLanguage.Profile_picture_validation);

            if (ModelState.IsValid)
            {
                var entity = services.appUserRepo.FirstOrDefault(x => x.ID == model.UserID);
                entity.EmailInformationEnabled = model.EmailIsShow;
                entity.CompanyInformation = model.CompanyInformation;
                entity.About = model.About;
                entity.PhoneNumber = model.PhoneNumber;
                entity.ProfilePhoto = model.ProfilePhotoPath==null ? MediaManagerService.Save(new MediaFormatDTO { Media = model.profile_picture, MediaType = 0 }) : model.ProfilePhotoPath;
                entity.Name = model.FullName.Split(' ')[0];
                entity.SurName = model.FullName.Split(' ')[1];
                entity.UserSlugify = model.FullName.SlugText();
                entity.CityID = model.CityID;
                entity.CountryID = model.CountryID;
                entity.BirthDate = DateTime.Parse(model.BirthDate);
                model.ProfilePhotoPath = entity.ProfilePhoto;

                services.Commit();

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.ProfileSettingsSuccess });
            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                .Select(e => new ValidationDTO { IsValid = false, ErrorMessage = e.ErrorMessage })
                                .ToList();

            return Json(errorList);
        }

        // GET: AppUserUI
        public ActionResult Profile(string username)
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