using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace UMovies.Data
{
    public class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public EntityFrameworkRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }

            _dbContext = dbContext;
        }

        protected DbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Add(entity);
        }

        public virtual TEntity GetById(TKey id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
            .Where(predicate)
            .AsEnumerable();
        }
    }
}