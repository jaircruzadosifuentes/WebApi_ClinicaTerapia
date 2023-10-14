using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PayMethod
    {
        public int Value { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }
        public bool State { get; set; }
        public bool HaveConcept { get; set; }
    }
}
