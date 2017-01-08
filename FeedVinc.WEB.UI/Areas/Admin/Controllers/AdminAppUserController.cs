using FeedVinc.WEB.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Areas.Admin.Controllers
{
    public class AdminAppUserController : AdminBaseController
    {
        // GET: Admin/AdminAppUser
        public ActionResult Index()
        {
            var model = services.appUserRepo.ToList().Select(a => new AppUserAccountVM
            {
                AccountIsFrozen = a.IsActive,
                UserFullName = a.Name + " " + a.SurName,
                UserJobInformation = a.JobInformation,
                UserTypeID = a.UserTypeID

            }).ToList();

            model.ForEach(a => a.UserTypeName = services.appUserTypeRepo.FirstOrDefault(y => y.ID == a.UserTypeID).UserTypeName);
            

            return View(model);
        }
    }
}