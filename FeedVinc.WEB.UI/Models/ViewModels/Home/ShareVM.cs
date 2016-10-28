using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class ShareVM
    {
        public long UserID { get; set; }
        public long ProjectID { get; set; }
        public long CommunityID { get; set; }
        public long ProjectIdeaID { get; set; }
        public long ProjectLaunchID { get; set; }
        public long ProjectFeedBackID { get; set; }
        public string ProjectName { get; set; }
        public string ProfilePhotoPath { get; set; }
        public string PrettyDate { get; set; }
        public string ShareTypeText { get; set; }
        public string Post { get; set; }
        public string PostMediaPath { get; set; }
        public long LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int ShareCount { get; set; }
        public byte ShareTypeID { get; set; }
        public int MediaTypeID { get; set; }
        public string Location { get; set; }
        public UserVM User { get; set; }
        public LaunchShareVM Launch { get; set; }
        public FeedBackShareVM FeedBack { get; set; }
        public ProjectShareVM Project { get; set; }
        public CommunityShareVM Community { get; set; }
        public IdeaShareVM Idea { get; set; }
        public DateTime? PostDate { get; set; }
        public long ShareID { get; set; }

        public bool LikedCurrentUser { get; set; }

    }
}