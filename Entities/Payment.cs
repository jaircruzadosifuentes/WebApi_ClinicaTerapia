using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Patient? Patient { get; set; }
        public decimal? Total { get; set; }
        public decimal? Igv { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TotalCancelled { get; set; }
        public decimal? TotalDebt { get; set; }
        public string? State { get; set; }
        public int DebtNumbertMax { get; set; }
        public List<PaymentScheduleDetail> PaymentScheduleDetails { get; set; }

        public Payment()
        {
            PaymentScheduleDetails = new List<PaymentScheduleDetail>();
        }
    }
}
