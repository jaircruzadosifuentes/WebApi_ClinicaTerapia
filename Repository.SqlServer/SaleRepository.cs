using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class SaleRepository : Repository, ISaleRepository
    {
        public SaleRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public Sale GetCorrelativoSale(int saleId)
        {
            var command = CreateCommand("PA_CORRELATIVO_GET_BY_ID_SALE");
            Sale? sale = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_sale_id", saleId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    sale = new()
                    {
                        Correlativo = reader["correlativo"].ToString(),
                    };
                }
            }
            return sale;
        }

        public bool PostSaveSaleDetailsProducts(SaleBuyOutProduct saleBuyOutProductale, int saleId)
        {
            try
            {
                var command = CreateCommand("PA_SALE_OR_BUYOUT_LIST_DETAIL_REGISTER_POST");
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_product_id", saleBuyOutProductale?.ProductId);
                command.Parameters.AddWithValue("@v_movement_sale_id", saleId);
                command.Parameters.AddWithValue("@v_quantity", saleBuyOutProductale?.Cantidad);
                command.Parameters.AddWithValue("@v_total", saleBuyOutProductale?.Total);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int PostSaveSaleHead(Sale sale)
        {
            int movementSaleId = 0;
            try
            {
                var command = CreateCommand("PA_SALE_OR_BUYOUT_HEAD_REGISTER_POST");
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new("@v_movement_sale_id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(paramId);

                command.Parameters.AddWithValue("@v_concept_pay", sale?.ConceptPay);
                command.Parameters.AddWithValue("@v_ex_change", sale?.ExChange);
                command.Parameters.AddWithValue("@v_cash_amount", sale?.CashAmount);
                command.Parameters.AddWithValue("@v_igv", sale?.Igv);
                command.Parameters.AddWithValue("@v_sub_total", sale?.SubTotal);
                command.Parameters.AddWithValue("@v_total", sale?.Total);
                command.Parameters.AddWithValue("@v_type_document_vou", sale?.TypeDocumentVouId);
                command.Parameters.AddWithValue("@v_is_client_manually_exists_in_data_base", sale?.IsClientManuallyExistsInDataBase);
                command.Parameters.AddWithValue("@v_is_client_manually_register", sale?.IsClientManuallyRegister);
                command.Parameters.AddWithValue("@v_pay_method_id", sale?.PayMethod?.Value);
                command.Parameters.AddWithValue("@v_names", sale?.Person?.Names);
                command.Parameters.AddWithValue("@v_surnames", sale?.Person?.Surnames);
                command.Parameters.AddWithValue("@v_employeed_id", sale?.EmployeedId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    movementSaleId = Convert.ToInt32(command.Parameters["@v_movement_sale_id"].Value);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return movementSaleId;
        }
    }
}
