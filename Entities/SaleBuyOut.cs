using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SaleBuyOut
    {
        public int SaleBuyOutId { get; set; }
        public OperationType? OperationType { get; set; }
        public Movement? Movement { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public string? Serie { get; set; }
        public string? Number { get; set; }
        public PayMethod? PayMethod { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PersonEmit { get; set; }
        public string? TypeTransaction { get; set; }
        public int TypeTransactionValue { get; set; }

    }
}
