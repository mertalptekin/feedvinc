using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectMission:BaseEntity<long>
    {

        public string MissionContent { get; set; }
        public DateTime CreateDate { get; set; }
        public int MissionTypeID { get; set; }

    }
}
