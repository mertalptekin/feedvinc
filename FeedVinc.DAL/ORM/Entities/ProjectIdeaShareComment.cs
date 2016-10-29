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
    public class ProjectIdeaShareComment: Entity, IEntityState
    {
        [Key, Column(Order = 0)]
        public int IdeaShareID { get; set; }

        [Key, Column(Order = 1)]
        public int UserID { get; set; }

        public string Comment { get; set; }

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

        public DateTime? PostDate { get; set; }

    }
}
