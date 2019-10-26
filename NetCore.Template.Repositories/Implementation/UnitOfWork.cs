using NetCore.Template.Context;

namespace NetCore.Template.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private IMyEntityRepository myEntityRepository;

        private readonly MyDbContext dbContext;

        public UnitOfWork(IMyEntityRepository myEntityRepository, MyDbContext dbContext)
        {
            this.myEntityRepository = myEntityRepository;
            this.dbContext = dbContext;
        }

        public IMyEntityRepository MyEntityRepository
        {
            get
            {
                if (myEntityRepository == null)
                {
                    myEntityRepository = new MyEntityRepository(dbContext);
                    return myEntityRepository;
                }
                else
                    return myEntityRepository;
            }
        }

        public int Complete()
        {
            var rows = dbContext.SaveChanges();
            return rows;
        }
    }
}