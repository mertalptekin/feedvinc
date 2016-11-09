using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Services
{
    public class AdminUserService
    {
        public static string GetCurrentAdmin
        {
            get
            {
                if (HttpContext.Current.Request.Cookies["AdminUserCookie"] !=null)
                {
                    return HttpContext.Current.Request.Cookies["AdminUserCookie"].Value.Decrpte();
                }

                return String.Empty;
            }
        }

    }
}