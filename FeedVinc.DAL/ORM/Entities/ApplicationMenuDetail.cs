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
    public class ApplicationMenuDetail:BaseEntity<byte>
    {
        [Key, Column(Order = 0)]
        public byte ApplicationMenuID { get; set; }

        [Key, Column(Order = 1)]
        public byte UserTypeID { get; set; }

    }
}
