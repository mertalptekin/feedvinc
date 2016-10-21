using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Event;
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
            DateTime date = DateTime.Now.AddDays(-7);

            var model = services.appUserActivityRepo.
                Where(x => x.StartDate < date).
                Select(a => new EventVM
                {
                    EventDate = a.StartDate,
                    EventTime = a.Time,
                    EventProfilePhoto = a.ActivityLogo,
                    EventTitle = a.Title,
                    Location = a.ActivityPlace,
                    UserID = a.UserID

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
        public ActionResult EventAdd(EventPostVM model)
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
                    UserID = UserManagerService.CurrentUser.ID

                };

                services.appUserActivityRepo.Add(entity);
                services.Commit();

                return Redirect("/events");
            }

            return View(model);
        }
    }
}