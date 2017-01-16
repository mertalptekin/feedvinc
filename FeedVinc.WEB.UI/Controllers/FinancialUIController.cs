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
using FeedVinc.DAL.ORM.Entities;

namespace FeedVinc.WEB.UI.Controllers
{
    public class FinancialUIController : BaseUIController
    {

        public ActionResult ShowReport(int id)
        {
            var response = services.ProjectReportResponseRepo.FirstOrDefault(x => x.ProjectID == id && x.InvesterID==UserManagerService.CurrentUser.ID);


           var _project = services.projectRepo.FirstOrDefault(x => x.ID == id);

            ViewBag.ProjectName = _project.ProjectName;
            ViewBag.ProjectPhoto = _project.ProjectProfileLogo;


            var answer = services.AnswerFinancialRequestRepo.Where(x => x.ID == response.AnswerFinancialRequestID).Select(z=> new AnswerFinancialRequestDTO {

                InvestmentStatus = z.InvestmentStatus,
                FuturePlans = z.FuturePlans,
                ProfitabilityPurchased = z.ProfitabilityPurchased,
                CustomerCost = z.CustomerCost,
                SecondChance = z.SecondChance,
                ProfitabilityRatio = z.ProfitabilityRatio,
                CashRatio = z.CashRatio,
                FinancialHoisting = z.FinancialHoisting,
                ValueRatio = z.ValueRatio,
                ProductivityRate = z.ProductivityRate,
                PurchaseCost = z.PurchaseCost

            }).FirstOrDefault();


            return View(answer);
        }



        public ActionResult AnswerFinancialRequest(long ProjectID, long InvesterID)
        {

            ViewBag.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == ProjectID).ProjectName;
            ViewBag.ProjectID = ProjectID;
            ViewBag.InvesterID = InvesterID;



            var invester = services.appUserRepo.FirstOrDefault(x => x.ID == InvesterID);

            ViewBag.InvesterName = invester.Name + " " + invester.SurName;
            ViewBag.InvesterProfilePhoto = invester.ProfilePhoto;
            ViewBag.InvesterProfileLink = "/profile/" + invester.UserSlugify + "/" + invester.UserCode;

            return View();
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult AnswerFinancialRequest(AnswerFinancialRequestDTO model)
        {

            if (ModelState.IsValid)
            {
                var entity = new AnswerFinancialRequest()
                {
                    InvestmentStatus = model.InvestmentStatus,
                    FuturePlans = model.FuturePlans,
                    ProfitabilityPurchased = model.ProfitabilityPurchased,
                    CustomerCost = model.CustomerCost,
                    SecondChance = model.SecondChance,
                    ProfitabilityRatio = model.ProfitabilityRatio,
                    CashRatio = model.CashRatio,
                    FinancialHoisting = model.FinancialHoisting,
                    ValueRatio = model.ValueRatio,
                    ProductivityRate = model.ProductivityRate,
                    PurchaseCost = model.PurchaseCost
                };

                services.AnswerFinancialRequestRepo.Add(entity);
                services.Commit();

                var _e = new ProjectFinancialReportResponse()
                {
                    AnswerFinancialRequestID = entity.ID,
                    InvesterID = model.InvestorID,
                    ProjectID = model.ProjectID
                };

                services.ProjectReportResponseRepo.Add(_e);
                services.Commit();


                ViewBag.Message = "Yatırımcıya Rapor Gönderildi";
                ViewBag.IsSuccess = true;

                return View(model);
            }

            ViewBag.IsSuccess = false;

            return View(model);
        }


        public ActionResult EntrepreneurRequest()
        {

            var projectIDs = services.projectRepo.Where(y => y.UserID == UserManagerService.CurrentUser.ID).Select(z => z.ID).ToList();

            var model = services.ProjectReportRepo.Where(x => projectIDs.Contains(x.ProjectID)).Select(a => new EntrepreneurRequestVM
            {
                InvesterID = a.InvestorID,
                ProjectID = a.ProjectID

            }).ToList();


            model.ForEach(y => y.ProjectName = services.projectRepo.FirstOrDefault(k => k.ID == y.ProjectID).ProjectName);

            model.ForEach(y => y.InvesterName = services.appUserRepo.FirstOrDefault(k => k.ID == y.InvesterID).Name + " " + services.appUserRepo.FirstOrDefault(m=> m.ID==y.InvesterID).SurName );

            return View(model);

        }


        public ActionResult InvesterRequest()
        {
            var model = new RequestVM();


            //gelen istekler

            model.Responses = services.ProjectReportResponseRepo.Where(x => x.InvesterID == UserManagerService.CurrentUser.ID).Select(k => new InvesterRequestVM
            {
                ProjectID = k.ProjectID

            }).ToList();

            model.Responses.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(t => t.ID == a.ProjectID).ProjectName);

            //gönderilenler

            model.SendRequests = services.ProjectReportRepo.Where(x => x.InvestorID == UserManagerService.CurrentUser.ID).Select(g => new EntrepreneurRequestVM
            {
                ProjectID = g.ProjectID

            }).ToList();

            model.SendRequests.ForEach(u => u.ProjectName = services.projectRepo.FirstOrDefault(o => o.ID == u.ProjectID).ProjectName);


            return View(model);

        }

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
                        CategoryID = a.ProjectCategoryID,
                        ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID


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
                    CategoryID = a.ProjectCategoryID,
                    ID = a.ID

                }).ToList();

                model.ForEach(y => y.CityName = services.cityRepo.FirstOrDefault(z => z.ID == y.CityID).CityName);

                model.ForEach(y => y.CountryName = services.countryRepo.FirstOrDefault(z => z.ID == y.CountryID).CountryName);

                model.ForEach(y => y.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(z => z.ID == y.CategoryID).CategoryName);

               var vm = model.ToPagedList(pageNumber, 4);

                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult SendRequest(long projectID,long investerID)
        {

            ViewBag.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == projectID).ProjectName;
            ViewBag.ProjectID = projectID;
            ViewBag.InvesterID = investerID;

            return View();
        }


        [HttpPost]
        public JsonResult SendRequest(ReportRequestVM model)
        {
            if (!services.ProjectReportRepo.Any(x=> x.ProjectID==model.ProjectID && x.InvestorID==model.InvestorID))
            {
                ProjectFinancialReportRequest entity = new ProjectFinancialReportRequest();
                entity.InvestorID = model.InvestorID;
                entity.ProjectID = model.ProjectID;

                services.ProjectReportRepo.Add(entity);
                services.Commit();

                return Json(SiteLanguage.ReportRequest);
            }


            return Json(SiteLanguage.ReportRequestIsExist);
           
        }
    }
}