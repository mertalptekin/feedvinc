using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
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


        public ActionResult GetInvestmentLetter()
        {
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