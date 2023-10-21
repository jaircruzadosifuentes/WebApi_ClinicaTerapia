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
    public class PatientInAttentionRepository : Repository, IPatientInAttentionRepository
    {
        public PatientInAttentionRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<PatientInAttention> GetAllPatientInAttention()
        {
            try
            {
                var patientInAttentions = new List<PatientInAttention>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETALL_PATIENT_IN_ATTENTION);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientInAttentions.Add(new PatientInAttention
                        {
                            Nro = Convert.ToInt32(reader["NRO"]),
                            PatientNamesAttention = reader["EN ATENCIÓN"].ToString(),
                            InAttentionFor = reader["EN ATENCION POR"].ToString(),
                            HourReserved = reader["HORA RESERVADA"].ToString()
                        });
                    }
                }

                return patientInAttentions;
            }
            catch (Exception)
            {
                throw;
            };
        }
    }
}
