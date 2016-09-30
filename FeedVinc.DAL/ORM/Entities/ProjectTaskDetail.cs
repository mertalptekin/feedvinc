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
    public class ProjectTaskDetail:Entity,IEntityState
    {
        //Hangi Görev olduğu
        [Key, Column(Order = 0)]
        public long ProjectTaskID { get; set; }
        //Hangi Projeye ait bir görev olduğu

        [Key, Column(Order = 1)]
        public long ProjectID { get; set; }
        //görev adımının tamamlanıp tamamlanmadığı
        public bool IsCompleted { get; set; }

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
