using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class AdminUser:BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
