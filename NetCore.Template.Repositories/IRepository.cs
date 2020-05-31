using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetCore.Template.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(long id);
        IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> Search(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T FindSingle(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T Add(T entity);
        T Update(T entity);
        void Remove(long entity);
    }
}