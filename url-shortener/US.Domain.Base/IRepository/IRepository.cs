using System.Collections.Generic;
using US.Domain.Entities;

namespace US.Domain.Base.IRepository
{
    public interface IRepository
    {
        IEnumerable<ShortUrl> GetCollectionFromDataStore();
        ShortUrl GetItemFromDataStoreByShortUrl(string url);
        ShortUrl GetItemFromDataStoreByLongUrl(string url);

        ShortUrl SaveItemToDataStore(ShortUrl shortUrl);
    }
}
