using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Financial
{
    public class RequestVM
    {
        public List<EntrepreneurRequestVM> SendRequests { get; set; }
        public List<InvesterRequestVM> Responses { get; set; }

    }
}