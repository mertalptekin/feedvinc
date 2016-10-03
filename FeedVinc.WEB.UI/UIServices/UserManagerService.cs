using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace FeedVinc.WEB.UI.UIServices
{
    public class UserManagerService
    {

        public static UserVM HasAccount(string email, string password)
        {
            using (ProjectContext context = new ProjectContext())
            {
               var model = context.Users.Where(x => x.Email == email && x.Password == password && x.IsActive).Select(a => new UserVM
                {
                    ProfilePhoto = a.ProfilePhoto,
                    About = a.About,
                    FullName = a.Name + " " + a.SurName,
                    BirthDate = a.BirthDate,
                    EmailInformationEnabled = a.EmailInformationEnabled,
                    AccountInformationEnabled = a.AccountInformationEnabled,
                    Email = a.Email,
                    ID = a.ID,
                    PhoneNumber = a.PhoneNumber,
                    UserTypeID = a.UserTypeID

                }).FirstOrDefault();

                return model;
            }
        }

        public static ApplicationUser CurrentUser
        {
            get
            {
                if (CookieManagerService.GetCookie("ApplicationUser") != null)
                {
                    var jsonString = CookieManagerService.GetCookie("AplicationUser").Value;

                    return JsonConvert.DeserializeObject<ApplicationUser>(jsonString);
                }

                return null;
            }
        }

        public static bool EmailIsUnique(string email)
        {
            using (ProjectContext context = new ProjectContext())
            {
                return context.Users.FirstOrDefault(x => x.Email == email) == null ? true : false;
            }
        }
    }
}