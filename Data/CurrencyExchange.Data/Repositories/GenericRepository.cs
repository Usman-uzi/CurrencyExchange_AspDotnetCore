using Microsoft.EntityFrameworkCore;
using CurrencyExchange.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyExchange.Data
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private IDatabaseContext _dbContext;
        public DbSet<TEntity> DbSet { get; private set; }

        public IDatabaseContext DbContext
        {
            get
            {
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }

        public GenericRepository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(TKey id)
        {
            TEntity async = await DbSet.FindAsync((object)id);
            return async;
        }

        public virtual async Task<List<TEntity>> GetAsync(
      Expression<Func<TEntity, bool>> filter = null,
      Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
      string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            if (filter != null)
                query = query.Where(filter);
            string[] strArray = includeProperties.Split(new char[1]
            {
        ','
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < strArray.Length; ++index)
            {
                string includeProperty = strArray[index];
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                List<TEntity> listAsync = await orderBy(query).ToListAsync(new CancellationToken());
                return listAsync;
            }
            List<TEntity> listAsync1 = await query.ToListAsync(new CancellationToken());
            return listAsync1;
        }

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            List<TEntity> listAsync = await DbSet.Where(where).ToListAsync(new CancellationToken());
            return listAsync;
        }

        public virtual IQueryable<TEntity> Get()
        {
            return DbSet.AsQueryable<TEntity>();
        }

        private TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity).Entity;
        }

        public virtual Task<TEntity> AddAsync(TEntity entity)
        {
            return Task.FromResult(Add(entity));
        }

        public virtual void Add(List<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual async Task AddAsync(List<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities, new CancellationToken());
        }

        private async Task Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            DbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Update(entity);
        }

        private async Task Update(List<TEntity> entitiesToUpdate)
        {
            foreach (TEntity entity1 in entitiesToUpdate)
            {
                TEntity entity = entity1;
                await Update(entity);
            }
        }

        public virtual Task UpdateAsync(List<TEntity> entities)
        {
            return Task.Run(() => Update(entities));
        }

        public virtual async Task DeleteAsync(TKey id)
        {
            TEntity entity = await DbSet.FindAsync((object)id);
            TEntity entityToDelete = entity;
            if (entityToDelete == null)
                return;
            Delete(entityToDelete);
        }

        private void Delete(TEntity entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Detach(TEntity entityToDetach)
        {
            DbSet.Attach(entityToDetach);
            DbContext.Entry(entityToDetach).State = EntityState.Detached;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            return Task.Run(() => Delete(entity));
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> where)
        {
            DbSet<TEntity> dbSet = DbSet;
            List<TEntity> entityList = await DbSet.Where(where).ToListAsync(new CancellationToken());
            dbSet.RemoveRange(entityList);
        }
    }
}
