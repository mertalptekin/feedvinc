using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectPhoto:BaseEntity<long>
    {
        public string PhotoPath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProjectID { get; set; }


    }
}
