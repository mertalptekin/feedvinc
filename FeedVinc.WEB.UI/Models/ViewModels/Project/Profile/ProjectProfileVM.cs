using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Project.Profile
{
    public class ProjectProfileVM
    {
        public long ProjectID { get; set; }
        public string ProjectSlugify { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string SalesPitch { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string ProjectCategoryName { get; set; }
        public string ProjectProfilePhoto { get; set; }
        public long FeedPoint { get; set; }
        public string ProjectOwnerName { get; set; }
        public long FollowerCount { get; set; }
        public string Weblink { get; set; }
        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string About { get; set; }
        public string ProjectLevel { get; set; }
        public int CityID { get; set; }
        public byte CategoryID { get; set; }
        public int CountryID { get; set; }
        public long ProjectOwnerID { get; set; }
        public ProjectProfileOwner Owner { get; set; }
        public string InvestedText { get; set; }



    }
}