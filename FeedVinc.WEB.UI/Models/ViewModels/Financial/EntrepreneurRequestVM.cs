using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Financial
{
    public class EntrepreneurRequestVM
    {
        public string InvesterName { get; set; }
        public string ProjectName { get; set; }
        public long InvesterID { get; set; }
        public long ProjectID { get; set; }

    }
}