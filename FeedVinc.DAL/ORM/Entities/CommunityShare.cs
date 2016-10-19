using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    
    public class CommunityShare:BaseEntity<long>
    {
        public string Content { get; set; }
        public string Location { get; set; }
        public int MediaType { get; set; }
        public string SharePath { get; set; }
        public string ShareTitle { get; set; }
        public int ShareTypeID { get; set; }
        public int CommunityID { get; set; }
        public DateTime? ShareDate { get; set; }


    }
}
