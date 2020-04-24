using MongoDB.Bson;

namespace US.Domain
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}
