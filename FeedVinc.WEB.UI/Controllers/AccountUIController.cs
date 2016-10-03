using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Resources;
using FeedVinc.WEB.UI.UIServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Controllers
{
    public class AccountUIController : BaseUIController
    {



        [HttpPost]
        public ActionResult Login(LoginVM model)
        {

            var user = services.appUserRepo.Where(x => x.Email == model.Email && x.Password == model.Password && x.IsActive).Select(a => new
            {
                ProfilePhoto = a.ProfilePhoto,
                About = a.About,
                FullName = a.Name + a.SurName,
                BirthDate = a.BirthDate,
                EmailInformationEnabled = a.EmailInformationEnabled,
                Email = a.Email,
                ID = a.ID,
                PhoneNumber = a.PhoneNumber,
                UserTypeID = a.UserTypeID

            }).FirstOrDefault();

            if (user != null)
            {
                string jsonString = JsonConvert.SerializeObject(user);
                return Redirect("/home");
            }

            return View();

        }

        public ActionResult UserActivation(string activationCode)
        {
           var model = services.appUserRepo.FirstOrDefault(x => x.UserGUID == activationCode);
            model.IsActive = true;

            return Redirect("/home");
        }

        [HttpPost]
        public JsonResult Register(RegisterVM model)
        {
            if (ModelState.IsValid && UserManagerService.EmailIsUnique(model.Email))
            {
                var user = new ApplicationUser
                {
                    Name = model.FullName.Split(' ')[0],
                    SurName = model.FullName.Split(' ')[1],
                    Email = model.Email,
                    Password = model.Password,
                    UserGUID = Guid.NewGuid().ToString()
                };

                services.appUserRepo.Add(user);
                services.Commit();

                string subject = "FeedVinc | Üyelik Onaylama";
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Template/activate.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{URL}", "http://localhost:60020/activate-account/" + user.UserGUID);
                body = body.Replace("{NAME}", user.Name);

                List<MailAddress> toList = new List<MailAddress>();
                toList.Add(new MailAddress(user.Email, user.Name + " " + user.SurName, System.Text.Encoding.UTF8));

                string logoPath = Server.MapPath(@"~/Content/Image/Email/FeedVinc_Logo.png");

                EmailService.SendMail(toList, null, null, subject, body, logoPath);
                return Json(new { message = SiteLanguage.Register_Success, IsValid = true });


            }

            ModelState.AddModelError("Email", SiteLanguage.EmailUnique_Validation);

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

            return Json(new { error = errorList, IsValid=false });
        }

        public JsonResult EmailIsUnique(string email)
        {
            var data = new { Langue = LanguageService.getCurrentLanguage, Value = UserManagerService.EmailIsUnique(email) };

            return Json(data);
        }


        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordVM modal)
        {
            return View();
        }

        public ActionResult LogOut()
        {
            CookieManagerService.DeleteCookie("ApplicationUser");
            return Redirect("/index");
        }
    }
}