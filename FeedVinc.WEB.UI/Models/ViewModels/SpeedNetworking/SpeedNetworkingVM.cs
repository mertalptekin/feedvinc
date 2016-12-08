using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.SpeedNetworking
{
    public class SpeedNetworkingVM
    {
        public string ActiveVideoPath { get; set; }
        public List<SpeedNetworkingVideoVM> Videos { get; set; }

    }
}