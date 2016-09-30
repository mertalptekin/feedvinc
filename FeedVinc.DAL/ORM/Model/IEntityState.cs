using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Model
{
    public interface IEntityState
    {
         bool IsActive { get; set; }
         bool IsDeleted { get; set; }

    }
}
