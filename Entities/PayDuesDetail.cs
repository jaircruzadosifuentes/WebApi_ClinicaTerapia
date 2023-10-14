using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PayDuesDetail
    {
        public int? PayDuesDetailId { get; set; }
        public PatientNew? PatientNew { get; set; }
        public int Dues { get; set; }
        public DateTime InitialDate { get; set; }
        public decimal MountDue { get; set; }
        public string? State { get; set; }
        public DateTime PaymentDay { get; set; }
        public int? StateId { get; set; }
        public List<PayDuesDetailHistory> PayDuesDetailHistories { get; set; }
        public Patient? Patient { get; set; }
        public Payment? Payment { get; set; }
        public PayDuesDetail()
        {
            PayDuesDetailHistories = new List<PayDuesDetailHistory>();
        }
    }
}
