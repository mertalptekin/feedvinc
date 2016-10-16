using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectVideo:BaseEntity<long>
    {
        public string VideoPath { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long ProjectID { get; set; }
    }
}
