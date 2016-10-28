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
    //Ara Tablo
    public class ProjectShareLike:Entity,IEntityState
    {
        [Key, Column(Order = 0)]
        public long UserID { get; set; }

        [Key, Column(Order = 1)]
        public long ProjectShareID { get; set; }

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
