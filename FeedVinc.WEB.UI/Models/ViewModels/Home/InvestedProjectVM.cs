using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Home
{
    public class InvestedProjectVM
    {
        public string ProjectName { get; set; }
        public string ProjectSlug { get; set; }
        public string SalesPitch { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectProfilePhoto { get; set; }
        public DateTime? CreateDate { get; set; }
        public long ProjectID { get; set; }
        public long OwnerID { get; set; }
        public bool IsFollowed { get; set; }


    }
}