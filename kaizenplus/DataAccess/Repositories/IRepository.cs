using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace kaizenplus.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object id);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null, int? take = null,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null);

        TEntity FirstOrDefault();

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeQuery = null);

        void Create(TEntity entity);

        void CreateMultiple(IEnumerable<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entity);

        void DeleteMultiple(List<TEntity> entities);

        void Update(TEntity entity);
    }
}