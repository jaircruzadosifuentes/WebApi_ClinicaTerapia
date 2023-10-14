using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public string? MessageContent { get; set; }
        public int? ToId { get; set; }
        public int? FromId { get; set; }
        public string? TypeUserTo { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? Seen { get; set; }
        public string? TypeUserFrom { get; set; }
        public bool? IsStaff { get; set; }
        public Person? Person { get; set; }
        public int? Count { get; set; }
        public string? UserName { get; set; }

    }
}
