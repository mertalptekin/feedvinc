using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FeedVinc.WEB.UI.Attributes
{

    public class LoginControlAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserManagerService.CurrentUser == null)
            {
                filterContext.Result = new RedirectResult("/index");
            }
        }
    }
}