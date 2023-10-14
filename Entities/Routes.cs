using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Routes
    {
        public int RoutesId { get; set; }
        public string? Path { get; set; }
        public bool Exact { get; set; }
        public string? Name { get; set; }
        public string? Element { get; set; }
    }
}

