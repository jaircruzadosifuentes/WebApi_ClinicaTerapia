using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer
{
    public class SolicitudAttentionRepository : Repository, ISolicitudAttentionRepository
    {
        public SolicitudAttentionRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<Patient> GetPacientesConPrimeraAtencionClinica()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PACIENTES_PRIMER_ANALISIS_GET_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["id"].ToString()),
                            Person = new Person()
                            {
                                Names = reader["names_patient"].ToString(),
                                Surnames = reader["surnames_patient"].ToString(),
                                Age = Convert.ToInt32(reader["age"]),
                                BirthDate = Convert.ToDateTime(reader["birth_date"].ToString()),
                                PersonDocument = new PersonDocument()
                                {
                                    NroDocument = reader["number_document"].ToString()
                                },
                                ProfilePicture = reader["profile_picture"].ToString()
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                HourAttention = reader["hour_attention"].ToString(),
                                DateAttention = Convert.ToDateTime(reader["date_attention"].ToString()),
                                Employeed = new Employeed()
                                {
                                    Person = new Person()
                                    {
                                        Names = reader["names_employeed"].ToString(),
                                        Surnames = reader["surnames_employeed"].ToString(),
                                    }
                                },
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["state"].ToString()
                            },
                            ClinicalHistory = new ClinicalHistory()
                            {
                                PacketsOrUnitSessions = new PacketsOrUnitSessions()
                                {
                                    PacketsOrUnitSessionsId = Convert.ToInt32(reader["packet_id"].ToString()),
                                    CostPerUnit = Convert.ToDecimal(reader["cost_per_unit"].ToString()),
                                    NumberSessions = Convert.ToInt32(reader["number_sessions"].ToString()),
                                    MaximumFeesToPay = Convert.ToInt32(reader["maximum_fees_to_pay"].ToString()),
                                    Abbreviation = reader["abbreviation"].ToString(),
                                },
                                FrecuencyId = Convert.ToInt32(reader["frecuency_id"].ToString()),
                            },
                            ScheduleGenerate = Convert.ToBoolean(reader["payment_schedule"].ToString()),
                            Pay = new Pay()
                            {
                                DateInitial = Convert.ToDateTime(reader["initial_date"].ToString())
                            }
                        });
                    }
                }

                return patients;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Patient> GetPatientsSolicitudeInDraft()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_GET_SOLICITUD_IN_DRAFT_GET_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Person = new Person()
                            {
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                                BirthDate = Convert.ToDateTime(reader["birth_date"].ToString()),
                                PersonDocument = new PersonDocument()
                                {
                                    Document = new Document()
                                    {
                                        value = Convert.ToInt32(reader["document_id"].ToString())
                                    },
                                    NroDocument = reader["number_document"].ToString()
                                },
                                PersonCellphone = new CellPhone()
                                {
                                    CellPhoneNumber = reader["number_cellphone"].ToString()
                                },
                                PersonEmail = new Email()
                                {
                                    EmailDescription = reader["email"].ToString()
                                },
                                Gender = reader["gender"].ToString()
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                HourAttention = reader["hour_attention"].ToString(),
                                DateAttention = Convert.ToDateTime(reader["date_attention"].ToString()),
                                Employeed = new Employeed()
                                {
                                    EmployeedId = Convert.ToInt32(reader["employeed_id"].ToString()),
                                    Person = new Person()
                                    {
                                        Names = reader["names_employeed"].ToString(),
                                        Surnames = reader["surnames_employeed"].ToString(),
                                    }
                                },
                            },

                        });
                    }
                }

                return patients;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertFirstClinicalAnalysis(Patient patient)
        {
            try
            {
                var command = CreateCommand("PA_REGISTER_FIRST_ATTENCION_CLINIC_HISTORY");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patient?.PatientId);
                command.Parameters.AddWithValue("@v_weight", patient?.ClinicalHistory?.Weight);
                command.Parameters.AddWithValue("@v_disease", patient?.ClinicalHistory?.Disease);
                command.Parameters.AddWithValue("@v_has_disease", patient?.ClinicalHistory?.HasDisease);
                command.Parameters.AddWithValue("@v_has_operation", patient?.ClinicalHistory?.HasOperation);
                command.Parameters.AddWithValue("@v_desc_operation", patient?.ClinicalHistory?.DescriptionOperation);
                command.Parameters.AddWithValue("@v_physical_exploration", patient?.ClinicalHistory?.PhysicalExploration);
                command.Parameters.AddWithValue("@v_shadow_pain", patient?.ClinicalHistory?.ShadowPain);
                command.Parameters.AddWithValue("@v_diagnosis", patient?.ClinicalHistory?.DescriptionDiagnostica);
                command.Parameters.AddWithValue("@v_take_medicina", patient?.ClinicalHistory?.TakeMedicina);
                command.Parameters.AddWithValue("@v_desc_medic", patient?.ClinicalHistory?.DescriptionMedic);
                command.Parameters.AddWithValue("@v_packet_id", patient?.ClinicalHistory?.PacketsOrUnitSessions?.PacketsOrUnitSessionsId);
                command.Parameters.AddWithValue("@v_frecuency_id", patient?.ClinicalHistory?.FrecuencyId);
                command.Parameters.AddWithValue("@v_information_additional", patient?.ClinicalHistory?.InformationAdditional);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertNewSolicitudAttention(Patient patient)
        {
            try
            {
                var command = CreateCommand(StoreProcedure.G_STORE_POST_REGISTER_SOLICITUD_ATTENTION);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_names", patient?.Person?.Names);
                command.Parameters.AddWithValue("@v_surNames", patient?.Person?.Surnames);
                command.Parameters.AddWithValue("@v_birthDate", patient?.Person?.BirthDate);
                command.Parameters.AddWithValue("@v_documentId", patient?.Person?.PersonDocument?.Document?.value);
                command.Parameters.AddWithValue("@v_documentNro", patient?.Person?.PersonDocument?.NroDocument);
                command.Parameters.AddWithValue("@v_reservedDay", patient?.PatientSolicitude?.DateAttention.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_hourReservedDay", patient?.PatientSolicitude?.HourAttention);
                command.Parameters.AddWithValue("@v_employeedId", patient?.PatientSolicitude?.Employeed?.EmployeedId);
                command.Parameters.AddWithValue("@v_save_in_draft", patient?.SaveInDraft);
                command.Parameters.AddWithValue("@v_cellphone", patient?.Person?.PersonCellphone?.CellPhoneNumber);
                command.Parameters.AddWithValue("@v_email", patient?.Person?.PersonEmail?.EmailDescription);
                command.Parameters.AddWithValue("@v_gender", patient?.Person?.Gender);

                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
