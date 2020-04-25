using System;
using System.Collections.Generic;

namespace US.Domain.Entities
{
    public class ShortUrl : Entity
    {
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
        public DateTime CreationDate { get; set; }

        // Navigation properties
        public IEnumerable<Visitor> Visitors { get; set; }
    }
}
