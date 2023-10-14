using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Error
    {
        public int ErrorId { get; set; }
        public string? Description { get; set; }
        public string? DescriptionTrace { get; set; }
        public string? CodeUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CodeError { get; set; }
        public int TypeError { get; set; }
    }
}
