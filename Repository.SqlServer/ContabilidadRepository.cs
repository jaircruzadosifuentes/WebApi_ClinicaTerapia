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
    public class ContabilidadRepository : Repository, IContabilidadRepository
    {
        public ContabilidadRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public CajaChica DetailDataEmployeedCajaChica(int employeedId, DateTime dateApertu)
        {
            var command = CreateCommand("PA_DETAIL_DATA_EMP_GET_BY_ID_AND_FECHA");
            CajaChica? cajaChica = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_employeed_id", employeedId);
            command.Parameters.AddWithValue("@v_fecha", dateApertu.ToString("dd/MM/yyyy"));
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    cajaChica = new()
                    {
                        Employeed = reader["person"].ToString(),
                        Campus = reader["campus"].ToString(),
                        CashRegister = reader["caja"].ToString(),
                        IsApertu = Convert.ToInt32(reader["is_aperturado"].ToString()),
                        State = reader["state"].ToString(),
                        MontoAperturado = Convert.ToDecimal(reader["amount_apertu"].ToString())
                    };
                }
            }
            return cajaChica;
        }

        public List<CajaChicaMontos> GetCajaChicaMontos(DateTime dateOpened, int employeedCashId)
        {
            var command = CreateCommand("PA_HEAD_CAJA_CHICA_GET_BY_CAJA_ID_DATE");
            List<CajaChicaMontos>? lstCajaChicaMontos = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_opening_date", dateOpened);
            command.Parameters.AddWithValue("@v_employeed_cash_register_id", employeedCashId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lstCajaChicaMontos.Add(new CajaChicaMontos
                    {
                        CajaChicaMontosId = Convert.ToInt32(reader["id"].ToString()),
                        Amount = Convert.ToDecimal(reader["amount"]),
                        Description = reader["description"].ToString(),
                        Color = reader["color"].ToString()
                    });
                }
            }
            return lstCajaChicaMontos;
        }

        public List<SaleBuyOut> GetDetailMovementsCajaChica(DateTime dateOpened, int employeedCashId)
        {
            try
            {
                var saleBuyOuts = new List<SaleBuyOut>();
                var command = CreateCommand("PA_DETAIL_MOVEMENTS_CAJA_CHICA_GET_BY_DATE_CASH_REGISTER");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_date_consult", dateOpened);
                command.Parameters.AddWithValue("@v_cash_register_id", employeedCashId);
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
                            Total = Convert.ToDecimal(reader["total"].ToString()),
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
                            VoucherDocumentId = Convert.ToInt32(reader["voucher_document_id"].ToString()),
                            EmployeedCashRegisterId = Convert.ToInt32(reader["employeed_cash_register_id"].ToString()),
                            VoucherDocument = new VoucherDocument()
                            {
                                Label = reader["voucher_name"].ToString(),
                            }
                            
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

        public List<CajaChica> GetHistDetailCajaChicaByIdEmployeed(int employeedId)
        {
            var command = CreateCommand("PA_HIST_CAJA_CHICA_GET_BY_ID_EMPLOYEED");
            List<CajaChica>? lstHistCajaChica = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_employeed_id", employeedId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    lstHistCajaChica.Add(new CajaChica
                    {
                        Employeed = reader["employeed"].ToString(),
                        Campus = reader["campus"].ToString(),
                        CashRegister = reader["caja"].ToString(),
                        MontoAperturado = Convert.ToDecimal(reader["opened_amount"].ToString()),
                        FechaApertu = Convert.ToDateTime(reader["opening_date"].ToString()),
                        IsApertu = Convert.ToInt32(reader["is_close"].ToString())
                    });
                }
            }
            return lstHistCajaChica;
        }

        public bool PostApertuCajaChica(CajaChica cajachica)
        {
            try
            {
                var command = CreateCommand("PA_INS_REGISTER_CAJA_CHICA_POST");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_employeed_id", cajachica.EmployeedCashId);
                command.Parameters.AddWithValue("@v_amount", cajachica.MontoAperturado);
                command.Parameters.AddWithValue("@v_opening_date", cajachica?.FechaApertu?.ToString("dd/MM/yyyy"));

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostCloseCajaChica(CajaChica cajachica)
        {
            try
            {
                var command = CreateCommand("PA_INS_CASH_DETAIL_POST");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_monto_aperturado", cajachica.MontoAperturado);
                command.Parameters.AddWithValue("@v_monto_vendido", cajachica.MontoVendido);
                command.Parameters.AddWithValue("@v_monto_egreso", cajachica.MontoEgreso);
                command.Parameters.AddWithValue("@v_monto_esperado", cajachica.MontoEsperado);
                command.Parameters.AddWithValue("@v_fecha_cierre", cajachica?.FechaCierre?.ToString("dd/MM/yyyy"));
                command.Parameters.AddWithValue("@v_employeed_caja_chica_id", cajachica?.EmployeedCashId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CajaChica VerifyCajaChica(DateTime dateOpened, int employeedCashId)
        {
            var command = CreateCommand("PA_VERIFY_CAJA_CACHICA_GET_BY_DATE_APER_EMPLOYEED_CASH_ID");
            CajaChica? cajaChica = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_fecha_apertura", dateOpened.ToString("dd/MM/yyyy"));
            command.Parameters.AddWithValue("@v_employeed_cash_id", employeedCashId);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    cajaChica = new()
                    {   
                        IsApertu = Convert.ToInt32(reader["closed_cash_register"].ToString()),
                        MessageState = reader["message"].ToString(),
                    };
                }
            }
            return cajaChica;
        }
    }
}
