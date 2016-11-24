using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels;
using FeedVinc.WEB.UI.Models.ViewModels.Community;
using FeedVinc.WEB.UI.Models.ViewModels.Community.Profile;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class CommunityUIController : BaseUIController
    {
        // GET: CommunityUI
        public ActionResult CommunityProfile(string communityName, string communityCode)
        {
            var model = new CommunityProfileWrapperVM();
            //
            model.CommunityProfile = services.communityRepo.Where(x => x.CommunitySlug == communityName && x.CommunityCode == communityCode).Select(a => new CommunityProfileVM
            {
                CommunityID = a.ID,
                CommunityName = a.CommunityName,
                CommunityInformation = a.CommunityObjective,
                CommunityCode = a.CommunityCode,
                CommunitySlug = a.CommunitySlug,
                CommunityLink = a.WebLink,
                MemberCount = 0,
                CommunityProfilePhoto = a.CommunityLogo,
                CountryID = a.CountryID,
                CityID = a.CityID,
                OwnerID = a.OwnerID,
                About = a.About

            }).FirstOrDefault();

            model.CommunityProfile.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == model.CommunityProfile.CountryID).CountryName;

            model.CommunityProfile.CityName = services.cityRepo.FirstOrDefault(x => x.ID == model.CommunityProfile.CityID).CityName;

            var communityMemberIDs = services.communityUserRepo.Where(x => x.CommunityID == model.CommunityProfile.CommunityID).Select(c => c.UserID);

            //topluluğa üye olan kullanıcıların bilgileri
            model.CommunityMembers = services.appUserRepo.Where(x => communityMemberIDs.Contains(x.ID) && model.CommunityProfile.OwnerID == x.ID).Select(a => new CommunityMemberVM
            {
                MemberName = a.Name + " " + a.SurName,
                MemberProfilePhoto = a.ProfilePhoto,
                MemberSlugify = a.UserSlugify,
                MemberCode = a.MemberCode,
                MemberID = a.ID,
                MemberType = GetUserTypeString(a.UserTypeID)

            }).ToList();

            //topluluğa üye olan kullanıcıların topluluğun yöneticisi olup olmadığı
            model.CommunityMembers.ForEach(a => a.IsAdmin = services.communityUserRepo.FirstOrDefault(x => x.UserID == a.MemberID).IsAdmin);

            //topluluğa üye olan kullanıcıların yer aldıkları projeler
            model.CommunityMembers.ForEach(a => a.MemberProjectNames = services.projectRepo.Where(x => x.UserID == a.MemberID).Select(z => z.ProjectName).ToList());

            model.CommunityProjects = services.projectRepo.Where(x => communityMemberIDs.Contains(x.UserID) && model.CommunityProfile.OwnerID == x.ID).Select(z => new CommunityProjectVM
            {
                ProjectID = z.ID,
                ProjectName = z.ProjectName,
                ProjectProfilePhoto = z.ProjectProfileLogo,
                ProjectCode = z.ProjectCode,
                ProjectSlug = z.ProjectSlugify,
                SalesPitch = z.SalesPitch,
                ProjectCategoryID = z.ProjectCategoryID

            }).Distinct().ToList();

            //bu topluluktakilerin yer aldığı projelerin bilgisi

            model.CommunityProjects.ForEach(f => f.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(x => x.ID == f.ProjectCategoryID).CategoryName);

            var communityShareModel = new CommunityShareVM
            {
                OwnerID = model.CommunityProfile.OwnerID,
                CommunityID = model.CommunityProfile.CommunityID,
                CommunityName = model.CommunityProfile.CommunityName,
                ProfilePhotoLogo = model.CommunityProfile.CommunityProfilePhoto
            };

            model.CommunityFeeds = services.communityShareRepo.Where(x => x.CommunityID == model.CommunityProfile.CommunityID).Select(z => new ShareVM
            {
                CommentCount = 0,
                ShareCount = 0,
                LikeCount = 0,
                CommunityID = z.CommunityID,
                ShareTypeID = (byte)z.ShareTypeID,
                MediaTypeID = z.MediaType,
                PostMediaPath = z.SharePath,
                Post = z.Content,
                ShareTypeText = GetShareTypeTextByLanguage((byte)z.ShareTypeID),
                PrettyDate = DateTimeService.GetPrettyDate(z.ShareDate, LanguageService.getCurrentLanguage),
                Location = z.Location,
                Community = communityShareModel,
                ShareID = z.ID,
                UserID = model.CommunityProfile.OwnerID

            }).ToList();

            model
                .CommunityFeeds
                .ForEach(a => a.LikeCount = services.communityShareLikeRepo.Count(x => x.CommunityShareID == a.ShareID));

            model.CommunityFeeds
                .ForEach(a => a.CommentCount = services.communityShareCommentRepo.Count(x => x.CommunityShareID == a.ShareID));

            model
                .CommunityFeeds
                .ForEach(a => a.LikedCurrentUser = services.communityShareLikeRepo
                .Any(x => x.CommunityShareID == a.ShareID && x.UserID == _currentUser.ID));

            model.CommunityFeeds.ForEach(a => a.ShareComments = GetCommentsByShareID(a.ShareID,"community").ShareComments);

            return View(model);
        }

        public ActionResult CommunityAdd()
        {
            ViewData["City"] = GetCityDropDown(1);
            ViewData["Country"] = GetCountryDropDown();

            return View(new CommunityPostVM());
        }

        [HttpGet]
        public ActionResult CommunityList()
        {
            var model = new CommunityListVM();
            ViewBag.CurrentUserID = _currentUser.ID;

            model.Communities = services.communityRepo.ToList().
                Select(a => new CommunityVM
                {

                    CommunityCode = a.CommunityCode,
                    CommunityID = a.ID,
                    CommunityName = a.CommunityName,
                    CommunityObjective = a.CommunityObjective,
                    CommunitySlug = a.CommunitySlug,
                    CommunityProfilePhoto = a.CommunityLogo,
                    CountryID = a.CountryID,
                    CityID = a.CityID
                   

                }).
                OrderBy(x => x.CreateDate).
                Take(10).
                ToList();

            model.Communities.ForEach(a => a.Joined = services.communityUserRepo.Any(f => f.UserID == _currentUser.ID && f.CommunityID==a.CommunityID));

            model.Communities.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            model.Communities.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);




            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommunityAdd(CommunityPostVM model)
        {

            ViewData["City"] = GetCityDropDown(1);
            ViewData["Country"] = GetCountryDropDown();

            if (ModelState.IsValid)
            {
                var entity = new Community
                {
                    CommunityName = model.CommunityName,
                    CommunityObjective = model.CommunityObjective,
                    CommunityLogo = MediaManagerService.Save(new Models.DTO.MediaFormatDTO { Media = model.CommunityPhoto, MediaType = 0 }),
                    CommunityCode = RandomCodeGenerator.Generate(),
                    CommunitySlug = SlugIfyService.SlugText(model.CommunityName),
                    CityID = model.CityID,
                    CountryID = model.CountryID,
                    IsActive = true,
                    About = model.About,
                    OwnerID = UserManagerService.CurrentUser.ID,
                    WebLink = model.WebLink

                };

                model.CommunityProfilePhoto = entity.CommunityLogo;

                services.communityRepo.Add(entity);
                services.Commit();

                ViewBag.Success = SiteLanguage.Community_Success;
                ViewBag.IsSuccess = true;

                return View(model);

            }

            return View(model);
        }


        [HttpPost]
        public JsonResult AddCommunityManager(CommunityAdminAddVM model)
        {
            

            var communityAdminIDs = services.communityUserRepo.Where(x => x.CommunityID == model.CommunityID).Select(a=> a.UserID).ToList();

            var user = services.appUserRepo.Where(a => a.IsActive && a.Email == model.AddMemberEmail && !communityAdminIDs.Contains(a.ID)).Select(c => new CommunityManagerUserVM
            {
                UserID = c.ID,
                UserName = c.Name + " " + c.SurName,
                ProfilePhoto = c.ProfilePhoto,
                UserJobType = c.JobInformation,
                UserSlugify = c.UserSlugify,
                UserCode = c.UserCode

            }).FirstOrDefault();

            if (user != null)
            {
                var entity = new CommunityUser {
                    UserID = user.UserID,
                    CommunityID = (int)model.CommunityID,
                    IsActive = true,
                    IsAdmin=true
                };

                services.communityUserRepo.Add(entity);
                services.Commit();

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Community_Admin_Add_Success, Data = user });
            }
            else
            {
                return Json(new ValidationDTO { IsValid = false, ErrorMessage = SiteLanguage.ProjectTeamAdd_Error, Data = user });
            }

        }

        [HttpGet]
        public ActionResult CommunityManagerEdit(string communityName, string CommunityCode)
        {
            var community = services.communityRepo.FirstOrDefault(x => x.CommunitySlug == communityName && x.CommunityCode == CommunityCode);

            var owner = services.appUserRepo.FirstOrDefault(x => x.ID == community.OwnerID);


            var communityAdminIds = services.communityUserRepo.Where(x => x.CommunityID == community.ID && x.IsAdmin).Select(a => a.UserID);


            var communityManagers = services.appUserRepo.Where(x => communityAdminIds.Contains(x.ID)).
                Select(a => new CommunityManagerUserVM
                {
                    ProfilePhoto = a.ProfilePhoto,
                    UserID = a.ID,
                    UserSlugify = a.UserSlugify,
                    UserJobType = a.JobInformation,
                    UserName = a.Name + " " + a.SurName

                }).
            ToList();

            var communityAdminsDrp = communityManagers
                .Select(a => new SelectListItem
                {
                    Value = a.UserID.ToString(),
                    Text = a.UserName,
                    Selected = false
                }).
                ToList();

            communityAdminsDrp.Add(new SelectListItem { Text = owner.Name + " " + owner.SurName, Value = owner.ID.ToString(), Selected = true });


            ViewData["CommunityManagerDropDown"] = communityAdminsDrp;

            var model = new CommunityManagerTeamVM
            {
                CommunityAdmins = communityManagers,
                CommunityID = community.ID,
                OwnerID = owner.ID,
                Menu = new CommunityMenuVM { MenuID = 2, CommunityCode = community.CommunityCode, CommunitySlugify = community.CommunitySlug }
            };

            return View(model);
        }


        public ActionResult CommunityEdit(string communityName, string communityCode)
        {

            var model = services.communityRepo.
                Where(x => x.CommunitySlug == communityName && x.CommunityCode == communityCode)
                .Select(a => new CommunityPostVM
                {
                    CommunityID = a.ID,
                    About = a.About,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountryID,
                    CommunityProfilePhoto = a.CommunityLogo,
                    CommunityName = a.CommunityName,
                    CommunityObjective = a.CommunityObjective,
                    WebLink = a.WebLink,
                    CommunityMenu = new CommunityMenuVM { CommunityCode = communityCode, CommunitySlugify = communityName, MenuID = 1 }

                })
                .FirstOrDefault();

            ViewData["City"] = GetCityDropDown(model.CityID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CommunityEdit(CommunityPostVM model)
        {
            ViewData["City"] = GetCityDropDown(model.CityID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);

            var entity = services.communityRepo.FirstOrDefault(x => x.ID == model.CommunityID);

            if (model.CommunityPhoto == null)
                ModelState.Remove("ProjectPhoto");

            if (ModelState.IsValid)
            {
                entity.CommunityName = model.CommunityName;
                entity.CommunityObjective = model.CommunityObjective;
                entity.CommunityLogo = model.CommunityPhoto != null ? MediaManagerService.Save(new Models.DTO.MediaFormatDTO { Media = model.CommunityPhoto, MediaType = 0 }) : model.CommunityProfilePhoto;
                entity.CityID = model.CityID;
                entity.CountryID = model.CountryID;
                entity.About = model.About;
                entity.WebLink = model.WebLink;
                entity.IsActive = true;
                entity.OwnerID = UserManagerService.CurrentUser.ID;
                entity.CommunitySlug = SlugIfyService.SlugText(model.CommunityName);
                model.CommunityProfilePhoto = entity.CommunityLogo;

                services.Commit();
                ViewBag.Success = SiteLanguage.Community_Success;
                ViewBag.IsSuccess = true;

                model.CommunityMenu = new CommunityMenuVM { MenuID = 1, CommunityCode = entity.CommunityCode, CommunitySlugify = entity.CommunitySlug };

                return View(model);
            }

            return View(model);
        }

    }
}