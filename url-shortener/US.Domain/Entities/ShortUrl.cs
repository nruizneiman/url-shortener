using MongoDB.Bson.Serialization.Attributes;
using System;

namespace US.Domain.Entities
{
    public class ShortUrl : Entity
    {
        [BsonElement("ShortURL")]
        public string ShortURL { get; set; }

        [BsonElement("LongURL")]
        public string LongURL { get; set; }

        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; set; }
    }
}
