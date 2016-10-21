using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Advertisement;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AdvertisementUIController : BaseUIController
    {

        public ActionResult Index()
        {
            ViewData["MyProject"] = GetMyProjectsDropDown();
            ViewData["UserType"] = GetUserTypeDropDown();
            ViewData["Country"] = GetCountryDropDown();
            ViewData["City"] = GetCityDropDown(1);

            var model = services.projectAnnouncementRepo.
                ToList().
                OrderByDescending(c => c.PostDate).
                Take(10).
                Select(a => new AdvertisementVM
                {
                    Description = a.Description,
                    ProjectID = a.ProjectID,
                    DeadLine = a.DeadLine.ToShortDateString(),
                    PostDate = a.PostDate
                }).
                ToList();

            model.ForEach(a => a.ProjectAdds = services.projectRepo
            .Where(x => x.ID == a.ProjectID)
            .Select(s => new AdvertisementProjectVM
            {
                ProjectName = s.ProjectName,
                ProjectProfileLogo = s.ProjectProfileLogo,
                ProjectCode = s.ProjectCode,
                ProjectSlug = s.ProjectSlugify

            }).FirstOrDefault());


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddvertisementAdd(AdvertisementPostVM model)
        {
            ViewData["MyProject"] = GetMyProjectsDropDown();
            ViewData["UserType"] = GetUserTypeDropDown();
            ViewData["Country"] = GetCountryDropDown();
            ViewData["City"] = GetCityDropDown(1);

            if (ModelState.IsValid)
            {

                var entity = new ProjectAnnoucement
                {
                    ProjectID = model.ProjectID,
                    City = model.CityID,
                    Country = model.CountryID,
                    Description = HtmlTextService.ConvertToHtml(new AdContentVM { Description = model.Description, Title = model.Title }, "<p>", "</p>"),
                    DeadLine = DateTime.Now.AddMonths(1),
                    UserTypeID = (byte)model.UserTypeID,
                    PostDate = DateTime.Now,
                    IsActive = true
                    
                };

                services.projectAnnouncementRepo.Add(entity);
                services.Commit();


                var Data = new AdvertisementVM
                {
                    DeadLine = entity.DeadLine.ToShortDateString(),
                    Description = entity.Description,
                    ProjectID = model.ProjectID
                };

                Data.ProjectAdds = services.projectRepo.
                    Where(x => x.ID == model.ProjectID).
                    Select(c => new AdvertisementProjectVM
                    {
                        ProjectCode = c.ProjectCode,
                        ProjectName = c.ProjectName,
                        ProjectSlug = c.ProjectSlugify,
                        ProjectProfileLogo = c.ProjectProfileLogo

                    }).
                    FirstOrDefault();


                return Json(new ValidationDTO { IsValid=true,Data=Data,SuccessMessage=SiteLanguage.Advertisement_Success });
            }

            var errorList = ModelState.Values.
               SelectMany(m => m.Errors).
               Select(e => e.ErrorMessage).
               ToList();

            return Json(new ValidationDTO { IsValid=false, Data=errorList });
        }

    }
}