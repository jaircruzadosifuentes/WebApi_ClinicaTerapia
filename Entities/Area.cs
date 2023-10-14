using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Area
    {
        public int AreaId { get; set; }
        public string? AreaDescription { get; set; }
        public int? Value { get; set; }
        public string? Label { get; set; }
    }
}
