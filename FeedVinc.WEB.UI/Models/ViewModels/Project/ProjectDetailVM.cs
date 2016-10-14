using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectDetailVM
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectProfilePhoto { get; set; }
        public string SalesPitch { get; set; }
        public long ProjectID { get; set; }
        public string ProjectSlugify { get; set; }

        public byte ProjectCategoryID { get; set; }


    }
}