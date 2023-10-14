using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientState
    {
        public int PatientStateId { get; set; }
        public string? Description { get; set; }
        public string? Abbreviation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool State { get; set; }
    }
}
