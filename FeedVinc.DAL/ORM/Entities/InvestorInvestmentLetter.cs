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
    public class InvestorInvestmentLetter : Entity, IEntityState
    {
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

        [Key, Column(Order = 0)]
        public int  InvestmentLetterID { get; set; }

        [Key, Column(Order = 1)]
        public long InvesterID { get; set; }


    }
}
