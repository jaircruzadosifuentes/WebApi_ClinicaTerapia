using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientSolicitude
    {
        public int PatientSolicitudeId { get; set; }
        public Employeed? Employeed { get; set; }
        public string? HourAttention { get; set; }
        public DateTime DateAttention { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool State { get; set; }
        public Patient? Patient { get; set; }
        public int TimeAttention { get; set; }
        public string? SystemHour { get; set; }
    }
}
