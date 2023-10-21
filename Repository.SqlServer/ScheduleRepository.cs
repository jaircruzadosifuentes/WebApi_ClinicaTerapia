using Entities;
using Repository.Interfaces;
using System.Data.SqlClient;

namespace Repository.SqlServer
{
    public class ScheduleRepository : Repository, IScheduleRepository
    {
        public ScheduleRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public bool GenerateSchedule(PayDuesDetail payDuesDetail)
        {
            try
            {
                var command = CreateCommand("PA_GENERA_CRONOGRAMA_POST_PAYMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_debt_number", payDuesDetail?.Dues);
                command.Parameters.AddWithValue("@v_patient_id", payDuesDetail?.Patient?.PatientId);
                command.Parameters.AddWithValue("@v_initial_date", payDuesDetail?.InitialDate.ToString("yyyy/MM/dd"));

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<EmployeedDisponibilty> GetAllScheduleEmployeed(int employeedId)
        {
            try
            {
                var employeeds = new List<EmployeedDisponibilty>();
                var command = CreateCommand("PA_SCHEDULE_SESSIONS_GET_BY_ID_EMPLOYEED");
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeds.Add(new EmployeedDisponibilty
                        {
                            Name = reader["name"].ToString(),
                            startDateTime = Convert.ToDateTime(reader["startDateTime"]),
                            endDateTime = Convert.ToDateTime(reader["endDateTime"]),
                            State = Convert.ToInt32(reader["state"])
                        });
                    }
                }

                return employeeds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PayDuesDetail> GetAllSchedulePatient(int patientId)
        {
            try
            {
                var payDuesDetails = new List<PayDuesDetail>();
                var command = CreateCommand("PA_CRONOGRAMA_PAGOS_GET_PAYMENT");
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payDuesDetails.Add(new PayDuesDetail
                        {
                            PatientNew = new PatientNew()
                            {
                                SurNames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString()
                            },
                            Dues = Convert.ToInt32(reader["debt_number"].ToString()),
                            MountDue = Convert.ToDecimal(reader["amount"].ToString()),
                            InitialDate = Convert.ToDateTime(reader["payment_date"].ToString()),
                            State = reader["state"].ToString()
                        });
                    }
                }

                return payDuesDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
