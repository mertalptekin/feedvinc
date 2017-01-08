using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeedVinc.WEB.UI.Areas.Admin.Models
{
    public class AppUserAccountVM
    {
        public string UserFullName { get; set; }
        public string UserJobInformation { get; set; }
        public bool AccountIsFrozen { get; set; }
        public string UserTypeName { get; set; }
        public int UserTypeID { get; set; }


    }
}