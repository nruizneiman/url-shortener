using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace US.Domain.Entities
{
    public class Visitor : Entity
    {
        public DateTime Date { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public ShortUrl ShortUrl { get; set; }
    }
}
