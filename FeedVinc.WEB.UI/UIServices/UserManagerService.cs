using FeedVinc.DAL.ORM.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.UIServices
{
    public class UserManagerService
    {
        public static bool HasAccount(string email,string password)
        {
            using (ProjectContext context = new ProjectContext())
            {
               return context.Users.Any(x => x.Email == email && x.Password == password && x.IsActive);
            }
        }
    }
}