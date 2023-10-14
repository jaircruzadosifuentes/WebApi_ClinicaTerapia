using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientInQueueGeneral
    {
        public int PatientInQueueGeneralId { get; set; }
        public string? Nro { get; set; }
        public string? PatientName { get; set; }
        public DateTime DayReserved { get; set; }
        public string? HourReserved { get; set; }
        public string? Reason { get; set; }
        public int TimeDemoration { get; set; }
        public Employeed? Employeed { get; set; }
        public int IsNew { get; set; }
        public int PatientId { get; set; }
        public string? State { get; set; }
        public List<PatientProgress> PatientProgresses { get; set; }
        
        public PatientInQueueGeneral()
        {
            PatientProgresses = new List<PatientProgress>();    
        }
    }
}
