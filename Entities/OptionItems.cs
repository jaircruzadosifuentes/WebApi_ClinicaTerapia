using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OptionItems
    {
        public int OptionItemId { get; set; }
        public string? Component { get; set; }
        public string? Name { get; set; }
        public string? To { get; set; }
        public bool State { get; set; }
        public Option? Option { get; set; }
    }
}
