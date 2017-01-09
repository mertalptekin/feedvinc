using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminProjectController : AdminBaseController
    {
        // GET: Admin/AdminProject
        public ActionResult Index()
        {

            var model = services.projectRepo.ToList().Select(a => new AdminProjectVM
            {
                ProjectName = a.ProjectName,
                ProjectPhaseID = a.ProjectPhaseID,
                ProjectLogo = a.ProjectProfileLogo,
                ProjectMissionLevel = a.ProjectLevel,
               ProjectOwnerID = a.UserID,
               ProjectCategoryID = a.ProjectCategoryID,
               IsInvested = a.IsInvested,
               ProjectID = a.ID

            }).ToList();


            model.ForEach(a => a.ProjectOwner = services.appUserRepo.Where(y => y.ID == a.ProjectOwnerID).Select(z => new ProjectOwnerVM
            {
                FullName = z.Name + " " + z.SurName
            }).FirstOrDefault());

            model.ForEach(a => a.ProjectCategoryName = services.projectCategoryRepo.FirstOrDefault(f => f.ID == a.ProjectCategoryID).CategoryName);

            model.ForEach(a => a.ProjectMissionName = services.projectTaskTypeRepo.FirstOrDefault(z => z.ID == a.ProjectPhaseID).Name);


            return View(model);
        }

        [HttpGet]
        public ActionResult AddInvestmentNews(int id)
        {

            var project = services.projectRepo.FirstOrDefault(x => x.ID == id);


            var news = services.InvestmentNewsShareRepo.FirstOrDefault(x => x.ProjectID == id);

            var model = new InvestmentShareVM
            {
                ProjectID = id,
                ProjectName = project.ProjectName,
                ProjectProfileLogo = project.ProjectProfileLogo,
                ShareCount = 0,
                Currency = news==null ? "" : news.Currency,
                InvestmentPrice = news==null ?  0.0M: news.InvestmentPrice,
                InvestmentShareText = news==null ? "" : news.InvestmentShareText,
                PrettyDate = news == null ? DateTime.Now : news.PrettyDate

            };


            return View(model);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult AddInvestmentNews(InvestmentShareVM model)
        {

            if (ModelState.IsValid)
            {

                if (services.InvestmentNewsShareRepo.Any(x=> x.ProjectID==model.ProjectID))
                {
                    var _entity = services.InvestmentNewsShareRepo.FirstOrDefault(x => x.ProjectID == model.ProjectID);
                    _entity.ProjectName = model.ProjectName;
                    _entity.ProjectProfileLogo = model.ProjectProfileLogo;
                    _entity.PrettyDate = DateTime.Now;
                    _entity.InvestmentPrice = model.InvestmentPrice;
                    _entity.Currency = model.Currency;
                    _entity.InvestmentShareText = model.InvestmentShareText;
                    _entity.ShareCount = 0;
                    _entity.IsActive = true;

                    services.Commit();

                    ViewBag.IsSuccess = true;
                    ViewBag.Message = "Yatırım Haberi Güncellendi!";

                }
                else
                {
                    InvestmentNewsShare entity = new InvestmentNewsShare();
                    entity.ProjectName = model.ProjectName;
                    entity.ProjectProfileLogo = model.ProjectProfileLogo;
                    entity.PrettyDate = DateTime.Now;
                    entity.InvestmentPrice = model.InvestmentPrice;
                    entity.Currency = model.Currency;
                    entity.InvestmentShareText = model.InvestmentShareText;
                    entity.ShareCount = 0;
                    entity.IsActive = true;
                    services.InvestmentNewsShareRepo.Add(entity);
                    services.Commit();

                    ViewBag.IsSuccess = true;
                    ViewBag.Message = "Yatırım Haberi Girişi Yapıldı";
                }
                

                return View(model);
            }

            return View(model);

        }
    }
}