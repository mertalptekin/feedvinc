using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
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
            model.CommunityProfile = services.communityRepo.Where(x => x.CommunityName == communityName && x.CommunityCode == communityCode).Select(a => new CommunityProfileVM
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

            //en son yatırım alan 5 tane proje

            model.LastInvestedProjects = services.projectRepo.Where(x => x.IsInvested == true).Select(z => new InvestedProjectVM
            {

                ProjectCode = z.ProjectCode,
                ProjectSlug = z.ProjectSlugify,
                ProjectName = z.ProjectName,
                ProjectProfilePhoto = z.ProjectProfileLogo,
                SalesPitch = z.SalesPitch,
                CreateDate = z.CreateDate

            }).OrderByDescending(x => x.CreateDate).Take(10).ToList();


            //son 7 günde paylaşılan ilk 10 lansman

            model.LastLaunches = services.projectLaunchRepo.ToList().Select(f => new CommunityProfileLaunchVM
            {
                Information = f.Information,
                LaunchProfilePhoto = f.MediaPath,
                PostDate = f.PostDate,
                ProjectID = f.ProjectID

            }).OrderBy(x => x.PostDate).Take(10).ToList();

            model.LastLaunches.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            model.LastLaunches.ForEach(a => a.ProjectSlug = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectSlugify);

            model.LastLaunches.ForEach(a => a.ProjectCode = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectCode);


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
                Community = communityShareModel

            }).ToList();

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

            model.Communities = services.communityRepo.ToList().
                Select(a => new CommunityVM {

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

            model.Communities.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            model.Communities.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);

            model.InvestedProjects = services.projectRepo.Where(x => x.IsInvested == true).Select(z => new InvestedProjectVM
            {

                ProjectCode = z.ProjectCode,
                ProjectSlug = z.ProjectSlugify,
                ProjectName = z.ProjectName,
                ProjectProfilePhoto = z.ProjectProfileLogo,
                SalesPitch = z.SalesPitch,
                CreateDate = z.CreateDate

            }).OrderByDescending(x => x.CreateDate).Take(10).ToList();

            model.LastestLaunch = services.projectLaunchRepo.ToList().Select(f => new CommunityProfileLaunchVM
            {
                Information = f.Information,
                LaunchProfilePhoto = f.MediaPath,
                PostDate = f.PostDate,
                ProjectID = f.ProjectID

            }).OrderBy(x => x.PostDate).Take(10).ToList();

            model.LastestLaunch.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            model.LastestLaunch.ForEach(a => a.ProjectSlug = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectSlugify);

            model.LastestLaunch.ForEach(a => a.ProjectCode = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectCode);

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
                    CommunitySlug  = SlugIfyService.SlugText(model.CommunityName),
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
        public ActionResult CommunityEdit(string communityName,string communityCode)
        {

            var model = services.communityRepo.
                Where(x => x.CommunityName == communityName && x.CommunityCode == communityCode)
                .Select(a => new CommunityPostVM
                {
                    CommunityID = a.ID,
                    About = a.About,
                    CityID =(int)a.CityID,
                    CountryID =(int)a.CountryID,
                    CommunityProfilePhoto = a.CommunityLogo,
                    CommunityName = a.CommunityName,
                    CommunityObjective = a.CommunityObjective,
                    WebLink = a.WebLink,
                    CommunityMenu = new CommunityMenuVM { CommunityCode=communityCode, CommunitySlugify = communityName, MenuID=1  }
                    
                })
                .FirstOrDefault();

            ViewData["City"] = GetCityDropDown(model.CityID);
            ViewData["Country"] = GetCountryDropDown(model.CountryID);

            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult CommunityEdit(CommunityPostVM model)
        {

            return View();
        }

    }
}