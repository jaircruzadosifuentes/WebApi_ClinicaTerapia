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
    public class MovementsRepository : Repository, IMovementsRepository
    {
        public MovementsRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<SaleBuyOut> GetAllMovementsSaleBuyOut()
        {
            try
            {
                var saleBuyOuts = new List<SaleBuyOut>();
                var command = CreateCommand("PA_SALEBUY_GET_ALL_MOVEMENT_SALE_BUTOUT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        saleBuyOuts.Add(new SaleBuyOut
                        {
                            OperationType = new OperationType()
                            {
                                Name = reader["operation_description"].ToString()
                            },
                            Movement = new Movement()
                            {
                                MovementId = Convert.ToInt32(reader["id"].ToString())
                            },
                            Total = Convert.ToDecimal(reader["total_pay"].ToString()),
                            Igv = Convert.ToDecimal(reader["igv"].ToString()),
                            SubTotal = Convert.ToDecimal(reader["sub_total"].ToString()),
                            Serie = reader["serie"].ToString(),
                            Number = reader["number"].ToString(),
                            PayMethod = new PayMethod()
                            {
                                Description = reader["description"].ToString(),
                            },
                            CreatedAt = Convert.ToDateTime(reader["created_at"].ToString()),
                            PersonEmit = reader["person"].ToString(),
                            TypeTransaction = reader["type_transaction"].ToString(),
                            TypeTransactionValue = Convert.ToInt32(reader["type_transaction_value"].ToString()),
                            VoucherDocumentId = Convert.ToInt32(reader["voucher_document_id"].ToString())
                        });
                    }
                }

                return saleBuyOuts;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
