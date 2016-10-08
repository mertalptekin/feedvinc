using FeedVinc.Common.Services;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Models.ViewModels.Project;
using FeedVinc.WEB.UI.Models.ViewModels.UserProfile;
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
        // GET: AppUserUI
        public ActionResult Profile(string username)
        {
            UserProfileVM model = new UserProfileVM();
            model.User = UserManagerService.CurrentUser;
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
                ProfilePhotoPath =model.User.ProfilePhoto,
                ShareTypeID =(byte)a.ShareTypeID,
                PrettyDate = DateTimeService.GetPrettyDate(a.ShareDate,LanguageService.getCurrentLanguage),
                ShareTypeText = GetShareTypeTextByLanguage((byte)a.ShareTypeID),
                MediaTypeID = a.MediaType

            }).ToList();

            return View(model);
        }
    }
}