using NetCore.Template.Context;
using NetCore.Template.Entities;

namespace NetCore.Template.Repositories.Implementation
{
    public class MyEntityRepository : EntityWithKeyRepository<MyEntity>, IMyEntityRepository
    {
        public MyDbContext DbContext => Context as MyDbContext;

        public MyEntityRepository(MyDbContext context) : base(context)
        {
        }
    }
}
