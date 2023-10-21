using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SaleBuyOutProduct
    {
        public int SaleBuyOutProductId { get; set; }
        public int ProductId { get; set; }
        public Sale? Sale { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Total { get; set; }

    }
}
