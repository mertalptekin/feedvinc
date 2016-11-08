using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class SecondShare:BaseEntity<Guid>
    {
        public SecondShare()
        {
            ID = Guid.NewGuid();
        }

        public DateTime PostDate { get; set; }
        public string Post { get; set; }
        public string PostMediaPath { get; set; }
        public long ShareID { get; set; }
        public int? MediaTypeID { get; set; }
        public string Location { get; set; }
        public long OwnerID { get; set; }

    }
}
