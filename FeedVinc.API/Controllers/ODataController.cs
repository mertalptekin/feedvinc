using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace FeedVinc.API.Controllers
{
    public class ODataController : BaseApiController
    {
        public ProjectContext context = new ProjectContext();
        

        [Queryable]
        public IQueryable<ShareVM> GetAroundMe()
        {
            var model = services.appUserShareRepo.ToList().Select(a=> new ShareVM
            {
                Location = a.Location,
                User = context.Users.Where(x=> x.ID==a.ID).Select(c=> new UserVM {
                    FullName = c.Name + " " + c.SurName,
                    ProfilePhoto = c.ProfilePhoto,
                    ID = c.ID
                }).FirstOrDefault(),
                MediaTypeID = a.MediaType,
                Post = a.Content,
                ShareTypeID = (byte)a.ShareTypeID,
                PostMediaPath = a.SharePath,
                PostDate = a.ShareDate
            });

            return model.AsQueryable<ShareVM>();
        }
    }
}
