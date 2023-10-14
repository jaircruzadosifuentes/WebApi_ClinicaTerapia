using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Pay
    {
        public int PayId { get; set; }
        public PayMethod? PayMethod { get; set; }
        public PatientNew? PatientNew { get; set; }
        public string? DescPayMethod { get; set; }
        public string? HourInitial { get; set; }
        public DateTime DateInitial { get; set; }
        public Patient? Patient { get; set; }

    }
}
