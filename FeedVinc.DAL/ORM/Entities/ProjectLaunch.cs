﻿using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectLaunch:BaseEntity<int>
    {
        public string MediaPath { get; set; }
        public string ProjectVersion { get; set; }
        public string Information { get; set; }
        public long ProjectID { get; set; }
        public int MediaTypeID { get; set; }
        public byte ShareTypeID { get; set; }
        public DateTime? PostDate { get; set; }


    }
}
