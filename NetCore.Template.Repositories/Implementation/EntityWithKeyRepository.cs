using Microsoft.EntityFrameworkCore;
using NetCore.Template.Entities;
using System;
using System.Linq;

namespace NetCore.Template.Repositories.Implementation
{
    public class EntityWithKeyRepository<T> : Repository<T>, IEntityWithKeyRepository<T> where T : EntityWithKey
    {
        public EntityWithKeyRepository(DbContext context) : base(context)
        {
        }

        public T GetByKey(string key)
        {
            return Context.Set<T>().SingleOrDefault(e => e.Key == key);
        }

        public void RemoveByKey(string key)
        {
            Context.Set<T>().Remove(GetByKey(key));
        }
    }
}
