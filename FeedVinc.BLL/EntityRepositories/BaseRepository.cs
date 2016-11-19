using FeedVinc.BLL.RepositoryModel;
using FeedVinc.DAL.ORM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using FeedVinc.DAL.ORM.Context;
using FeedVinc.DAL.ORM.Entities;
using System.Linq.Dynamic;

namespace FeedVinc.BLL.EntityRepositories
{
    public class BaseRepository<T> : IRepository<T> where T:Entity,IEntityState
    {
        protected DbContext _context;
        protected IDbSet<T> _dbset;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> lamda)
        {
            return _dbset.Any(lamda);
        }

        public void Delete(object id)
        {
           var entity =  _dbset.Find(id);
            entity.IsDeleted = true;
            entity.IsActive = false;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> lamda)
        {
            var  data = _dbset.FirstOrDefault(lamda);
            return data;
        }

        public IEnumerable<T> Include(Expression<Func<T,bool>> lamda,string tableName)
        {
            return _dbset.Where(lamda).Include(tableName).AsEnumerable();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> lamda)
        {
            return await _dbset.FirstOrDefaultAsync(lamda);
        }

        public IEnumerable<T> Query(string query)
        {
            return _dbset.Where(query).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> QueryAsync(string query)
        {
            return await _dbset.Where(query).ToListAsync<T>();
        }

        public void Remove(Expression<Func<T, bool>> lamda)
        {
            var entity = _dbset.FirstOrDefault(lamda);
            _dbset.Remove(entity);
        }


        public IEnumerable<T> ToList()
        {
            return _dbset.AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _dbset.ToListAsync<T>();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> lamda)
        {
            return _dbset.Where(lamda).AsEnumerable<T>();
        }

        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> lamda)
        {
            return await _dbset.Where(lamda).ToListAsync<T>();
        }

        public long Count()
        {
            return _dbset.Count();
        }

        public long Count(Expression<Func<T, bool>> _lamda)
        {
            return _dbset.Count(_lamda);
        }
    }
}
