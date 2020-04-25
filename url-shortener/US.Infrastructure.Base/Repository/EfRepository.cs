using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using US.Domain;
using US.Domain.Base.IRepository;
using US.Domain.Base.IUnitOfWork;

namespace US.Infrastructure.Base.Repository
{
    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        private readonly IUnitOfWork _unitOfWork;

        public EfRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(T entity)
        {
            await _unitOfWork.Context.Set<T>().AddAsync(entity);
        }

        public void Delete(long id)
        {
            T entity = _unitOfWork.Context.Set<T>().SingleOrDefault(x => x.Id == id);
            _unitOfWork.Context.Remove(entity);
        }

        public Task<T> Get(long id)
        {
            return _unitOfWork.Context.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> whereCondition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>();

            if (whereCondition != null) query = query.Where(whereCondition);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
        }

        public void Commit()
        {
            _unitOfWork.Commit();
        }

        public void Rollback()
        {
            _unitOfWork.Rollback();
        }
    }
}
