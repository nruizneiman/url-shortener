using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace US.IService.Visitor.DTOs
{
    public class VisitorDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
        public string UserAgent { get; set; }
        public string ShortURL { get; set; }
        public string LongURL { get; set; }
    }
}
