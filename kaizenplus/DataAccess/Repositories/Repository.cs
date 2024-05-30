using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace kaizenplus.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DatabaseService databaseService;
        internal DbSet<TEntity> dbSet;

        public Repository(DatabaseService databaseService)
        {
            this.databaseService = databaseService;
            dbSet = databaseService.Set<TEntity>();
        }

        public virtual TEntity Get(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeQuery != null)
            {
                query = includeQuery(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }
            }

            return query.ToList();
        }

        public virtual IQueryable<TEntity> Where(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeQuery != null)
            {
                query = includeQuery(query);
            }

            return query;
        }

        public virtual TEntity FirstOrDefault()
        {
            return dbSet.FirstOrDefault();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null)
        {
            if (includeQuery != null)
            {
                return includeQuery(dbSet.Where(expression)).FirstOrDefault();
            }

            return dbSet.Where(expression).FirstOrDefault();
        }

        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void CreateMultiple(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (databaseService.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void DeleteMultiple(List<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            databaseService.Entry(entity).State = EntityState.Modified;
        }
    }
}