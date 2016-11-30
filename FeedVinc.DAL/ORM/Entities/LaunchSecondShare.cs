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
    public class LaunchSecondShare:Entity,IEntityState
    {
        //hangi kullanıcı paylaşımına ait olduğu

        [Key, Column(Order = 0)]
        public int AppProjectLaunchShareID { get; set; }
        //Kim Tarafından Paylaşıldığı

        [Key, Column(Order = 1)]
        public int OwnerID { get; set; }

        public string PostOwnerProjectName { get; set; }

        public DateTime PostDate { get; set; }

        [NotMapped]
        public bool IsActive
        {
            get;
            set;
        }

        [NotMapped]
        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
