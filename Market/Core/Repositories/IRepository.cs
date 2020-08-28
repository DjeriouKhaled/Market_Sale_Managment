using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Market.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

     
        TEntity Get(int id);
        TEntity Get(int? id);
        int GetCount();

        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);


        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        // add to database
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        // remove from database
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
