using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectIdeaShare: BaseEntity<long>
    {
        public string Post { get; set; }
        public long ProjectID { get; set; }
        public DateTime? PostDate { get; set; }
        public byte ShareTypeID { get; set; }

        public bool IsSecondShare { get; set; }

        public long? OwnerID { get; set; }


    }
}
