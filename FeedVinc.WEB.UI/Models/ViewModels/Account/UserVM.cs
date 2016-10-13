using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Models.ViewModels.Account
{
    public class UserVM
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public long ID { get; set; }
        public string Email { get; set; }
        public byte UserTypeID { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ProfilePhoto { get; set; }
        public bool EmailInformationEnabled { get; set; }
        public bool AccountInformationEnabled { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public string UserTypeText { get; set; }
        public string Description { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public string UserGUID { get; set; }



    }
}