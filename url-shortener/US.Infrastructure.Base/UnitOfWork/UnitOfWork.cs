using US.Domain.Base.IUnitOfWork;
using US.Infrastructure.Context;

namespace US.Infrastructure.Base.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public DataContext Context { get; }

        public UnitOfWork(DataContext dataContext)
        {
            Context = dataContext;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Rollback()
        {
            Context.Dispose();
        }
    }
}
