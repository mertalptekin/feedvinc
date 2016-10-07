﻿using FeedVinc.Common.Services;
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
        [Queryable]
        [Route("launch")]
        public IQueryable<ShareVM> GetFeedLaunch()
        {
            var model = services.projectLaunchRepo.ToList().Select(a => new ShareVM
            {
                ProjectLaunchID = a.ID,
                ShareCount = 0,
                MediaTypeID = a.MediaTypeID,
                ShareTypeID = a.ShareTypeID,
                Post = a.Information,
                PostDate = a.PostDate,
                PostMediaPath = a.MediaPath,
                ProjectID = a.ProjectID

            }).OrderByDescending(c => c.PostDate).ToList();

            model.ForEach(x => x.Launch = services.projectRepo.Where(y => y.ID == x.ProjectID).Select(z => new LaunchShareVM
            {
                AndroidLink = z.AndroidLink,
                AppleLink = z.AppleLink,
                WebLink = z.WebLink,
                ProjectName = z.ProjectName

            }).FirstOrDefault());

            model.ForEach(x => x.Launch.ProjectLaunchVote = services.projectLaunchVote.Where(y => y.ProjectLaunchID == x.ProjectLaunchID).Average(f => f.LaunchVotePoint));

            model.ForEach(x => x.Launch.ProjectLaunchVersion = services.projectLaunchRepo.Where(y => y.ID == x.ProjectLaunchID).Select(z => z.ProjectVersion).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }

        [Queryable]
        [Route("feedback")]
        public IQueryable<ShareVM> GetFeedBackFeed()
        {
            var model = services.projectFeedBackRepo.ToList().Select(a => new ShareVM
            {
                ProjectFeedBackID = a.ID,
                MediaTypeID = a.MediaTypeID,
                ShareTypeID = (byte)a.ShareTypeID,
                ShareCount = 0,
                ProjectID = a.ProjectID,
                Post = a.Information,
                PostDate = a.PostDate,
                PostMediaPath = a.FeedBackMedia

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.ProjectName = services.projectRepo.Where(x => x.ID == a.ProjectID).Select(y => y.ProjectName).FirstOrDefault());
     
            model.ForEach(a => a.FeedBack = services.projectFeedBackVote.Where(y => y.ProjectFeedBackID == a.ProjectFeedBackID).Select(z => new FeedBackShareVM
            {
                FeedBackVotePoint = z.FeedBackVotePoint

            }).FirstOrDefault());

            model.ForEach(a => a.FeedBack.FeedBackTestLink = services.projectFeedBackRepo.Where(x => x.ID == a.ProjectFeedBackID).Select(c => c.TestLink).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }

        [Queryable]
        [Route("idea")]
        public IQueryable<ShareVM> GetIdeaFeed()
        {
            var model = services.ideaShareRepo.ToList().Select(a => new ShareVM
            {
                ProjectIdeaID = a.ID,
                ShareTypeID = (byte)a.ShareTypeID,
                Post = a.Post,
                ProjectID = a.ProjectID,
                PostDate = a.PostDate,
                ShareCount = 0

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Idea = services.projectRepo.Where(y => y.ID == a.ProjectID).Select(z => new IdeaShareVM
            {
                ProjectID = z.ID,
                ProjectName = z.ProjectName,
                Post = z.SalesPitch,
                ProjectProfileLogo = z.ProjectProfileLogo

            }).FirstOrDefault());


            return model.AsQueryable<ShareVM>();
        }


        [Queryable]
        [Route("community")]
        public IQueryable<ShareVM> GetCommunityFeed()
        {
            var model = services.communityShareRepo.ToList().Select(a => new ShareVM
            {

                CommunityID = a.CommunityID,
                CommentCount = 0,
                LikeCount = 0,
                ShareCount = 0,
                Location = a.Location,
                MediaTypeID = a.MediaType,
                ShareTypeID = (byte)a.ShareTypeID,
                Post = a.Content,
                PostMediaPath = a.SharePath

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Community = services.communityRepo.Where(y => y.ID == a.ProjectID).Select(z => new CommunityShareVM
            {
                CommunityID = z.ID,
                OwnerID = z.OwnerID,
                CommunityName = z.CommunityName,
                ProfilePhotoLogo = z.CommunityLogo

            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();

        }


        [Queryable]
        [Route("story-tellin")]
        public IQueryable<ShareVM> GetStoryTelling()
        {
            var model = services.projectShareRepo.ToList().Select(a => new ShareVM
            {
                ProjectID = a.ProjectID,
                CommentCount = 0,
                LikeCount = 0,
                ShareCount = 0,
                Location = a.Location,
                MediaTypeID = a.MediaType,
                ShareTypeID = (byte)a.ShareTypeID,
                Post = a.Content,
                PostMediaPath = a.SharePath,

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Project = services.projectRepo.Where(y => y.ID == a.ProjectID).Select(z => new ProjectShareVM
            {
                OwnerID = z.UserID,
                ProfilePath = z.ProjectProfileLogo,
                ProjectName = z.ProjectName,
                ID = z.ID
            }).FirstOrDefault());

            return model.AsQueryable<ShareVM>();
        }


        [Queryable]
        [Route("around-me")]
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
