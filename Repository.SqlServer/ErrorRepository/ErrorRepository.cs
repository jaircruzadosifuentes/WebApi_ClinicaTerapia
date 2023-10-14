using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.ErrorRepository
{
    public class ErrorRepository : Repository, IErrorRepository
    {
        public ErrorRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public int InsertErrorRepository(Error error)
        {
            var command = CreateCommand(StoreProcedure.G_STORE_POST_INSERT_ERROR);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@description", error.Description);
            command.Parameters.AddWithValue("@description_trace", error.DescriptionTrace);
            command.Parameters.AddWithValue("@code_user", error.CodeUser);
            command.Parameters.AddWithValue("@created_at", error.CreatedAt);
            command.Parameters.AddWithValue("@type_error", error.TypeError);

            return Convert.ToInt32(command.ExecuteScalar());
        }
    }
}
