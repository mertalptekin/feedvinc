using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.UserProfile;
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
        [OverrideActionFilters]
        public JsonResult Login(LoginVM model)
        {
            var user = UserManagerService.HasAccount(model.Email, model.Password);

            if (user != null)
            {
                CookieManagerService.SetCookie("ApplicationUser", user.ID.ToString());   

                return Json(new { message = SiteLanguage.RedirectMessage, RedirectURL = "/home", IsValid = true });
            }

            return Json(new { message = SiteLanguage.Login_Error, IsValid = false });

        }

        [HttpGet]
        [OverrideActionFilters]
        public ActionResult UserActivation(string activationCode)
        {
            var model = services.appUserRepo.FirstOrDefault(x => x.UserGUID == activationCode);
            model.IsActive = true;
            services.Commit();

            return Redirect("/index");
        }

        [HttpPost]
        [OverrideActionFilters]
        public JsonResult Register(RegisterVM model)
        {

            if (!UserManagerService.UserNameIsCorrectFormat(model.FullName))
            {
                ModelState.AddModelError("FullName", SiteLanguage.FullName_Pattern_Error);
            }

            if (!UserManagerService.EmailIsUnique(model.Email))
            {
                ModelState.AddModelError("Email", SiteLanguage.EmailUnique_Validation);
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Name = model.FullName.Split(' ')[0].Capitalize(),
                    SurName = model.FullName.Split(' ')[1].ToUpper(),
                    Email = model.Email,
                    Password = model.Password,
                    UserGUID = Guid.NewGuid().ToString(),
                    UserTypeID = model.UserTypeID,
                    UserSlugify = SlugIfyService.SlugText(model.FullName)

                };

                services.appUserRepo.Add(user);
                services.Commit();

                string subject = "FeedVinc | " + SiteLanguage.Membership_Confirmation;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Template/activate.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{HELLO}", SiteLanguage.Email_Hello);
                body = body.Replace("{WELCOME}", SiteLanguage.Welcome);
                body = body.Replace("{CONTENT}", SiteLanguage.Email_Activation_Body);
                body = body.Replace("{WARNING}", SiteLanguage.Email_Activation_Warning);
                body = body.Replace("{URL}", "http://feedvinc.workstudyo.com/activate-account/" + user.UserGUID);
                body = body.Replace("{NAME}", user.Name);
                body = body.Replace("{LINK}", SiteLanguage.Activate_Link);

                List<MailAddress> toList = new List<MailAddress>();
                toList.Add(new MailAddress(user.Email, user.Name + " " + user.SurName, System.Text.Encoding.UTF8));

                string logoPath = Server.MapPath(@"~/Content/Template/FeedVinc_Logo.png");

                EmailService.SendMail(toList, null, null, subject, body, logoPath);
                return Json(new { message = SiteLanguage.Register_Success, IsValid = true });


            }

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

            return Json(new { error = errorList, IsValid = false });
        }


        [HttpPost]
        [OverrideActionFilters]
        public JsonResult ForgetPassword(ForgetPasswordVM modal)
        {
            var user = services.appUserRepo.FirstOrDefault(x => x.Email == modal.Email);
            user.Password = System.Web.Security.Membership.GeneratePassword(8, 1);
            services.Commit();

            if (user != null)
            {
                string subject = "FeedVinc | " + SiteLanguage.Forget_Password;
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/Content/Template/activate.html")))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{HELLO}",SiteLanguage.Email_Hello);
                body = body.Replace("{LINK}", SiteLanguage.WebSiteLink);
                body = body.Replace("{WELCOME}", "");
                body = body.Replace("{URL}", "http://feedvinc.workstudyo.com/");
                body = body.Replace("{NAME}", user.Name);
                body = body.Replace("{CONTENT}", SiteLanguage.Password_Reset_Warning_Message + "<p><span>Password : "+user.Password+"</span></p>");
                body = body.Replace("{WARNING}", SiteLanguage.Email_Activation_Warning);

                List<MailAddress> toList = new List<MailAddress>();
                toList.Add(new MailAddress(user.Email, user.Name + " " + user.SurName, System.Text.Encoding.UTF8));

                string logoPath = Server.MapPath(@"~/Content/Template/FeedVinc_Logo.png");

                bool IsSend = EmailService.SendMail(toList, null, null, subject, body, logoPath);
                return Json(new { message = SiteLanguage.Forget_Password_Success, IsValid = true });
            }


            return Json(new { error = SiteLanguage.Forget_Password_User_not_Found, IsValid = false });


        }

        [HttpGet]
        public ActionResult LogOut()
        {
            CookieManagerService.DeleteCookie("ApplicationUser");
            return Redirect("/index");
        }
    }
}