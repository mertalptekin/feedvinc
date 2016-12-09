using FeedVinc.WEB.UI.Models.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class SearchController : BaseUIController
    {
        // GET: Search
        public ActionResult Index(string searchText, int? pageIndex, int? categoryID, int? cityID, int? countryID)
        {

            ViewBag.SearchText = searchText;
            var _currentPageIndex = pageIndex ?? 0;


            #region Kullanıcı

            var userCollection = services.appUserRepo.Where(x => x.Name.Contains(searchText) || x.SurName.Contains(searchText) || x.Email.Contains(searchText)).Select(a => new SearchVM
            {
                Name = a.Name + " " + a.SurName,
                CategoryID = null,
                CityID = a.CityID,
                CountryID = a.CountryID,
                Code = a.UserCode,
                ID = a.ID,
                cssType = "user",
                Slugify = a.UserSlugify,
                Content = a.JobInformation,
                ProfilePath = a.ProfilePhoto,
                CategoryName = null

            }).Skip(_currentPageIndex * 2).Take(2).ToList();

            userCollection.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID) == null ? null : services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);

            userCollection.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID) == null ? null : services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            #endregion

            #region Proje

            var projectCollection = services.projectRepo.Where(x => x.ProjectName.Contains(searchText)).Select(a => new SearchVM
            {
                Name = a.ProjectName,
                CategoryID = a.ProjectCategoryID,
                CityID = a.CityID,
                CountryID = a.CountyID,
                Code = a.ProjectCode,
                ID = a.ID,
                cssType = "project",
                Slugify = a.ProjectSlugify,
                Content = a.SalesPitch,
                ProfilePath = a.ProjectProfileLogo

            }).Skip(_currentPageIndex * 2).Take(2).ToList();


            projectCollection.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID) == null ? null : services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);

            projectCollection.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID) == null ? null : services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            projectCollection.ForEach(a => a.CategoryName = services.projectCategoryRepo.FirstOrDefault(x => x.ID == a.CategoryID) == null ? null : services.projectCategoryRepo.FirstOrDefault(x => x.ID == a.CategoryID).CategoryName);
            #endregion

            #region Topluluk

            var communityCollection = services.communityRepo.Where(x => x.CommunityName.Contains(searchText)).Select(a => new SearchVM
            {

                Name = a.CommunityName,
                CategoryID = null,
                CityID = a.CityID,
                CountryID = a.CountryID,
                Code = a.CommunityCode,
                ID = a.ID,
                cssType = "community",
                Slugify = a.CommunitySlug,
                Content = a.CommunityObjective,
                ProfilePath = a.CommunityLogo,
                CategoryName = null


            }).Skip(_currentPageIndex * 2).Take(2).ToList();

            communityCollection.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID) == null ? null : services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);

            communityCollection.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID) == null ? null : services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);

            #endregion

            #region Reklamlar

            var addsCollection = services.projectAnnouncementRepo.Where(x => x.Description.Contains(searchText)).Select(a => new SearchVM
            {
                Name = null,
                CategoryID = null,
                CityID = a.City,
                CountryID = a.Country,
                Code = null,
                ID = a.ID,
                cssType = "ads",
                Slugify = null,
                Content = a.Description,
                ProfilePath = null,
                CategoryName = null,
                ProjectID = a.ProjectID

            }).Skip(_currentPageIndex * 2).Take(2).ToList();

            addsCollection.ForEach(a => a.Name = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID) == null ? null : services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);

            addsCollection.ForEach(a => a.ProfilePath = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID) == null ? null : services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectProfileLogo);

            addsCollection.ForEach(a => a.CityName = services.cityRepo.FirstOrDefault(x => x.ID == a.CityID) == null ? null : services.cityRepo.FirstOrDefault(x => x.ID == a.CityID).CityName);

            addsCollection.ForEach(a => a.CountryName = services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID) == null ? null : services.countryRepo.FirstOrDefault(x => x.ID == a.CountryID).CountryName);


            #endregion

            #region Union

            var model = userCollection.Union(projectCollection).Union(communityCollection).Union(addsCollection).ToList();

            #endregion

            #region Filitreleme

            if (categoryID != null && cityID != null && countryID != null)
            {
                model = model.Where(x => x.CityID == cityID && x.CategoryID == categoryID && x.CountryID == countryID).ToList();
                ViewBag.ResultCount = model.Count();
                return View(model);
            }
            else if (categoryID != null && countryID != null)
            {
                model = model.Where(x => x.CategoryID == categoryID && x.CountryID == countryID).ToList();
                ViewBag.ResultCount = model.Count();
                return View(model);
            }
            else if (categoryID != null)
            {
                model = model.Where(x => x.CategoryID == categoryID).ToList();
                ViewBag.ResultCount = model.Count();
                return View(model);
            }
            else if (countryID != null)
            {
                model = model.Where(x => x.CountryID == countryID).ToList();
                ViewBag.ResultCount = model.Count();
                return View(model);
            }
            else if (cityID != null)
            {
                model = model.Where(x => x.CityID == cityID).ToList();
                ViewBag.ResultCount = model.Count();
                return View(model);
            }


            #endregion

            ViewBag.ResultCount = model.Count();

            return View(model);
        }

       
    }
}