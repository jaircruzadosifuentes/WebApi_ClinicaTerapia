using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string? Company { get; set; }
        public bool? StillWorks { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Activities { get; set; }
    }
}
