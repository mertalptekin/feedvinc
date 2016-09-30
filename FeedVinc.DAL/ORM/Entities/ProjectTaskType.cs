using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectTaskType:BaseEntity<byte>
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
