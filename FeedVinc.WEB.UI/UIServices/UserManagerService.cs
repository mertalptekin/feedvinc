using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    ID = a.ID,
                    PhoneNumber = a.PhoneNumber,
                    UserTypeID = a.UserTypeID,
                   

                }).FirstOrDefault();

                return model;
            }
        }

        public static UserVM CurrentUser
        {
            get
            {
                if (CookieManagerService.GetCookie("ApplicationUser") != null)
                {
                    long UserID = Convert.ToInt64(CookieManagerService.GetCookie("ApplicationUser").Value);

                    using (ProjectContext context = new ProjectContext())
                    {
                        var model = context.Users.Where(x => x.ID == UserID).Select(a => new UserVM
                        {
                            UserTypeID = a.UserTypeID,
                            FullName = a.Name + " " + a.SurName,
                            ProfilePhoto = a.ProfilePhoto,
                            ID = a.ID,
                            About = a.About,
                            Company = a.CompanyInformation,
                            BirthDate = a.BirthDate,
                            Email = a.Email

                        }).FirstOrDefault();

                        return model;
                    }
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

        public static bool UserNameIsCorrectFormat(string username)
        {
            if (String.IsNullOrWhiteSpace(username) || !username.Contains(" ") || String.IsNullOrEmpty(username))
            {
                return false;
            }

            return true;
        }

        public static bool UserNameIsUnique(string username,long id)
        {
            using (ProjectContext context = new ProjectContext())
            {
                return context.Users.FirstOrDefault(x => (x.ID != id) && ((x.Name +" " + x.SurName) == username)) == null ? true : false;
            }
        }
    }
}