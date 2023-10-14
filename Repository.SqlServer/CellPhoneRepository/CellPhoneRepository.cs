using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.CellPhoneRepository
{
    public class CellPhoneRepository : Repository, ICellPhoneRepository
    {
        public CellPhoneRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<CellPhone> GetAllByCellPhoneForPersonId(int personId)
        {
            try
            {
                var cellPhones = new List<CellPhone>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETBYID_CELLPHONE_FOR_PERSON);
                command.Parameters.AddWithValue("@v_person_id", personId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cellPhones.Add(new CellPhone
                        {
                            CellPhoneId = Convert.ToInt32(reader["cellPhoneId"]),
                            CellPhoneNumber = reader["cellPhoneNumber"].ToString(),
                            IsDefault = Convert.ToBoolean(reader["isDefault"]),
                            Operator = new Operator
                            {
                                OperatorId = Convert.ToInt32(reader["operatorId"]),
                                OperatorDescription = reader["operatorDescription"].ToString()
                            }
                        });
                    }
                }

                return cellPhones;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
