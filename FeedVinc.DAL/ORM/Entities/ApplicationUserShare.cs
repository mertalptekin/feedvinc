using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUserShare:BaseEntity<long>
    {
        public string Content { get; set; }
        public string Location { get; set; }
        public int? MediaType { get; set; }
        public string SharePath { get; set; }
        //ShareType unutulmuş
        public int ShareTypeID { get; set; }
        public long UserID { get; set; }

        public DateTime? ShareDate { get; set; }
        public bool IsSecondShare { get; set; }

        public int? ProjectID { get; set; }




    }
}
