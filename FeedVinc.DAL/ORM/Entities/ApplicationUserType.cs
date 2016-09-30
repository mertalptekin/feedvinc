using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUserType:BaseEntity<byte>
    {
        public string UserTypeName { get; set; }
    }
}
