using MongoDB.Bson;
using System;

namespace US.IService.ShortUrl.DTOs
{
    public class ShortUrlDto
    {
        public long Id { get; set; }
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
