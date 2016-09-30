using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.DAL.ORM.Model
{
    public abstract class BaseEntity<T> :Entity, IEntityState, IEntityKey<T>
    {
        public T ID
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public bool IsDeleted
        {
            get;
            set;
        }
    }
}
