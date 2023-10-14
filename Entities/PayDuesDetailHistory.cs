using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PayDuesDetailHistory
    {
        public int PayDuesDetailHistoryId { get; set; }
        public PayDuesDetail? PayDuesDetail { get; set; }
        public DateTime DatePaymentCanceled { get; set; }
        public int? DebtNumber { get; set; }
        public decimal? Amount { get; set; }
        public string? UserPayment { get; set; }
        public Employeed? Employeed { get; set; }

    }
}
