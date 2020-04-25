using System.Collections.Generic;
using System.Threading.Tasks;
using US.Domain.Base.IRepository;

namespace US.Domain.Repository.ShortUrl
{
    public interface IShortUrlRepository : IRepository<Entities.ShortUrl>
    {
        Task<IEnumerable<Entities.ShortUrl>> GetCollectionFromDataStore();
        Entities.ShortUrl GetItemFromDataStoreByShortUrl(string url);
        Entities.ShortUrl GetItemFromDataStoreByLongUrl(string url);

        Task<Entities.ShortUrl> SaveItemToDataStore(Entities.ShortUrl shortUrl);
    }
}
