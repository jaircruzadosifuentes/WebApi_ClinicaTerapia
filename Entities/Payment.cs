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

        //Pendiente de pagos
        public int CuoPendingPayment { get; set; }
        public DateTime NextPaymentDate { get; set; }
        public int LateDays { get; set; }
        public string? StatePay { get; set; }
        public DateTime DateOfIssue { get; set; }
        public Campus? Campus{ get; set; }
        public int? StatePayId { get; set; }
        public DateTime? SesionDateMin { get; set; }
        public DateTime? SesionDateMax { get; set; }
        public Employeed? Employeed { get; set; }
        public string? UserPay { get; set; }
        public int? StateValue { get; set; }
        public int? StatePayValue { get; set; }
        public Payment()
        {
            PaymentScheduleDetails = new List<PaymentScheduleDetail>();
        }
    }
}
