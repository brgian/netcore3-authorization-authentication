using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCore.Template.Repositories.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private const string PredicateEmpty = "False";

        protected DbContext Context { get; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public T Add(T entity)
        {
            return Context.Set<T>().Add(entity).Entity;
        }

        public T GetById(long id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Remove(long id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Set<T>().Remove(entity);
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Context.Set<T>();
            
            if (include != null)
                query = include(query);

            return query.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Context.Set<T>();
            
            if (include != null)
                query = include(query);

            return query;
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Context.Set<T>();
            
            if (include != null)
                query = include(query);

            if (!PredicateEmpty.Equals(predicate.Body.ToString(), StringComparison.InvariantCultureIgnoreCase))
                return query.Where(predicate);
            
            return query;
        }

        public T Update(T entity)
        {
            return Context.Set<T>().Update(entity).Entity;
        }
    }
}