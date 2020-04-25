using US.Domain.Base.IUnitOfWork;
using US.Domain.Repository.Visitor;
using US.Infrastructure.Base.Repository;

namespace US.Infrastructure.Repository.Visitor
{
    public class VisitorRepository : EfRepository<Domain.Entities.Visitor>, IVisitorRepository
    {
        public VisitorRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }
    }
}
