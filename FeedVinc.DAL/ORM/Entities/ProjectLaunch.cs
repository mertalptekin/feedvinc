using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectLaunch:BaseEntity<int>
    {
        public string MediaPath { get; set; }
        public string ProjectVersion { get; set; }
        public string Information { get; set; }
        public long ProjectID { get; set; }
        public int? MediaTypeID { get; set; }
        public int? ShareTypeID { get; set; }
        public DateTime? PostDate { get; set; }
        public string AndroidLink { get; set; }
        public string AppleLink { get; set; }
        public string WebLink { get; set; }
        public long? OwnerID { get; set; }

        public bool IsSecondShare { get; set; }


    }
}
