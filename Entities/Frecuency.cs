using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Frecuency
    {
        public int FrecuencyId { get; set; }
        public string? FrecuencyDescription { get; set; }
        public string? Abbreviation { get; set; }
        public int? Value { get; set; }
        public string? State { get; set; }
    }
}
