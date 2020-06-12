using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CurrencyExchange.Data.Interfaces
{
    public interface IGenericRepository<TEntity, TKey>
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddAsync(List<TEntity> entities);
        Task DeleteAsync(TKey id);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        void Detach(TEntity entityToDetach);
        IQueryable<TEntity> Get();
        Task<TEntity> GetAsync(TKey id);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where);
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(List<TEntity> entities);
    }
}
