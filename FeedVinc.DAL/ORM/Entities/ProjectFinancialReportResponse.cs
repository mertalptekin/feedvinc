﻿using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectFinancialReportResponse : Entity, IEntityState
    {

        [Key, Column(Order = 0)]
        public long ProjectID { get; set; }

        [Key, Column(Order = 1)]
        public int AnswerFinancialRequestID { get; set; }

        [Key,Column(Order =2)]
        public long InvesterID { get; set; }



        [NotMapped]
        public bool IsActive
        {
            get;
            set;
        }

        [NotMapped]
        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
