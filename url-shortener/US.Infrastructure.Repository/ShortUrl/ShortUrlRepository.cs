using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using US.Domain.Base.IUnitOfWork;
using US.Domain.Repository.ShortUrl;
using US.Infrastructure.Base.Repository;

namespace US.Infrastructure.Repository.ShortUrl
{
    public class ShortUrlRepository : EfRepository<Domain.Entities.ShortUrl>, IShortUrlRepository
    {
        public ShortUrlRepository(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {

        }

        public async Task<IEnumerable<Domain.Entities.ShortUrl>> GetCollectionFromDataStore()
        {
            return await GetAll();
        }

        public Domain.Entities.ShortUrl GetItemFromDataStoreByShortUrl(string shortUrl)
        {
            return GetByFilter(c => c.ShortURL == shortUrl).Result.FirstOrDefault();
        }


        public Domain.Entities.ShortUrl GetItemFromDataStoreByLongUrl(string longUrl)
        {
            return GetByFilter(c => c.LongURL == longUrl).Result.FirstOrDefault();
        }

        public async Task<Domain.Entities.ShortUrl> SaveItemToDataStore(Domain.Entities.ShortUrl model)
        {
            await Create(model);

            return model;
        }
    }
}
