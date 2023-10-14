using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientInAttention
    {
        public int PatientInAttentionId { get; set; }
        public int? Nro { get; set; }
        public string? PatientNamesAttention { get; set; }
        public string? InAttentionFor { get; set; }
        public string? HourReserved { get; set; }
    }
}
