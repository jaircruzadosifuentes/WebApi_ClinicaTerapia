using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.PatientRepository
{
    public class PatientInQueueRepository : Repository, IPatientInQueueRepository
    {
        public PatientInQueueRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<PatientInQueue> GetAllPatientInQueues()
        {
            try
            {
                var patientInQueues = new List<PatientInQueue>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETALL_PATIENT_IN_QUEUE);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientInQueues.Add(new PatientInQueue
                        {
                            Nro = Convert.ToInt32(reader["NRO"]),
                            PatientWaiting = reader["EN ESPERA"].ToString(),
                            Reason = reader["MOTIVO"].ToString(),
                            HourReserved = reader["HORA RESERVADA"].ToString()
                        });
                    }
                }

                return patientInQueues;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
