using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<Product> GetProductoByCategoryIdSubCategoryId(int categoryId, int subCategoryId)
        {
            try
            {
                var products = new List<Product>();
                var command = CreateCommand("PA_GET_PRODUCT_BY_ID_CATEGORY_SUBCATEGORY");
                command.Parameters.AddWithValue("@v_category_id", categoryId);
                command.Parameters.AddWithValue("@v_sub_category_id", subCategoryId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = Convert.ToInt32(reader["id"].ToString()),
                            Name = reader["product"].ToString(),
                            Imagen = reader["imagen"].ToString(),
                            Category = new CategoryEntity()
                            {
                                Label = reader["category"].ToString(),
                            },
                            SubCategory = new SubCategory()
                            {
                                Label = reader["sub_category"].ToString()
                            },
                            Cantidad = Convert.ToDecimal(reader["cantidad"].ToString()),
                            Precio = Convert.ToDecimal(reader["precio"].ToString()),
                            StockSale = Convert.ToDecimal(reader["stock_sale"].ToString()),
                            StockStore = Convert.ToDecimal(reader["stock_store"].ToString()),
                            CostPrice = Convert.ToDecimal(reader["cost_price"].ToString()),
                            Utility = Convert.ToDecimal(reader["utility"].ToString()),
                        });
                    }
                }

                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
