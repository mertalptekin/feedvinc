using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AdminAppListVM
    {
        public int ID { get; set; }
        public string AppNameTR { get; set; }
        public string AppNameEn { get; set; }
        public bool IsFree { get; set; }
        public string InformationTR { get; set; }
        public string InformationEN { get; set; }
        public string SalesPrice { get; set; }
        public bool IsActive { get; set; }


    }
}