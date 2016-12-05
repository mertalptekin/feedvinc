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
    public class ProjectTask:BaseEntity<int>
    {
       
        public string NameTR { get; set; }
        public string NameEN { get; set; }
        public string TaskLogo { get; set; }
        public string DescriptionTR { get; set; }
        //Hangi seviyede olduğunu görmek için
        public string DescriptionEN { get; set; }
        public byte ProjectTaskTypeID { get; set; }
        public bool HasTextInput { get; set; }
        public bool HasHyperLink { get; set; }
        public string HyperLink { get; set; }
        public bool IsDynamic { get; set; }



    }
}
