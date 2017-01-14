using FeedVinc.WEB.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FeedVinc.WEB.UI.UIServices;
using FeedVinc.WEB.UI.Models.ViewModels.Financial;

namespace FeedVinc.WEB.UI.Controllers
{
    public class FinancialUIController : BaseUIController
    {
        // GET: FinancialUI
        public ActionResult Index(int? page, int? category, int? country, int? city, string searchText)
        {
            int pageNumber = page ?? 1;


            ViewData["ProjectCategory"] = GetProjectCategoryDropDown();
            ViewData["Country"] = GetCountryDropDown();
            ViewData["City"] = GetCityDropDown(1);


            ViewBag.CategoryValue = category;
            ViewBag.CountryValue = country;
            ViewBag.CityValue = city;
            ViewBag.SearchTextValue = searchText;


            if (country==0)
            {
                country = null;
            }

            if (city==0)
            {
                city = null;
            }

            if (category==0)
            {
                category = null;
            }


            if (String.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }

        

            if (category != null && city != null && country != null)
            {

                var model = services.projectRepo
                    .Where(x => x.CountyID == country && x.CityID == city && x.ProjectCategoryID == category && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                    {
                        ProjectName = a.ProjectName,
                        ProjectSlugify = a.ProjectSlugify,
                        ProjectCode = a.ProjectCode,
                        SalesPitch = a.SalesPitch,
                        ProjectProfile = a.ProjectProfileLogo,
                        CityID = (int)a.CityID,
                        CountryID = (int)a.CountyID,
                        CategoryID = a.ProjectCategoryID

                    }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);


                return View(vm);
            }
            else if (category != null && city != null && country != null)
            {
                var model = services.projectRepo.Where(x => x.CountyID == country && x.CityID == city && x.ProjectCategoryID == category && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);


                return View(vm);
            }
            else if (category != null && city != null && country != null && !string.IsNullOrEmpty(searchText))
            {
                var model = services.projectRepo.Where(x => x.CountyID == country && x.CityID == city && x.ProjectCategoryID == category && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);


                return View(vm);
            }
            else if (category != null && country != null)
            {

                var model = services.projectRepo.Where(x => x.ProjectCategoryID == category && x.CountyID == country && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);


                return View(vm);
            }
            else if (category != null && city != null)
            {
                var model = services.projectRepo.Where(x => x.ProjectCategoryID == category && x.CityID == city && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
            else if (country != null && city != null)
            {
                var model = services.projectRepo.Where(x => x.CountyID == country && x.CityID == city && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

               var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
            else if (category != null)
            {
                var model = services.projectRepo.Where(x => x.ProjectCategoryID == category && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
            else if (city != null)
            {
                var model = services.projectRepo.Where(x => x.CityID == city && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

               var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
            else if (country != null)
            {
                var model = services.projectRepo.Where(x => x.CountyID == country && x.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

                var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
            else
            {
                var model = services.projectRepo.Where(y=> y.ProjectName.Contains(searchText)).Select(a => new FinancialProjectVM
                {
                    ProjectName = a.ProjectName,
                    ProjectSlugify = a.ProjectSlugify,
                    ProjectCode = a.ProjectCode,
                    SalesPitch = a.SalesPitch,
                    ProjectProfile = a.ProjectProfileLogo,
                    CityID = (int)a.CityID,
                    CountryID = (int)a.CountyID,
                    CategoryID = a.ProjectCategoryID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

               var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
        }
    }
}