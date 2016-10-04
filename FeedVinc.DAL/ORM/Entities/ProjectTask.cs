using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ProjectTask:BaseEntity<byte>
    {
       
        public string Name { get; set; }
        public string TaskLogo { get; set; }
        public string Description { get; set; }
        //Hangi seviyede olduğunu görmek için
        public byte ProjectTaskTypeID { get; set; }
        public bool HasTextInput { get; set; }
        public bool HasHyperLink { get; set; }
        public string HyperLink { get; set; }
        public bool IsDynamic { get; set; }

    }
}
