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
    public class ProjectCategory:BaseEntity<byte>
    {
        
        [Required(ErrorMessage ="Kategoriye isim vermek zorundasınız")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage ="Dili Seçmek zorundasınız")]
        public string Lang { get; set; }

    }
}
