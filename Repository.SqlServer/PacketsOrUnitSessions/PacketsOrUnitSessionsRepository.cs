using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.PacketsOrUnitSessionsRepository
{
    public class PacketsOrUnitSessionsRepository : Repository, IPacketsOrUnitSessionsRepository
    {
        public PacketsOrUnitSessionsRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<PacketsOrUnitSessions> GetAllPacketsOrUnitSessions()
        {
            try
            {
                var packetsOrUnitSessions = new List<PacketsOrUnitSessions>();
                var command = CreateCommand("PA_REGISTER_ATTENTION_GET_PACKETS_OR_UNIT_SESSIONES");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        packetsOrUnitSessions.Add(new PacketsOrUnitSessions
                        {
                            PacketsOrUnitSessionsId = Convert.ToInt32(reader["id"]),
                            Description = reader["description"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"]),
                            NumberSessions = Convert.ToInt32(reader["number_sessions"]),
                            CostPerUnit = Convert.ToDecimal(reader["cost_per_unit"]),
                            Abbreviation = reader["abbreviation"].ToString(),
                            MaximumFeesToPay = Convert.ToInt32(reader["maximum_fees_to_pay"]),
                            State = reader["state"].ToString(),
                            StateValue = Convert.ToBoolean(reader["state_value"].ToString())
                        });
                    }
                }

                return packetsOrUnitSessions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostRegisterPacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            try
            {
                var command = CreateCommand("PA_REGISTER_POST_PACKETS_OR_UNIT_SESSIONS");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_description", packetsOrUnitSessions.Description);
                command.Parameters.AddWithValue("@v_number_sessions", packetsOrUnitSessions.NumberSessions);
                command.Parameters.AddWithValue("@v_cost_per_unit", packetsOrUnitSessions.CostPerUnit);
                command.Parameters.AddWithValue("@v_abbreviation", packetsOrUnitSessions.Abbreviation);
                command.Parameters.AddWithValue("@v_maximum_fees_to_pay", packetsOrUnitSessions.MaximumFeesToPay);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutUpdatePacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            try
            {
                var command = CreateCommand("PA_UPDATE_PACKETS_PUT_PACKETS_OR_UNIT_SESSIONS");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_packet_id", packetsOrUnitSessions.PacketsOrUnitSessionsId);
                command.Parameters.AddWithValue("@v_number_sessions", packetsOrUnitSessions.NumberSessions);
                command.Parameters.AddWithValue("@v_cost_per_unit", packetsOrUnitSessions.CostPerUnit);
                command.Parameters.AddWithValue("@v_abbreviation", packetsOrUnitSessions.Abbreviation);
                command.Parameters.AddWithValue("@v_maximum_fees_to_pay", packetsOrUnitSessions.MaximumFeesToPay);
                command.Parameters.AddWithValue("@v_description", packetsOrUnitSessions.Description);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
