﻿using FeedVinc.WEB.UI.Models.ViewModels.Community.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Community
{
    public class CommunityListVM
    {
        public List<CommunityVM> Communities { get; set; }
        public List<InvestedProjectVM> InvestedProjects { get; set; }
        public List<LastestLaunchVM> LastestLaunch { get; set; }

    }
}