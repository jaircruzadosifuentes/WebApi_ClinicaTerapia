using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public CategoryEntity? Category { get; set; }
        public SubCategory? SubCategory { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? StockSale { get; set; }
        public decimal? StockStore { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? Utility { get; set; }
        public string? Imagen { get; set; }

    }
}
