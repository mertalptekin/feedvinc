﻿using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUser:BaseEntity<long>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? BirthDate { get; set; }
        public string ProfilePhoto { get; set; }
        public string About { get; set; }
        public string JobInformation { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public byte UserTypeID { get; set; }
        public string CompanyInformation { get; set; }
        public bool EmailInformationEnabled { get; set; }
        public bool AccountInformationEnabled { get; set; }
        public string UserGUID { get; set; }
        public string UserSlugify { get; set; }
        public string OldEmail { get; set; }
        public string MemberCode { get; set; }
        public string UserCode { get; set; }

        public bool PublicMessageAccess { get; set; }
        public bool IsOnline { get; set; }
        public bool FollowerMessageAccess { get; set; }
        public bool NoMessageAccess { get; set; }
        public bool InvestorMessageAccessEnabled { get; set; }
        public bool EntrepreneurMessageAccessEnabled { get; set; }
        public bool DeveloperMessageAccessEnabled { get; set; }
        public string UserWebSiteLink { get; set; }

    }
}
