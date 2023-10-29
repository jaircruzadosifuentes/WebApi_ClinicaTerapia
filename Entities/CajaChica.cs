using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CajaChica
    {
        public int CajaChicaId { get; set; }
        public decimal? MontoAperturado { get; set; }
        public decimal? MontoVendido { get; set; }
        public decimal? MontoEgreso { get; set; }
        public decimal? MontoEsperado { get; set; }
        public DateTime? FechaCierre { get; set; }
        public int? EmployeedCashId { get; set; }
        public int? IsApertu { get; set; }
        public string? MessageState { get; set; }

        public string? Employeed { get; set; }
        public string? Campus { get; set; }
        public string? CashRegister { get; set; }
        public string? State { get; set; }
        public DateTime? FechaApertu { get; set; }
    }
}
