using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectMedia:BaseEntity<long>
    {
        public string PathOrginal { get; set; }
        public string PathThumbNail { get; set; }
        public long ProjectID { get; set; }
        public byte MediaType { get; set; }

    }
}
