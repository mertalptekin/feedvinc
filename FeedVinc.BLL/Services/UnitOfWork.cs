using FeedVinc.BLL.EntityRepositories;
using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.BLL.Services
{
    public class UnitOfWork:IUnitOfWork
    {
        private DbContext _context;

        private BaseRepository<ApplicationMarket> _appMarketRepo;

        public BaseRepository<ApplicationMarket> appMarketRepo {
            get {

                return _appMarketRepo ?? new BaseRepository<ApplicationMarket>(_context);
            }
        }

        private BaseRepository<ApplicationMenu> _appMenuRepo;

        public BaseRepository<ApplicationMenu> appMenuRepo
        {
            get
            {

                return _appMenuRepo ?? new BaseRepository<ApplicationMenu>(_context);
            }
        }

        private BaseRepository<ApplicationMenuDetail> _appMenuDetailRepo;

        public BaseRepository<ApplicationMenuDetail> appMenuDetailRepo
        {
            get
            {

                return _appMenuDetailRepo ?? new BaseRepository<ApplicationMenuDetail>(_context);
            }
        }

        private BaseRepository<ApplicationStore> _appStoreRepo;

        public BaseRepository<ApplicationStore> appStoreRepo
        {
            get
            {

                return _appStoreRepo ?? new BaseRepository<ApplicationStore>(_context);
            }
        }

        private BaseRepository<ApplicationUser> _appUserRepo;

        public BaseRepository<ApplicationUser> appUserRepo
        {
            get
            {

                return _appUserRepo ?? new BaseRepository<ApplicationUser>(_context);
            }
        }

        private BaseRepository<ApplicationUserActivity> _appUserActivityRepo;

        public BaseRepository<ApplicationUserActivity> appUserActivityRepo
        {
            get
            {

                return _appUserActivityRepo ?? new BaseRepository<ApplicationUserActivity>(_context);
            }
        }

        private BaseRepository<ApplicationUserActivityCategory> _appUserActivityCategoryRepo;

        public BaseRepository<ApplicationUserActivityCategory> appUserActivityCategoryRepo
        {
            get
            {

                return _appUserActivityCategoryRepo ?? new BaseRepository<ApplicationUserActivityCategory>(_context);
            }
        }

        private BaseRepository<ApplicationUserFollow> _appUserFollowRepo;

        public BaseRepository<ApplicationUserFollow> appUserFollowRepo
        {
            get
            {

                return _appUserFollowRepo ?? new BaseRepository<ApplicationUserFollow>(_context);
            }
        }

        private BaseRepository<ApplicationUserJobType> _appUserJobTypeRepo;

        public BaseRepository<ApplicationUserJobType> appUserJobTypeRepo
        {
            get
            {

                return _appUserJobTypeRepo ?? new BaseRepository<ApplicationUserJobType>(_context);
            }
        }

        private BaseRepository<ApplicationUserJobTypeDetail> _appUserJobTypeDetailRepo;

        public BaseRepository<ApplicationUserJobTypeDetail> appUserJobTypeDetailRepo
        {
            get
            {

                return _appUserJobTypeDetailRepo ?? new BaseRepository<ApplicationUserJobTypeDetail>(_context);
            }
        }

        private BaseRepository<ApplicationUserMessage> _appUserMessageRepo;

        public BaseRepository<ApplicationUserMessage> appUserMessageRepo
        {
            get
            {

                return _appUserMessageRepo ?? new BaseRepository<ApplicationUserMessage>(_context);
            }
        }

        private BaseRepository<ApplicationUserShare> _appUserShareRepo;

        public BaseRepository<ApplicationUserShare> appUserShareRepo
        {
            get
            {

                return _appUserShareRepo ?? new BaseRepository<ApplicationUserShare>(_context);
            }
        }

        private BaseRepository<ApplicationUserShareComment> _appUserShareCommentRepo;

        public BaseRepository<ApplicationUserShareComment> appUserShareCommentRepo
        {
            get
            {

                return _appUserShareCommentRepo ?? new BaseRepository<ApplicationUserShareComment>(_context);
            }
        }

        private BaseRepository<ApplicationUserShareLike> _appUserShareLikeRepo;

        public BaseRepository<ApplicationUserShareLike> appUserShareLikeRepo
        {
            get
            {

                return _appUserShareLikeRepo ?? new BaseRepository<ApplicationUserShareLike>(_context);
            }
        }

        private BaseRepository<ApplicationUserShareTag> _appUserShareTagRepo;

        public BaseRepository<ApplicationUserShareTag> appUserShareTagRepo
        {
            get
            {

                return _appUserShareTagRepo ?? new BaseRepository<ApplicationUserShareTag>(_context);
            }
        }

        private BaseRepository<ApplicationUserShareTagDetail> _appUserShareTagDetailRepo;

        public BaseRepository<ApplicationUserShareTagDetail> appUserShareTagDetailRepo
        {
            get
            {

                return _appUserShareTagDetailRepo ?? new BaseRepository<ApplicationUserShareTagDetail>(_context);
            }
        }

        private BaseRepository<ApplicationUserType> _appUserTypeRepo;

        public BaseRepository<ApplicationUserType> appUserTypeRepo
        {
            get
            {

                return _appUserTypeRepo ?? new BaseRepository<ApplicationUserType>(_context);
            }
        }

        private BaseRepository<City> _cityRepo;

        public BaseRepository<City> cityRepo
        {
            get
            {

                return _cityRepo ?? new BaseRepository<City>(_context);
            }
        }

        private BaseRepository<Community> _communityRepo;

        public BaseRepository<Community> communityRepo
        {
            get
            {

                return _communityRepo ?? new BaseRepository<Community>(_context);
            }
        }

        private BaseRepository<CommunityShare> _communityShareRepo;

        public BaseRepository<CommunityShare> communityShareRepo
        {
            get
            {

                return _communityShareRepo ?? new BaseRepository<CommunityShare>(_context);
            }
        }

        private BaseRepository<CommunityShareComment> _communityShareCommentRepo;

        public BaseRepository<CommunityShareComment> communityShareCommentRepo
        {
            get
            {

                return _communityShareCommentRepo ?? new BaseRepository<CommunityShareComment>(_context);
            }
        }

        private BaseRepository<CommunityShareLike> _communityShareLikeRepo;

        public BaseRepository<CommunityShareLike> communityShareLikeRepo
        {
            get
            {

                return _communityShareLikeRepo ?? new BaseRepository<CommunityShareLike>(_context);
            }
        }

        private BaseRepository<CommunityUser> _communityUserRepo;

        public BaseRepository<CommunityUser> communityUserRepo
        {
            get
            {

                return _communityUserRepo ?? new BaseRepository<CommunityUser>(_context);
            }
        }

        private BaseRepository<Country> _countryRepo;

        public BaseRepository<Country> countryRepo
        {
            get
            {

                return _countryRepo ?? new BaseRepository<Country>(_context);
            }
        }

        private BaseRepository<InvestorSpeedNetworking> _investorSpeedNetworkingRepo;

        public BaseRepository<InvestorSpeedNetworking> investorSpeedNetworkingRepo
        {
            get
            {

                return _investorSpeedNetworkingRepo ?? new BaseRepository<InvestorSpeedNetworking>(_context);
            }
        }

        private BaseRepository<Project> _projectRepo;

        public BaseRepository<Project> projectRepo
        {
            get
            {

                return _projectRepo ?? new BaseRepository<Project>(_context);
            }
        }

        private BaseRepository<ProjectAnnoucement> _projectAnnouncementRepo;

        public BaseRepository<ProjectAnnoucement> projectAnnouncementRepo
        {
            get
            {

                return _projectAnnouncementRepo ?? new BaseRepository<ProjectAnnoucement>(_context);
            }
        }

        private BaseRepository<ProjectApp> _projectAppRepo;

        public BaseRepository<ProjectApp> projectAppRepo
        {
            get
            {

                return _projectAppRepo ?? new BaseRepository<ProjectApp>(_context);
            }
        }

        private BaseRepository<ProjectCategory> _projectCategoryRepo;

        public BaseRepository<ProjectCategory> projectCategoryRepo
        {
            get
            {

                return _projectCategoryRepo ?? new BaseRepository<ProjectCategory>(_context);
            }
        }

        private BaseRepository<ProjectFeedBack> _projectFeedBackRepo;

        public BaseRepository<ProjectFeedBack> projectFeedBackRepo
        {
            get
            {

                return _projectFeedBackRepo ?? new BaseRepository<ProjectFeedBack>(_context);
            }
        }

        private BaseRepository<ProjectFollow> _projectFollowRepo;

        public BaseRepository<ProjectFollow> projectFollowRepo
        {
            get
            {

                return _projectFollowRepo ?? new BaseRepository<ProjectFollow>(_context);
            }
        }

        private BaseRepository<ProjectLaunch> _projectLaunchRepo;

        public BaseRepository<ProjectLaunch> projectLaunchRepo
        {
            get
            {

                return _projectLaunchRepo ?? new BaseRepository<ProjectLaunch>(_context);
            }
        }

        private BaseRepository<ProjectLike> _projectLikeRepo;

        public BaseRepository<ProjectLike> projectLikeRepo
        {
            get
            {

                return _projectLikeRepo ?? new BaseRepository<ProjectLike>(_context);
            }
        }

        private BaseRepository<ProjectMedia> _projectMediaRepo;

        public BaseRepository<ProjectMedia> projectMediaRepo
        {
            get
            {

                return _projectMediaRepo ?? new BaseRepository<ProjectMedia>(_context);
            }
        }

        private BaseRepository<ProjectShare> _projectShareRepo;

        public BaseRepository<ProjectShare> projectShareRepo
        {
            get
            {

                return _projectShareRepo ?? new BaseRepository<ProjectShare>(_context);
            }
        }

        private BaseRepository<ProjectShareComment> _projectShareCommentRepo;

        public BaseRepository<ProjectShareComment> projectShareCommentRepo
        {
            get
            {

                return _projectShareCommentRepo ?? new BaseRepository<ProjectShareComment>(_context);
            }
        }

        private BaseRepository<ProjectShareLike> _projectShareLikeRepo;

        public BaseRepository<ProjectShareLike> projectShareLikeRepo
        {
            get
            {

                return _projectShareLikeRepo ?? new BaseRepository<ProjectShareLike>(_context);
            }
        }

        private BaseRepository<ProjectTask> _projectTaskRepo;

        public BaseRepository<ProjectTask> projectTaskRepo
        {
            get
            {

                return _projectTaskRepo ?? new BaseRepository<ProjectTask>(_context);
            }
        }

        private BaseRepository<ProjectTaskDetail> _projectTaskDetailRepo;

        public BaseRepository<ProjectTaskDetail> projectTaskDetailRepo
        {
            get
            {

                return _projectTaskDetailRepo ?? new BaseRepository<ProjectTaskDetail>(_context);
            }
        }

        private BaseRepository<ProjectTaskType> _projectTaskTypeRepo;

        public BaseRepository<ProjectTaskType> projectTaskTypeRepo
        {
            get
            {

                return _projectTaskTypeRepo ?? new BaseRepository<ProjectTaskType>(_context);
            }
        }

        private BaseRepository<ProjectTeam> _projectTeamRepo;

        public BaseRepository<ProjectTeam> projectTeamRepo
        {
            get
            {

                return _projectTeamRepo ?? new BaseRepository<ProjectTeam>(_context);
            }
        }

        private BaseRepository<ProjectIdeaShare> _ideaShareRepo;

        public BaseRepository<ProjectIdeaShare> ideaShareRepo
        {
            get
            {

                return _ideaShareRepo ?? new BaseRepository<ProjectIdeaShare>(_context);
            }
        }

        private BaseRepository<ProjectIdeaShareComment> _ideaShareCommentRepo;

        public BaseRepository<ProjectIdeaShareComment> ideaShareCommentRepo
        {
            get
            {

                return _ideaShareCommentRepo ?? new BaseRepository<ProjectIdeaShareComment>(_context);
            }
        }

        private BaseRepository<ProjectIdeaShareLike> _ideaShareLikeRepo;

        public BaseRepository<ProjectIdeaShareLike> ideaShareLikeRepo
        {
            get
            {

                return _ideaShareLikeRepo ?? new BaseRepository<ProjectIdeaShareLike>(_context);
            }
        }

        private BaseRepository<ProjectFeedBackVote> _projectFeedBackVote;

        public BaseRepository<ProjectFeedBackVote> projectFeedBackVote
        {
            get
            {

                return _projectFeedBackVote ?? new BaseRepository<ProjectFeedBackVote>(_context);
            }
        }

        private BaseRepository<ProjectLaunchVote> _projectLaunchVote;

        public BaseRepository<ProjectLaunchVote> projectLaunchVote
        {
            get
            {

                return _projectLaunchVote ?? new BaseRepository<ProjectLaunchVote>(_context);
            }
        }

        private BaseRepository<ProjectPhoto> _projectPhotoRepo;

        public BaseRepository<ProjectPhoto> projectPhotoRepo
        {
            get
            {

                return _projectPhotoRepo ?? new BaseRepository<ProjectPhoto>(_context);
            }
        }

        private BaseRepository<ProjectVideo> _projectVideoRepo;
        public BaseRepository<ProjectVideo> projectVideoRepo
        {
            get
            {

                return _projectVideoRepo ?? new BaseRepository<ProjectVideo>(_context);
            }
        }

        private BaseRepository<FollowNotification> _followNotifyRepo;

        public BaseRepository<FollowNotification> followNotifyRepo
        {
            get
            {

                return _followNotifyRepo ?? new BaseRepository<FollowNotification>(_context);
            }
        }

        private BaseRepository<ShareNotification> _shareNotifyRepo;

        public BaseRepository<ShareNotification> shareNotifyRepo
        {
            get
            {

                return _shareNotifyRepo ?? new BaseRepository<ShareNotification>(_context);
            }
        }

        public UnitOfWork()
        {
            _context = new ProjectContext();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
