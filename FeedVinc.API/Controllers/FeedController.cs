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
    [RoutePrefix("api/feed")]
    public class FeedController : BaseApiController
    {


        //public IQueryable<ShareVM> GetIdeaFeed()
        //{
        //    //var model = services.projectRepo.ToList().Select(a=> new ShareVM
        //    //{

        //    //})
        //}


        [Queryable][Route("community")]
        public IQueryable<ShareVM> GetCommunityFeed()
        {
            var model = services.communityShareRepo.ToList().Select(a => new ShareVM
            {

                CommunityID = a.CommunityID,
                CommentCount = 0,
                LikeCount = 0,
                ShareCount =0,
                Location = a.Location,
                MediaTypeID = a.MediaType,
                ShareTypeID = (byte)a.ShareTypeID,
                Post = a.Content,
                PostMediaPath = a.SharePath

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Community = services.communityRepo.Where(y => y.ID == a.ProjectID).Select(z => new CommunityVM
            {
                CommunityID = z.ID,
                OwnerID = z.OwnerID,
                CommunityName = z.CommunityName,
                ProfilePhotoLogo = z.CommunityLogo

            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();

        }


        [Queryable][Route("story-tellin")]
        public IQueryable<ShareVM> GetStoryTelling()
        {
            var model = services.projectShareRepo.ToList().Select(a => new ShareVM
            {
                ProjectID = a.ProjectID,
                CommentCount = 0,
                LikeCount = 0,
                ShareCount=0,
                Location = a.Location,
                MediaTypeID = a.MediaType,
                ShareTypeID = (byte)a.ShareTypeID,
                Post = a.Content,
                PostMediaPath = a.SharePath,

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Project = services.projectRepo.Where(y => y.ID == a.ProjectID).Select(z => new ProjectVM
            {
                OwnerID = z.UserID,
                ProfilePath = z.ProjectProfileLogo,
                ProjectName = z.ProjectName,
                ID = z.ID
            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }


        [Queryable][Route("around-me")]
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
                ID = z.ID,
                UserTypeID = z.UserTypeID,
                FullName = z.Name + " " + z.SurName,
                ProfilePhoto = z.ProfilePhoto
               
            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }
    }
}
