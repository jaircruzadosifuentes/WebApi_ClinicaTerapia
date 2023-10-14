using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.PaymentRepository
{
    public class PaymentRepository : Repository, IPaymentRepository
    {
        public PaymentRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<Payment> GetPayments()
        {
            try
            {
                var payments = new List<Payment>();
                var command = CreateCommand("PA_GET_PAYMENT_GET_PAYMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payments.Add(new Payment
                        {
                            PaymentId = Convert.ToInt32(reader["id"].ToString()),
                            Patient = new Patient()
                            {
                                Person = new Person()
                                {
                                    Names = reader["names"].ToString(),
                                    Surnames = reader["surnames"].ToString(),
                                    ProfilePicture = reader["profile_picture"].ToString()
                                },
                                ClinicalHistory = new ClinicalHistory()
                                {
                                    PacketsOrUnitSessions = new PacketsOrUnitSessions()
                                    {
                                        Description = reader["packet"].ToString()
                                    }
                                },
                                UserNamePatient = reader["user_name"].ToString()
                            },
                            Total = Convert.ToDecimal(reader["total"].ToString()),
                            Igv = Convert.ToDecimal(reader["igv"].ToString()),
                            SubTotal = Convert.ToDecimal(reader["sub_total"].ToString()),
                            TotalCancelled = Convert.ToDecimal(reader["total_cancelled"].ToString()),
                            TotalDebt = Convert.ToDecimal(reader["total_debt"].ToString()),
                            DebtNumbertMax = Convert.ToInt32(reader["debt_number_max"].ToString()),
                            State = reader["state"].ToString()
                        });
                    }
                }

                return payments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PaymentScheduleDetail> GetPaymentsScheduleDetail(int paymentId)
        {
            try
            {
                var paymentScheduleDetails = new List<PaymentScheduleDetail>();
                var command = CreateCommand("PA_GET_DETAIL_SCHEDULE_GET_PAYMENT");
                command.Parameters.AddWithValue("@v_payment_id", paymentId);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        paymentScheduleDetails.Add(new PaymentScheduleDetail
                        {
                            PaymentScheduleDetailId = Convert.ToInt32(reader["id"].ToString()),
                            Payment = new Payment()
                            {
                                PaymentId = Convert.ToInt32(reader["payment_id"].ToString()),
                            },
                            DebtNumber = Convert.ToInt32(reader["debt_number"].ToString()),
                            Amount = Convert.ToDecimal(reader["amount"].ToString()),
                            State = reader["state"].ToString(),
                            PaymentDate = Convert.ToDateTime(reader["payment_date"].ToString())
                        });
                    }
                }

                return paymentScheduleDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutUpdateDebtPayment(PaymentScheduleDetail paymentScheduleDetail)
        {
            try
            {
                var command = CreateCommand("PA_PAY_DEBT_PUT_PAYMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_payment_schedule_id", paymentScheduleDetail?.PaymentScheduleDetailId);
                command.Parameters.AddWithValue("@v_payment_debt", paymentScheduleDetail?.DebtNumber);
                command.Parameters.AddWithValue("@v_payment_user", paymentScheduleDetail?.UserPayment);
                command.Parameters.AddWithValue("@v_concepto_pago", paymentScheduleDetail?.ConceptoPago);
                command.Parameters.AddWithValue("@v_pay_method_id", paymentScheduleDetail?.PayMethod?.Value);
                command.Parameters.AddWithValue("@v_cash", paymentScheduleDetail?.Cash);
                command.Parameters.AddWithValue("@v_monetary_exchange", paymentScheduleDetail?.MonetaryExchange);
                command.Parameters.AddWithValue("@v_type_document_vou", paymentScheduleDetail?.VoucherDocument?.Value);
                command.Parameters.AddWithValue("@v_is_new_customer", paymentScheduleDetail?.IsNewCustomer);


                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
