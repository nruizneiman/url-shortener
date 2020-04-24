using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace US.IService.ShortUrl.DTOs
{
    public class ShortUrlDto
    {
        public ObjectId Id { get; set; }
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
