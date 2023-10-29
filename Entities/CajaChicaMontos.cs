using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CajaChicaMontos
    {
        public int CajaChicaMontosId { get; set; }
        public decimal? Amount { get; set; }
        public string? Description{ get; set; }
        public string? Color { get; set; }
    }
}
