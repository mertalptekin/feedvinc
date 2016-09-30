using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
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

        public static ApplicationUser CurrentUser
        {
            get
            {
                if (CookieManagerService.GetCookie("ApplicationUser")!=null)
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
                return !context.Users.Any(x => x.Email == email);
            }
        }
    }
}