using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectFeedBack:BaseEntity<int>
    {
        public string FeedBackMedia { get; set; }
        public int MediaTypeID { get; set; }
        public string TestLink { get; set; }
        public string Information { get; set; }
        public long ProjectID { get; set; }
        public byte ShareTypeID { get; set; }
        public DateTime? PostDate { get; set; }
        public bool IsEnableNotifyInvestor { get; set; }



    }
}
