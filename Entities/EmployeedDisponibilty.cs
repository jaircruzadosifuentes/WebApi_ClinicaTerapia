using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class EmployeedDisponibilty
    {
        public int EmployeedDisponibiltyId { get; set; }
        public string? PersonnelInCharge { get; set; }
        public DateTime DateProgramming { get; set; }
        public string? HourInitial  { get; set; }
        public string? HourFinished { get; set; }
        public string? Name { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int State { get; set; }
    }
}
