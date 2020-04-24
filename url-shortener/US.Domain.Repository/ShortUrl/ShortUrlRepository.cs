using System;
using System.Collections.Generic;
using US.Domain.Base.IRepository;
using US.Infrastructure.Base.Repository;

namespace US.Domain.Repository.ShortUrl
{
    public class ShortUrlRepository : BaseMongoRepository<Entities.ShortUrl>, IRepository
    {
        public ShortUrlRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {

        }

        public IEnumerable<Entities.ShortUrl> GetCollectionFromDataStore()
        {
            return GetList();
        }

        public Entities.ShortUrl GetItemFromDataStoreByShortUrl(string shortUrl)
        {
            return GetBy(c => c.ShortURL == shortUrl);
        }


        public Entities.ShortUrl GetItemFromDataStoreByLongUrl(string longUrl)
        {
            return GetBy(c => c.LongURL == longUrl);
        }

        public Entities.ShortUrl SaveItemToDataStore(Entities.ShortUrl model)
        {
            try
            {
                this.Create(model);
            }
            catch (Exception)
            {
                return null;
            }

            return model;
        }
    }
}
