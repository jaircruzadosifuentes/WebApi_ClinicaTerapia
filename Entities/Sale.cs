using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Sale
    {
        public int SaleId { get; set; }
        public decimal CashAmount { get; set; }
        public string? ConceptPay { get; set; }
        public decimal? ExChange { get; set; }
        public decimal? Igv { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Total { get; set; }
        public bool? IsClientManuallyExistsInDataBase { get; set; }
        public bool? IsClientManuallyRegister { get; set; }
        public PayMethod? PayMethod { get; set; }
        public Person? Person { get; set; }
        public List<SaleBuyOutProduct> SaleBuyOutProducts { get; set; }
        public int TypeDocumentVouId { get; set; }
        public string? Correlativo { get; set; }
        public int? EmployeedId { get; set; }
        public Sale()
        {
            SaleBuyOutProducts = new List<SaleBuyOutProduct>();
        }
    }
}
