using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CellPhone
    {
        public int CellPhoneId { get; set; }
        public string? CellPhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public Operator? Operator { get; set; }
    }
}
