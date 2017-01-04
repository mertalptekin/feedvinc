using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.InvesterLetter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class InvestmentLetterController : BaseUIController
    {
        // GET: InvestmentLetter

       

        public ActionResult ShowInvestmentLetter()
        {
            var letterIds = services.InvestorLetterRepo.Where(x => x.InvesterID == _currentUser.ID).Select(a => a.InvestmentLetterID);

            var model = services.InvestmentLetterRepo.Where(x => letterIds.Contains(x.ID)).Select(a => new ComingInvesterLetterVM
            {
                LetterID = a.ID,
                ProjectID = a.ProjectID,
                UserID = a.UserID
 
            }).ToList();

            model.ForEach(a => a.ProjectName = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).ProjectName);
            model.ForEach(a => a.UserName = services.appUserRepo.Where(x => x.ID == a.UserID).Select(z => z.Name + " " + z.SurName).FirstOrDefault());


            return View(model);
           

        }

        public ActionResult InvestmentLetterDetail(int id)
        {

            var model = new InvestmentLetterDetailVM();

            model.InvestmentLetter = services.InvestmentLetterRepo.Where(x => x.ID == id).Select(a => new InvestmentLetterDTO
            {
                CompetitorAnalysis = a.CompetitorAnalysis,
                CustomerSegment = a.CustomerSegment,
                FinancialCondition = a.FinancialCondition,
                InvestmentExpectancy = a.InvestmentExpectancy,
                InvestmentStatus = a.InvestmentStatus, 
                MarketPotential = a.MarketPotential,
                ProjectOverview = a.ProjectOverview,
                TeamAndCollaborators = a.TeamAndCollaborators,
                ValueProposition = a.ValueProposition,
                Message = a.Message,
                ProjectID = a.ProjectID,
                OwnerID = a.UserID

            }).FirstOrDefault();

            model.Owner = services.appUserRepo.Where(x => x.ID == model.InvestmentLetter.OwnerID).Select(a => new InvestmentLetterOwnerVM
            {
                Code = a.UserCode,
                ProfilePhoto = a.ProfilePhoto,
                Slugify = a.UserSlugify,
                UserName = a.Name + " " + a.SurName

            }).FirstOrDefault();

            model.Project = services.projectRepo.Where(x => x.ID == model.InvestmentLetter.ProjectID).Select(a => new InvestmentLetterProjectVM
            {
                ProjectCode = a.ProjectCode,
                ProjectName = a.ProjectName,
                ProjectSlugify = a.ProjectSlugify

            }).FirstOrDefault();

            return View(model);
        }

        public ActionResult GetInvestmentLetter(string projectname, string projectCode, int appid)
        {

            var project = services.projectRepo.FirstOrDefault(x => x.ProjectSlugify == projectname && x.ProjectCode == projectCode);

            ViewBag.ProjectID = project.ID;

            var letter = services.InvestmentLetterRepo.FirstOrDefault(x => x.ProjectID == project.ID);

            var letterCount = services.InvestorLetterRepo.Count(x => x.InvestmentLetterID == letter.ID);

            ViewBag.LetterCount = letterCount;

            if (letterCount==10)
            {;
                services.projectAppRepo.Remove(x => x.AppStoreID == appid && x.ProjectID == project.ID);
                letterCount = 0;
                ViewBag.LetterCount = letterCount;
            }

            return View();
        }

        [HttpGet]
        public JsonResult SearchInvestor(string searchText)
        {
            var model = services.appUserRepo.Where(x => x.UserTypeID == 2).Where(x => x.Name.Contains(searchText) || x.SurName.Contains(searchText) || x.Email.Contains(searchText)).Select(a => new
            {
                Name = a.Name,
                SurName = a.SurName,
                ProfilePhoto = a.ProfilePhoto,
                ID = a.ID
            }).ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public class ValidationModel
        {
            public List<string> ErrorList { get; set; }
            public bool IsValid { get; set; }


        }


        [HttpPost]
        public JsonResult SendLetter(InvestmentLetterDTO model)
        {
            var response = new ValidationModel();

            if (ModelState.IsValid)
            {
                response.IsValid = true;
                response.ErrorList = new List<string>();

                InvestmentLetter letter = new InvestmentLetter();
                letter.Message = model.Message;
                letter.ProjectOverview = model.ProjectOverview;
                letter.CustomerSegment = model.CustomerSegment;
                letter.MarketPotential = model.MarketPotential;
                letter.ValueProposition = model.ValueProposition;
                letter.InvestmentStatus = model.InvestmentStatus;
                letter.InvestmentExpectancy = model.InvestmentExpectancy;
                letter.TeamAndCollaborators = model.TeamAndCollaborators;
                letter.CompetitorAnalysis = model.CompetitorAnalysis;
                letter.FinancialCondition = model.FinancialCondition;
                letter.ProjectID = model.ProjectID;
                letter.UserID = _currentUser.ID;

                services.InvestmentLetterRepo.Add(letter);
                services.Commit();

                InvestorInvestmentLetter investorLetter = new InvestorInvestmentLetter();
                investorLetter.InvesterID = model.InvestorID;
                investorLetter.InvestmentLetterID = letter.ID;

                services.InvestorLetterRepo.Add(investorLetter);
                services.Commit();

                return Json(response);
            }

            var errorList = (from item in ModelState.Values
                             from error in item.Errors
                             select error.ErrorMessage).ToList();

            response.ErrorList = errorList;
            response.IsValid = false;

            return Json(response);

        }
    }
}