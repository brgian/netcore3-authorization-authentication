using NetCore.Template.Entities;

namespace NetCore.Template.Repositories
{
    public interface IMyEntityRepository : IEntityWithKeyRepository<MyEntity>
    {
    }
}