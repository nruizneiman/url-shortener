using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace US.Domain.Base.IRepository
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Get(long id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> whereCondition = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task Create(T entity);
        void Update(T entity);
        void Delete(long id);
        void Commit();
        void Rollback();
    }
}
