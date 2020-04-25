using US.Infrastructure.Context;

namespace US.Domain.Base.IUnitOfWork
{
    public interface IUnitOfWork
    {
        DataContext Context { get; }
        void Commit();
        void Rollback();
    }
}
