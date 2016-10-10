using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Entities
{
    public class ApplicationUser:BaseEntity<long>
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? BirthDate { get; set; }
        public string ProfilePhoto { get; set; }
        public string About { get; set; }
        public string UserInformation { get; set; }
        public int? CityID { get; set; }
        public int? CountryID { get; set; }
        public byte UserTypeID { get; set; }
        public string CompanyInformation { get; set; }
        public bool EmailInformationEnabled { get; set; }
        public bool AccountInformationEnabled { get; set; }
        public string UserGUID { get; set; }
        public string UserSlugify { get; set; }


    }
}
