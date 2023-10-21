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
    public class PayRepository : Repository, IPayRepository
    {
        public PayRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<PayDuesDetail> GetPayDueDetailForPatientId(int patientId)
        {
            try
            {
                var payDuesDetails = new List<PayDuesDetail>();
                var command = CreateCommand("PA_PAY_DETAIL_GETBYID_PAYMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payDuesDetails.Add(new PayDuesDetail
                        {
                            PayDuesDetailId = Convert.ToInt32(reader["id"].ToString()),
                            Dues = Convert.ToInt32(reader["debt_number"].ToString()),
                            MountDue = Convert.ToDecimal(reader["amount"].ToString()),
                            PaymentDay = Convert.ToDateTime(reader["payment_date"].ToString()),
                            Patient = new Patient()
                            {
                                ClinicalHistory = new ClinicalHistory()
                                {
                                    Frecuency = new Frecuency()
                                    {
                                        FrecuencyDescription = reader["frecuency_description"].ToString()
                                    },
                                    PacketsOrUnitSessions = new PacketsOrUnitSessions()
                                    {
                                        Description = reader["packet_description"].ToString()
                                    }
                                },
                                Person = new Person()
                                {
                                    Names = reader["names"].ToString(),
                                    Surnames = reader["surnames"].ToString()
                                }
                            },
                            State = reader["state"].ToString(),
                            Payment = new Payment()
                            {
                                PaymentId = Convert.ToInt32(reader["payment_id"].ToString())
                            }
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

        public IEnumerable<PayDuesDetailHistory> GetPayDueDetailForPatientIdHistory(int paymentId)
        {
            try
            {
                var payDuesDetailHistories = new List<PayDuesDetailHistory>();
                var command = CreateCommand("PA_PAY_DETAIL_HISTORY_GETBYID_PAYMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_payment_id", paymentId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payDuesDetailHistories.Add(new PayDuesDetailHistory
                        {
                            DatePaymentCanceled = Convert.ToDateTime(reader["payment_date_canceled"].ToString()),
                            DebtNumber = Convert.ToInt32(reader["debt_number"].ToString()),
                            Amount = Convert.ToDecimal(reader["amount"].ToString()),
                            UserPayment = reader["user_payment"].ToString(),
                        });
                    }
                }

                return payDuesDetailHistories;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostInsertPaySolicitud(Pay pay)
        {
            try
            {
                var command = CreateCommand("PA_REGISTRO_FINALIZA_SOLICITUD_POST_PATIENT_SOLICITUDE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", pay?.Patient?.PatientId);
                command.Parameters.AddWithValue("@v_initial_date", pay?.DateInitial.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_initial_hour", pay?.HourInitial);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
