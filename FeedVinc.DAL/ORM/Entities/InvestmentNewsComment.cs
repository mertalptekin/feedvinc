using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class InvestmentNewsComment: Entity, IEntityState
    {
        [Key, Column(Order = 0)]
        public int InvestmentNewsID { get; set; }

        [Key, Column(Order = 1)]
        public long ApplicationUserID { get; set; }

        public string CommentText { get; set; }

        public bool IsActive
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
