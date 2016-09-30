using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.BLL.Services
{
    public interface IUnitOfWork:IDisposable
    {
        int Commit();
        Task<int> CommitAsync();

    }
}
