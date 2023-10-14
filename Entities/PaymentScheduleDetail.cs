using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PaymentScheduleDetail
    {
        public int? PaymentScheduleDetailId { get; set; }
        public Payment? Payment { get; set; }
        public int? DebtNumber { get; set; }
        public decimal? Amount { get; set; }
        public string? State { get; set; }
        public string? UserPayment { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? ConceptoPago { get; set; }
        public PayMethod? PayMethod { get; set; }
        public decimal? MonetaryExchange { get; set; }
        public decimal? Cash { get; set;}
        public VoucherDocument? VoucherDocument { get; set; }
        public bool? IsNewCustomer { get; set; }
    }
}
