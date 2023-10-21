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
    public class PatientRepository : Repository, IPatientRepository
    {
        public PatientRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public bool PutApproveSolicitude(int patientId, string type)
        {
            try
            {
                var command = CreateCommand("PA_APPOINTMENT_UPDATE_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                command.Parameters.AddWithValue("@v_type", type);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ApprovePatientNew(int patientId, string type)
        {
            try
            {
                var command = CreateCommand(StoreProcedure.G_STORE_UPDATE_APPROVE_PATIENT_NEW);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                command.Parameters.AddWithValue("@v_date_remove", DateTime.Now.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_type", type);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PatientProgress> GetAdvanceCliniciForPatientId(int patientId)
        {
            try
            {
                var patientProgresses = new List<PatientProgress>();
                var command = CreateCommand(StoreProcedure.G_STORE_GET_BY_ID_ADVANCE_CLINIC);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patientId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientProgresses.Add(new PatientProgress
                        {
                            ProgressDescription = reader["progress_description"].ToString(),
                            Recommendation = reader["recommendation"].ToString(),
                            DateOfAttention = Convert.ToDateTime(reader["date_of_attention"].ToString()),
                            HourOffAttention = reader["hour_attention"].ToString(),
                            IsAttention = Convert.ToBoolean(reader["is_atention"].ToString()),
                            IsQueueRemoval = Convert.ToBoolean(reader["attendance"].ToString()),
                            SystemHour = reader["system_hour"].ToString()
                        });
                    }
                }

                return patientProgresses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Patient> GetAllPatientsInPercentajeTreatment(int patientId)
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PERCENTAGE_TREATMENT_GETBYID_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_id", patientId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Person = new Person()
                            {
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                ProfilePicture = reader["profile_picture"].ToString(),
                            },
                            Percentaje = Convert.ToDecimal(reader["percentage"].ToString()),
                            DateInitial = Convert.ToDateTime(reader["date_initial"].ToString()),
                            DateFinished = Convert.ToDateTime(reader["date_finished"].ToString()),
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

        public IEnumerable<Patient> GetAllPatientsInTreatment()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PATIENT_IN_TREATMENT_GET_PATIENT");
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
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                ProfilePicture = reader["profile_picture"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString())
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["state"].ToString()
                            },
                            ClinicalHistory = new ClinicalHistory()
                            {
                                PacketsOrUnitSessions = new PacketsOrUnitSessions()
                                {
                                    Abbreviation = reader["packet_desc"].ToString()
                                },
                                Frecuency = new Frecuency()
                                {
                                    FrecuencyDescription = reader["frecuency_desc"].ToString()
                                }
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                Employeed = new Employeed()
                                {
                                    EmployeedId = Convert.ToInt32(reader["employeed_id"].ToString())
                                }
                            },
                            UserNamePatient = reader["user_name"].ToString()
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

        public IEnumerable<Patient> GetAllPatientsNewAttentionByEmployeedId(int employeedId)
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_SOLICITUDE_APPROVE_GET_BY_ID_PATIENT");
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
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
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString()),
                                ProfilePicture = reader["profile_picture"].ToString(),
                                BirthDate = Convert.ToDateTime(reader["birth_date"]),
                                PersonDocument = new PersonDocument()
                                {
                                    NroDocument = reader["number_document"].ToString()
                                },
                                PersonEmail = new Email()
                                {
                                    EmailDescription = reader["email"].ToString()
                                }
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                DateAttention = Convert.ToDateTime(reader["date_attention"].ToString()),
                                HourAttention = reader["hour_attention"].ToString(),
                                TimeAttention = Convert.ToInt32(reader["time"].ToString()),
                                SystemHour = reader["system_hour"].ToString(),
                                Employeed = new Employeed()

                                {
                                    Person = new Person()
                                    {
                                        Names = reader["names"].ToString(),
                                        Surnames = reader["surnames"].ToString()
                                    }
                                }
                            },
                            Reason = reader["reason"].ToString(),
                            UserNamePatient = reader["user_name"].ToString(),
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

        public IEnumerable<Patient> GetAllPatientsPatientWithAppoiment()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand(StoreProcedure.G_STORE_PA_APPOINTMENT_GET_PATIENTSOLICITUDE);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            PatientId = Convert.ToInt32(reader["id"].ToString()),
                            PatientCode = reader["code"].ToString(),
                            Person = new Person()
                            {
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString()),
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["state"].ToString()
                            },
                            NumberDocument = reader["number_document"].ToString(),
                            Operator = reader["operator"].ToString(),
                            Cellphone = reader["cellphone"].ToString(),
                            Email = reader["email"].ToString(),
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

        public IEnumerable<Patient> GetAllPatientsPendApro()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_APPOINTMENT_PENDING_GET_PATIENTSOLICITUDE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Correlative = Convert.ToInt32(reader["NRO"].ToString()),
                            State = reader["state"].ToString(),
                            Reason = reader["reason"].ToString(),
                            PatientId = Convert.ToInt32(reader["id"]),
                            Person = new Person()
                            {
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString()
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                DateAttention = Convert.ToDateTime(reader["date_attention"].ToString()),
                                HourAttention = reader["hour_attention"].ToString(),
                                Employeed = new Employeed()
                                {
                                    Person = new Person()
                                    {
                                        Names = reader["names_employeed"].ToString(),
                                        Surnames = reader["sur_names_employeed"].ToString(),
                                    },
                                    Role = new Role()
                                    {
                                        Name = reader["role"].ToString()
                                    }
                                }
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

        public IEnumerable<PatientValidateSchedule> GetAllPatientsWithSchedule(string hourInitial, string hourFinished, DateTime fechaReserved, int employeedId)
        {
            try
            {
                var employeeds = new List<PatientValidateSchedule>();
                var command = CreateCommand(StoreProcedure.G_STORE_VALIDA_SCHEDULE_OPEN);
                command.Parameters.AddWithValue("@v_hora_inicio", hourInitial);
                command.Parameters.AddWithValue("@v_hora_fin", hourFinished);
                command.Parameters.AddWithValue("@v_fecha_reservada", fechaReserved);
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeds.Add(new PatientValidateSchedule
                        {
                            PatientWithSchedule = reader["PACIENTE"].ToString(),
                            HourInitial = reader["HORA INICIO"].ToString(),
                            HourFinished = reader["HORA FIN"].ToString(),
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

        public IEnumerable<PatientProgress> GetByIdPatientProgress(int progressPatientId)
        {
            try
            {
                var patientProgress = new List<PatientProgress>();
                var command = CreateCommand("PA_PATIENT_PROGRESS_DETAIL_GEBYID_PATIENT_PROGRESS_SESION");
                command.Parameters.AddWithValue("@v_patient_progress_id", progressPatientId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientProgress.Add(new PatientProgress
                        {
                            PatientProgressId = Convert.ToInt32(reader["id"].ToString()),
                            Patient = new Patient()
                            {
                                Person = new Person()
                                {
                                    Names = reader["name_patient"].ToString(),
                                    Surnames = reader["surnames_patient"].ToString(),
                                    ProfilePicture = reader["profile_picture"].ToString()
                                },
                                UserNamePatient = reader["user_name_patient"].ToString()
                            },
                            SessionNumber = Convert.ToInt32(reader["session_number"].ToString()),
                            IsAttention = Convert.ToBoolean(reader["attended"].ToString()),
                            DateOfAttention = Convert.ToDateTime(reader["session_date"].ToString()),
                            HourOffAttention = reader["session_hour"].ToString(),
                            SystemHour = reader["system_hour"].ToString(),
                            Employeed = new Employeed()
                            {
                                EmployeedId = Convert.ToInt32(reader["employeed_id"].ToString()),
                                Person = new Person()
                                {
                                    Names = reader["names_employeed"].ToString(),
                                    Surnames = reader["surnames_employeed"].ToString(),
                                },
                                Role = new Role()
                                {
                                    Name = reader["role_employeed"].ToString()
                                },
                                UserName = reader["user_name"].ToString()
                            },
                            IsQueueRemoval = Convert.ToBoolean(reader["on_hold"].ToString()),

                        });
                    }
                }

                return patientProgress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PatientProgress> GetSessionForPatientId(int patientId)
        {
            try
            {
                var patientProgress = new List<PatientProgress>();
                var command = CreateCommand("PA_SESSIONS_PATIENT_GETBYID_PATIENT");
                command.Parameters.AddWithValue("@v_patient_id", patientId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patientProgress.Add(new PatientProgress
                        {
                            DateOfAttention = Convert.ToDateTime(reader["session_date"].ToString()),
                            HourOffAttention = reader["session_hour"].ToString(),
                            SessionNumber = Convert.ToInt32(reader["session_number"].ToString()),
                            PatientProgressId = Convert.ToInt32(reader["id"].ToString()),
                            TimeDemoration = Convert.ToInt32(reader["time_demoration"].ToString()),
                            IsAttention = Convert.ToBoolean(reader["attended"].ToString()),
                            IsQueueRemoval = Convert.ToBoolean(reader["on_hold"].ToString()),
                            SystemHour = reader["system_hour"].ToString(),
                            IsFlag = Convert.ToBoolean(reader["is_flag"].ToString()),
                            State = reader["state"].ToString(),
                            Employeed = new Employeed()
                            {
                                EmployeedId = Convert.ToInt32(reader["employeed_id"].ToString()),
                            }
                        });
                    }
                }

                return patientProgress;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostRegistrProgressSesion(PatientProgress patientProgress)
        {
            try
            {
                var command = CreateCommand("PA_PROGRESS_SESION_POST_PATIENT_PROGRESS_SESION_DETAIL");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_description", patientProgress.ProgressDescription);
                command.Parameters.AddWithValue("@v_patient_recommendation", patientProgress.Recommendation);
                command.Parameters.AddWithValue("@v_patient_progress_id", patientProgress.PatientProgressId);
                command.Parameters.AddWithValue("@v_time", patientProgress.Time);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutUpdateHourSesion(PatientProgress patientProgress)
        {
            try
            {
                var command = CreateCommand("PA_UPDATE_HOUR_EMPLOYEED_UPDATE_PATIENT_PROGRESS_SESION");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_patient_progress_id", patientProgress.PatientProgressId);
                command.Parameters.AddWithValue("@v_patient_hour_off_attention", patientProgress.HourOffAttention);
                command.Parameters.AddWithValue("@v_employeed_id", patientProgress?.Employeed?.EmployeedId);
                //command.Parameters.AddWithValue("@v_date_of_attention", patientProgress?.DateOfAttention);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Patient> GetAllPatientsFinishedTreatment()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PATIENT_FINISHED_TREATMENT_GET_PATIENT");
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
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                ProfilePicture = reader["profile_picture"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString())
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["state"].ToString()
                            },
                            ClinicalHistory = new ClinicalHistory()
                            {
                                PacketsOrUnitSessions = new PacketsOrUnitSessions()
                                {
                                    Abbreviation = reader["packet_desc"].ToString()
                                },
                                Frecuency = new Frecuency()
                                {
                                    FrecuencyDescription = reader["frecuency_desc"].ToString()
                                }
                            },
                            UserNamePatient = reader["user_name_patient"].ToString()
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

        public IEnumerable<Patient> GetAllPatientsInWaiting()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PATIENT_IN_WAITING_GET_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Person = new Person()
                            {
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                ProfilePicture = reader["profile_picture"].ToString(),
                                PersonCellphone = new CellPhone()
                                {
                                    CellPhoneNumber = reader["number_cellphone"].ToString()
                                }
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["reason"].ToString()
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                DateAttention = Convert.ToDateTime(reader["date_attention"].ToString()),
                                HourAttention = reader["hour_attention"].ToString(),
                                Employeed = new Employeed()
                                {
                                    Person = new Person()
                                    {
                                        Names = reader["names_employeed"].ToString(),
                                        Surnames = reader["surnames_employeed"].ToString(),
                                        ProfilePicture = reader["profile_picture_employeed"].ToString(),
                                        PersonCellphone = new CellPhone()
                                        {
                                            CellPhoneNumber = reader["number_cellphone_em"].ToString()
                                        }
                                    },
                                    Role = new Role()
                                    {
                                        Name = reader["role"].ToString()
                                    },
                                    UserName = reader["user_name"].ToString()
                                }
                            },
                            UserNamePatient = reader["user_name_patient"].ToString()
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

        public IEnumerable<Patient> GetAllPatientsInAttention()
        {
            try
            {
                var patients = new List<Patient>();
                var command = CreateCommand("PA_PATIENT_IN_ATTENTION_GET_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        patients.Add(new Patient
                        {
                            Person = new Person()
                            {
                                Surnames = reader["surnames"].ToString(),
                                Names = reader["names"].ToString(),
                                ProfilePicture = reader["profile_picture"].ToString(),
                            },
                            PatientState = new PatientState()
                            {
                                Description = reader["reason"].ToString()
                            },
                            PatientSolicitude = new PatientSolicitude()
                            {
                                DateAttention = Convert.ToDateTime(reader["session_date"].ToString()),
                                HourAttention = reader["session_hour"].ToString(),
                                Employeed = new Employeed()
                                {
                                    Person = new Person()
                                    {
                                        Names = reader["names_employeed"].ToString(),
                                        Surnames = reader["surnames_employeed"].ToString(),
                                        ProfilePicture = reader["profile_picture_employeed"].ToString(),
                                    },
                                    Role = new Role()
                                    {
                                        Name = reader["role"].ToString()
                                    }
                                }
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
    }
}
