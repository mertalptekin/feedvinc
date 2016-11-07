using FeedVinc.Common.Services;
using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using FeedVinc.WEB.UI.Models.DTO;
using FeedVinc.WEB.UI.Models.ViewModels.Account;
using FeedVinc.WEB.UI.Models.ViewModels.Home;
using FeedVinc.WEB.UI.UIServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
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
                ProjectID = a.ProjectID,
                ShareID = a.ID

            }).OrderByDescending(c => c.PostDate).ToList();

            model.ForEach(x => x.Launch = services.projectRepo.Where(y => y.ID == x.ProjectID).Select(z => new LaunchShareVM
            {
                AndroidLink = z.AndroidLink,
                AppleLink = z.AppleLink,
                WebLink = z.WebLink,
                ProjectName = z.ProjectName

            }).FirstOrDefault());

            model.ForEach(a => a.UserID = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).UserID);

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
                PostDate = a.PostDate,
                ShareID = a.ID

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.FeedBack = services.projectFeedBackRepo.
            Where(x => x.ID == a.ProjectFeedBackID).
            Select(y => new FeedBackShareVM { FeedBackTestLink = y.TestLink, ProjectID = y.ProjectID,Information = y.Information, ProjectProfileLogo= y.FeedBackMedia }).FirstOrDefault());

            model.ForEach(a => a.FeedBack.ProjectName = services.projectRepo.
            Where(x => x.ID==a.ProjectID).
            Select(y=> y.ProjectName).FirstOrDefault());

            model.ForEach(a => a.UserID = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).UserID);

            model.ForEach(a => a.FeedBack.FeedBackVotePoint = services.projectFeedBackVote.Where(y => y.ProjectFeedBackID == a.ProjectFeedBackID).Average(f => f.FeedBackVotePoint));

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
                ShareCount = 0,
                ShareID = a.ID

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.UserID = services.projectRepo.FirstOrDefault(x => x.ID == a.ProjectID).UserID);

            model.ForEach(a => a.Idea = services.projectRepo.Where(y => y.ID == a.ProjectID).Select(z => new IdeaShareVM
            {
                ProjectID = z.ID,
                ProjectName = z.ProjectName,
                Post = z.SalesPitch,
                ProjectProfileLogo = z.ProjectProfileLogo

            }).FirstOrDefault());

            model.ForEach(a => a.LikeCount = services.ideaShareLikeRepo
              .Count(x => x.IdeaShareID == a.ShareID));

            model.ForEach(a => a.ShareCount = services.ideaShareCommentRepo
            .Count(x => x.IdeaShareID == a.ShareID));

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
                PostMediaPath = a.SharePath,
                ShareID = a.ID,
                PostDate = a.ShareDate

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.UserID = services.communityRepo.FirstOrDefault(x => x.ID == a.CommunityID).OwnerID);

            model.ForEach(a => a.LikeCount = services.communityShareLikeRepo
                 .Count(x => x.CommunityShareID == a.ShareID));

            model.ForEach(a => a.CommentCount = services.communityShareCommentRepo
          .Count(x => x.CommunityShareID == a.ShareID));

            model.ForEach(a => a.Community = services.communityRepo.Where(y => y.ID == a.CommunityID).Select(z => new CommunityShareVM
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
                PostDate = a.ShareDate,
                PostMediaPath = a.SharePath,
                ShareID = a.ID

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.Project = services.projectRepo.Where(y => y.ID == a.ProjectID).Select(z => new ProjectShareVM
            {
                OwnerID = z.UserID,
                ProfilePath = z.ProjectProfileLogo,
                ProjectName = z.ProjectName,
                ID = z.ID
            }).FirstOrDefault());

            model.ForEach(a => a.LikeCount = services.projectShareLikeRepo
              .Count(x => x.ProjectShareID == a.ShareID));

            model.ForEach(a => a.ShareCount = services.projectShareCommentRepo
          .Count(x => x.ProjectShareID == a.ShareID));

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
                PostDate = a.ShareDate,
                ShareID = a.ID

            }).OrderByDescending(x => x.PostDate).ToList();

            model.ForEach(a => a.User = services.appUserRepo.Where(y => y.ID == a.UserID).Select(z => new UserVM
            {
                ID = z.ID,
                UserTypeID = z.UserTypeID,
                FullName = z.Name + " " + z.SurName,
                ProfilePhoto = z.ProfilePhoto

            }).FirstOrDefault());

            model.ForEach(a => a.LikeCount = services.appUserShareLikeRepo
              .Count(x => x.ApplicationUserShareID == a.ShareID));

            model.ForEach(a => a.ShareCount = services.appUserShareCommentRepo
          .Count(x => x.ApplicationUserShareID == a.ShareID));

            return model.AsQueryable<ShareVM>();
        }
    }
}
