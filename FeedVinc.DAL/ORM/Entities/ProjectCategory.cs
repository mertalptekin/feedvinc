using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectCategory:BaseEntity<byte>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

    }
}
