using FeedVinc.DAL.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace FeedVinc.DAL.ORM.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext()
        {

            //Database.Connection.ConnectionString = @"Server=mssql13.trwww.com; database=feedvincDB;uid=user_feedvinc;pwd=feedvincDB_1";
            Database.Connection.ConnectionString = @"Server=127.0.0.1;database=feedvincDB;uid=sa;pwd=123";
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationMarket> AppMarkets { get; set; }
        public DbSet<ApplicationMenu> AppMenus { get; set; }
        public DbSet<ApplicationMenuDetail> AppMenuDetails { get; set; }
        public DbSet<ApplicationStore> AppStores { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<ApplicationUserActivity> AppUserActivities { get; set; }
        public DbSet<ApplicationUserActivityCategory> AppUserActivityCategory { get; set; }
        public DbSet<ApplicationUserFollow> AppUserFollows { get; set; }
        public DbSet<ApplicationUserJobType> AppUserJobTypes { get; set; }
        public DbSet<ApplicationUserJobTypeDetail> AppUserJobTypeDetails { get; set; }
        public DbSet<ApplicationUserMessage> AppUserMessages { get; set; }
        public DbSet<ApplicationUserShare> AppUserShares { get; set; }
        public DbSet<ApplicationUserShareComment> AppUserShareComments { get; set; }
        public DbSet<ApplicationUserShareLike> AppUserLikes { get; set; }
        public DbSet<ApplicationUserShareTag> AppUserShareTags { get; set; }
        public DbSet<ApplicationUserShareTagDetail> AppUserShareTagDetails { get; set; }
        public DbSet<ApplicationUserType> AppUserType { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<CommunityShare> CommunityShares { get; set; }
        public DbSet<CommunityShareComment> CommunityShareComments { get; set; }
        public DbSet<CommunityShareLike> CommunityShareLikes { get; set; }
        public DbSet<CommunityUser> CommunityUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<InvestorSpeedNetworking> InvestorSpeedNetworkings { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectAnnoucement> ProjectAnnouncements { get; set; }
        public DbSet<ProjectApp> ProjectApps { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectFeedBack> ProjectFeedBacks { get; set; }
        public DbSet<ProjectFollow> ProjectFollows { get; set; }
        public DbSet<ProjectLaunch> ProjectLaunches { get; set; }
        public DbSet<ProjectLike> ProjectLikes { get; set; }
        public DbSet<ProjectMedia> ProjectMedias { get; set; }
        public DbSet<ProjectShare> ProjectShares { get; set; }
        public DbSet<ProjectShareComment> ProjectShareComments { get; set; }
        public DbSet<ProjectShareLike> ProjectShareLikes { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectTaskDetail> ProjectTaskDetails { get; set; }
        public DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }
        public DbSet<ProjectTeam> ProjectTeams { get; set; }
        public DbSet<ProjectIdeaShare> IdeaShares { get; set; }
        public DbSet<ProjectIdeaShareComment> IdeaShareComments { get; set; }
        public DbSet<ProjectIdeaShareLike> IdeaShareLikes { get; set; }
        public DbSet<ProjectFeedBackVote> ProjectFeedBackVotes { get; set; }
        public DbSet<ProjectLaunchVote> ProjectLaunchVotes { get; set; }
        public DbSet<ProjectPhoto> ProjectPhotos { get; set; }
        public DbSet<ProjectVideo> ProjectVideos { get; set; }
        public DbSet<FollowNotification> FollowNotifications { get; set; }
        public DbSet<ShareNotification> ShareNotifications { get; set; }

        public DbSet<ShareNotificationUser> ShareNotificationUser { get; set; }


    }
}
