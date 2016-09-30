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
        public string LaunchLogo { get; set; }
        public string ProjectVersion { get; set; }
        public string WebLink { get; set; }
        public string IPhoneLink { get; set; }
        public string AndroidLink { get; set; }
        public string Information { get; set; }
        public long ProjectID { get; set; }

    }
}
