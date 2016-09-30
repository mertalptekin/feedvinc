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
    public class InvestorSpeedNetworking:Entity,IEntityState
    {
        //Hangi Yatırımcıya ait olduğu
        [Key, Column(Order = 0)]
        public long UserID { get; set; }
        //Hangi Projenin App olduğu

        [Key, Column(Order = 1)]
        public byte ProjectAppID { get; set; }

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
