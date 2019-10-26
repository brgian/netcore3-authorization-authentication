namespace NetCore.Template.Repositories
{
    public interface IUnitOfWork
    {
        IMyEntityRepository MyEntityRepository { get; }
        int Complete();
    }
}