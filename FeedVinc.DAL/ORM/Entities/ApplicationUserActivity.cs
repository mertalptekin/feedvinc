using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUserActivity:BaseEntity<int>
    {
        public long UserID { get; set; }
        public string Title { get; set; }
        public byte? ActivityCategoryID { get; set; }
        public string ActivityLogo { get; set; }
        public DateTime StartDate { get; set; }
        public string Time { get; set; }
        public string ActivityPlace { get; set; }


    }
}
