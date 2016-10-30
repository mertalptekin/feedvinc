using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ShareNotification:BaseEntity<long>
    {
        public string Link { get; set; }
        public string NotificationPhotoPath { get; set; }
        public string OwnerName { get; set; }
        public DateTime PostDate { get; set; }
        public string NotificationText { get; set; }


    }
}
