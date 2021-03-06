﻿using FeedVinc.WEB.UI.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.DTO
{
    public class SharePostDTO
    {
        public string Post { get; set; }
        public string Location { get; set; }
        public byte ShareTypeID { get; set; }
        public string MediaPath { get; set; }
        public string ShareTitle { get; set; }
        public int MediaTypeID { get; set; }
        public ValidationDTO Validation { get; set; }
        public long FeedID { get; set; }
        public UserVM User { get; set; }
        public string PrettyDate { get; set; }
        public ProjectSharePostDTO ProjectShare { get; set; }
        public CommunitySharePostDTO CommunityShare { get; set; }
        public UserSharePostDTO UserShare { get; set; }
        public bool LikedCurrentUser { get; set; }
        public long LikeCount { get; set; }



    }
}