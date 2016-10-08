using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project
{
    public class ProjectVM
    {
        public long ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectProfileLogo { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectSalesPitch { get; set; }
        public DateTime? CreateDate { get; set; }
        public byte CategoryID { get; set; }

    }
}