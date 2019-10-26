using NetCore.Template.Entities;

namespace NetCore.Template.Repositories
{
    public interface IEntityWithKeyRepository<T> : IRepository<T> where T : EntityWithKey 
    {
        T GetByKey(string key);

        void RemoveByKey(string key);
    }
}
