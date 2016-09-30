﻿using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class Project:BaseEntity<long>
    {
        public string ProjectName { get; set; }
        public string SalesPitch { get; set; }
        public string ProjectProfileLogo { get; set; }
        public byte ProjectCategoryID { get; set; }
        public int? CountyID { get; set; }
        public int? CityID { get; set; }
        public int ProjectStatus { get; set; }
        public int InvestmentStatus { get; set; }
        public string WebLink { get; set; }
        public string AppleLink { get; set; }
        public string AndroidLink { get; set; }
        public string About { get; set; }
        //Bu kısım sorulacak
        public string ProjectTags { get; set; }
        //Projenin Owner'ı
        public long UserID { get; set; }
        //Yatırım Aldığı Tarih
        public DateTime InvestmentDate { get; set; }
        public bool IsInvested { get; set; }

    }
}
