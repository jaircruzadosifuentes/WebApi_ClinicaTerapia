using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientValidateSchedule
    {
        public int PatientValidateScheduleId { get; set; }
        public string? PatientWithSchedule { get; set; }
        public string? HourInitial { get; set; }
        public string? HourFinished { get; set; }
    }
}
