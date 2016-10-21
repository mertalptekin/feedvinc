using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Event;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class EventUIController : BaseUIController
    {

        public ActionResult Index()
        {
          

            var model = services.appUserActivityRepo.
                ToList().
                OrderByDescending(x=> x.CreatedDate).
                Take(10).
                Select(a => new EventVM
                {
                    EventDate = a.StartDate,
                    EventTime = a.Time,
                    EventProfilePhoto = a.ActivityLogo,
                    EventTitle = a.Title,
                    Location = a.ActivityPlace,
                    UserID = a.UserID,

                }).ToList();

            model.ForEach(a => a.User = services.appUserRepo.
            Where(x => x.ID == a.UserID).
            Select(c => new EventUserVM
            {
                UserCode = c.UserCode,
                UserName = c.Name + " " + c.SurName,
                UserSlug = c.UserSlugify
            }).
            FirstOrDefault());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EventAdd(EventPostVM model)
        {
            if (ModelState.IsValid)
            {
                var entity = new ApplicationUserActivity
                {
                    Title = model.EventTitle,
                    StartDate = model.EventDate,
                    Time = model.EventTime,
                    ActivityPlace = model.Location,
                    ActivityLogo = MediaManagerService.Save(new Models.DTO.MediaFormatDTO { Media = model.EventProfilePhoto, MediaType = 0 }),
                    IsActive = true,
                    UserID = UserManagerService.CurrentUser.ID,
                    CreatedDate = DateTime.Now

                };

                services.appUserActivityRepo.Add(entity);
                services.Commit();

                var Data = new EventVM
                {
                    EventDate = model.EventDate,
                    EventTime = model.EventTime,
                    EventProfilePhoto = entity.ActivityLogo,
                    EventTitle = model.EventTitle,
                    Location = model.Location,
                    UserID = entity.UserID

                };

                Data.User = services.appUserRepo.
                Where(x => x.ID == entity.UserID).
                Select(c => new EventUserVM
                {
                    UserCode = c.UserCode,
                    UserName = c.Name + " " + c.SurName,
                    UserSlug = c.UserSlugify

                }).FirstOrDefault();

                return Json(new ValidationDTO { IsValid = true, SuccessMessage = SiteLanguage.Event_Add_Success, Data = Data });
            }

            var errorList = ModelState.Values.
               SelectMany(m => m.Errors).
               Select(e => e.ErrorMessage).
               ToList();

            return Json(new ValidationDTO { IsValid=false, Data=errorList });
        }
    }
}