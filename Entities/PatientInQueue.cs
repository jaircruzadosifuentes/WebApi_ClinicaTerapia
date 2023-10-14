using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientInQueue
    {
        public int PatientInQueueId { get; set; }
        public int? Nro { get; set; }
        public string? PatientWaiting { get; set; }
        public string? Reason { get; set; }
        public string? HourReserved { get; set; }

    }
}
