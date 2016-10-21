using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectAnnoucement:BaseEntity<int>
    {
        //Yatırımcı ve Geliştirici için
        public byte UserTypeID { get; set; }
        public long ProjectID { get; set; }
        public string Description { get; set; }
        //Son Başvuru Tarihi
        public DateTime DeadLine { get; set; }
        public int? Country { get; set; }
        public int? City { get; set; }
        public DateTime PostDate { get; set; }


    }
}
