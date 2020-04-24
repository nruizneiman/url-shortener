using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using US.Domain;

namespace US.Infrastructure.Base.Repository
{
    public class BaseMongoRepository<TEntity> where TEntity : Entity
    {
        private readonly IMongoCollection<TEntity> mongoCollection;

        public BaseMongoRepository(string mongoDBConnectionString, string dbName, string collectionName)
        {
            MongoClient client = new MongoClient(mongoDBConnectionString);
            IMongoDatabase dataBase = client.GetDatabase(dbName);
            mongoCollection = dataBase.GetCollection<TEntity>(collectionName);
        }

        public virtual List<TEntity> GetList()
        {
            return mongoCollection.Find(book => true).ToList();
        }

        public virtual TEntity GetById(string id)
        {
            ObjectId docId = new ObjectId(id);
            return mongoCollection.Find<TEntity>(x => x.Id == docId).FirstOrDefault();
        }

        public TEntity GetBy(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            TEntity query = mongoCollection.Find(predicate).FirstOrDefault();
            return query;
        }

        public virtual TEntity Create(TEntity entity)
        {
            mongoCollection.InsertOne(entity);
            return entity;
        }

        public virtual void Update(string id, TEntity entity)
        {
            ObjectId docId = new ObjectId(id);
            mongoCollection.ReplaceOne(x => x.Id == docId, entity);
        }

        public virtual void Delete(TEntity entity)
        {
            mongoCollection.DeleteOne(x => x.Id == entity.Id);
        }

        public virtual void Delete(string id)
        {
            ObjectId docId = new ObjectId(id);
            mongoCollection.DeleteOne(x => x.Id == docId);
        }
    }
}
