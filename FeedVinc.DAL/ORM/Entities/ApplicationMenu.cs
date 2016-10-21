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
    public class ApplicationMenu:BaseEntity<byte>
    {
        public string Name { get; set; }
        public string Lang { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
    }
}
