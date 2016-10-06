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

        [Queryable]
        public IQueryable<ShareVM> GetAroundMe()
        {
            var model = services.appUserShareRepo.ToList().Select(a => new ShareVM
            {
                UserID = a.UserID,
                Location = a.Location,
                MediaTypeID = a.MediaType,
                Post = a.Content,
                ShareTypeID = (byte)a.ShareTypeID,
                PostMediaPath = a.SharePath,
                PostDate = a.ShareDate
            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.User = services.appUserRepo.Where(y => y.ID == a.UserID).Select(z => new UserVM
            {
                UserTypeID = z.UserTypeID,
                FullName = z.Name + " " + z.SurName,
                ProfilePhoto = z.ProfilePhoto
               
            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }
    }
}
