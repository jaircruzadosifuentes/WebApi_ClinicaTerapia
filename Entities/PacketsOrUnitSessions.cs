using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PacketsOrUnitSessions
    {
        public int PacketsOrUnitSessionsId { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberSessions { get; set; }
        public decimal CostPerUnit { get; set; }
        public string? Abbreviation { get; set; }
        public int MaximumFeesToPay { get; set; }
        public string? State { get; set; }
        public bool? StateValue { get; set; }
    }
}
