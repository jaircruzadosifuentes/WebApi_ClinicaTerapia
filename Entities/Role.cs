using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public int? Value { get; set; }
        public string? Label { get; set; }
        public decimal? Salary { get; set; }
        public string? StateDescription { get; set; }
        public bool? State { get; set; }
        public Area? Area { get; set; }
    }
}
