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
    //Ara Tablo
    public class ProjectApp:Entity,IEntityState
    {
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpireDate { get; set; }

        [Key, Column(Order = 0)]
        public byte AppStoreID { get; set; }

        [Key, Column(Order = 1)]
        public long ProjectID { get; set; }

        public bool IsNetworking { get; set; }

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
