using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FeedVinc.BLL.RepositoryModel
{
    public interface IRepository<T> where T: Entity,IEntityState
    {
        IEnumerable<T> ToList();
        Task<IEnumerable<T>> ToListAsync();
        IEnumerable<T> Where(Expression<Func<T,bool>> lamda);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T,bool>> lamda);
        IEnumerable<T> Query(string query);
        Task<IEnumerable<T>> QueryAsync(string query);
        bool Any(Expression<Func<T, bool>> lamda);
        T FirstOrDefault(Expression<Func<T, bool>> lamda);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> lamda);
        void Add(T entity);
        void Remove(Expression<Func<T,bool>> lamda);
        void Delete(object id);

    }
}
