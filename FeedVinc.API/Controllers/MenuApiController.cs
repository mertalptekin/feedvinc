using FeedVinc.API.Models.ViewModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FeedVinc.API.Controllers
{
    public class MenuApiController : BaseApiController
    {
        public IHttpActionResult GetAppUserMenu(byte userTypeID,string lang)
        {
            IEnumerable<byte> appMenuIDs = services.appMenuDetailRepo.Where(x => x.UserTypeID == userTypeID).Select(x => x.ApplicationMenuID).ToList();

            var model = services.appMenuRepo.Where(x => x.Lang == lang && appMenuIDs.Contains(x.ID)).Select(a => new ApplicationUserMenu
            {
                MenuName = a.Name,
                RedirectURL = a.Url

            }).ToList();

            return Ok(model);

        }
    }
}
