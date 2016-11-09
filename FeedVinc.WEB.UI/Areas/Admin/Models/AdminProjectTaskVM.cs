using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AdminProjectTaskVM
    {
        public int ID { get; set; }
        public string TaskNameTR { get; set; }
        public string TaskNameEN { get; set; }
        public string TaskDescriptionTR { get; set; }
        public string TaskDescriptionEN { get; set; }
        public string TaskTypeName { get; set; }
        public bool IsDynamic { get; set; }
        public bool IsActive { get; set; }

        public int TaskTypeID { get; set; }

    }
}