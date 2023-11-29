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
    public class EmployeedRepository : Repository, IEmployeedRepository
    {
        public EmployeedRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<Employeed> GetAllEmployeed()
        {
            try
            {
                var employeeds = new List<Employeed>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETALL_EMPLOYEED);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeds.Add(new Employeed
                        {
                            EmployeedId = Convert.ToInt32(reader["employeedId"]),
                            Label = reader["label"].ToString(),
                            Person = new Person()
                            {
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                                ProfilePicture = reader["profilePicture"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString()),
                                BirthDate = Convert.ToDateTime(reader["birth_date"].ToString())
                            },
                            Role = new Role()
                            {
                                RoleId = Convert.ToInt32(reader["roleId"]),
                                Name = reader["role"].ToString(),
                                Area = new Area()
                                {
                                    AreaDescription = reader["area"].ToString(),
                                }
                            },
                            State = reader["state"].ToString(),
                            User = reader["user_access"].ToString(),
                            AdmisionDate = Convert.ToDateTime(reader["admision_date"].ToString()),
                            Salary = new Salary()
                            {
                                MountSalary = Convert.ToDecimal(reader["salary"].ToString())
                            },
                            VacationDays = Convert.ToDecimal(reader["vacation_days"].ToString()),
                            UserName = reader["user_name"].ToString(),
                            AreaId = Convert.ToInt32(reader["area_id"].ToString()),
                            Campus = new Campus()
                            {
                                Name = reader["campus_name"].ToString()
                            },
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

        public IEnumerable<Employeed> GetAllEmployeedPendingAproval()
        {
            try
            {
                var employeeds = new List<Employeed>();
                var command = CreateCommand("PA_DATA_EMPLOYEED_PENDING_APROVAL_GET_EMPLOYEED");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeds.Add(new Employeed
                        {
                            EmployeedId = Convert.ToInt32(reader["employeedId"]),
                            Label = reader["label"].ToString(),
                            Person = new Person()
                            {
                                Names = reader["names"].ToString(),
                                Surnames = reader["surnames"].ToString(),
                                ProfilePicture = reader["profilePicture"].ToString(),
                                Age = Convert.ToInt32(reader["age"].ToString())
                            },
                            Role = new Role()
                            {
                                RoleId = Convert.ToInt32(reader["roleId"]),
                                Name = reader["role"].ToString()
                            },
                            State = reader["state"].ToString(),
                            User = reader["user_access"].ToString(),
                            AdmisionDate = Convert.ToDateTime(reader["admision_date"].ToString()),
                            Salary = new Salary()
                            {
                                MountSalary = Convert.ToDecimal(reader["salary"].ToString())
                            },
                            VacationDays = Convert.ToDecimal(reader["vacation_days"].ToString())
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

        public Employeed GetByUserNameEmployeed(string userName)
        {
            var command = CreateCommand("PA_DATAIL_EMPLOYEED_GET_BY_USERNAME_EMPLOYEED");
            Employeed? employeedEntity = new();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@v_user_name", userName);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {

                    Person person = new()
                    {
                        Names = reader["names"].ToString(),
                        Surnames = reader["sur_names"].ToString(),
                        ProfilePicture = reader["profile_picture"].ToString(),
                        Age = Convert.ToInt32(reader["age"].ToString()),
                        PersonEmail = new Email()
                        {
                            EmailDescription = reader["email"].ToString(),
                        },
                        BirthDate = Convert.ToDateTime(reader["birth_date"].ToString()),
                        CivilStatus = reader["civil_status"].ToString(),
                        PersonCellphone = new CellPhone()
                        {
                            CellPhoneNumber = reader["number_cellphone"].ToString()
                        },
                        Gender = reader["gender"].ToString(),
                    };
                    Role role = new()
                    {
                        Name = reader["role"].ToString(),
                        Area = new Area()
                        {
                            AreaId = Convert.ToInt32(reader["area_id"].ToString()),
                            AreaDescription = reader["area"].ToString(),
                        }
                    };
                    employeedEntity.Role = role;
                    employeedEntity.Person = person;
                    employeedEntity.State = reader["state"].ToString();
                    employeedEntity.UserName = reader["user_name"].ToString();
                    employeedEntity.IsStaff = Convert.ToBoolean(reader["isStaff"].ToString());
                    employeedEntity.EmployeedId = Convert.ToInt32(reader["employeedId"].ToString());
                }
            }
            return employeedEntity;
        }

        public IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId)
        {
            try
            {
                var employeeds = new List<EmployeedDisponibilty>();
                var command = CreateCommand("PA_SCHEDULE_DISPONIBILTY_GET_PATIENT_PROGRESS_SESION");
                command.Parameters.AddWithValue("@v_date_attention", dateToConsult);
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeeds.Add(new EmployeedDisponibilty
                        {
                            PersonnelInCharge = reader["persona_cargo"].ToString(),
                            DateProgramming = Convert.ToDateTime(reader["date_attention"]),
                            HourInitial = reader["hour_initial"].ToString(),
                            HourFinished = reader["hour_finished"].ToString(),
                            Name = reader["reason"].ToString(),
                            startDateTime = Convert.ToDateTime(reader["start_hour_initial"]),
                            endDateTime = Convert.ToDateTime(reader["finished_hour_finished"]),
                            State = Convert.ToInt32(reader["state"])
                        });
                    }
                }

                return employeeds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Employeed PostAccessSystem(Employeed employeed)
        {
            var command = CreateCommand("PA_ACCESS_SYSTEM_POST_EMPLOYEED");
            Employeed? employeedEntity = new();
            command.Parameters.AddWithValue("@v_user_access", employeed.User);
            command.Parameters.AddWithValue("@v_password", employeed.Password);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {

                    employeedEntity.EmployeedId = Convert.ToInt32(reader["id"]);
                    Person person = new()
                    {
                        Names = reader["names"].ToString(),
                        Surnames = reader["surnames"].ToString(),
                        ProfilePicture = reader["profile_picture"].ToString()
                    };
                    Role role = new()
                    {
                        Name = reader["role"].ToString(),
                    };
                    employeedEntity.Role = role;
                    employeedEntity.Person = person;
                    employeedEntity.State = reader["state"].ToString();
                    employeedEntity.StateAbbreviation = reader["abbreviation_state"].ToString();
                    employeedEntity.UserName = reader["user_name"].ToString();
                    employeedEntity.TypeUser = reader["typeUser"].ToString();
                    employeedEntity.EmployeedCashRegisterId = Convert.ToInt32(reader["employeed_cash_register_id"].ToString());

                }
            }
            return employeedEntity;
        }

        public bool PostRegisterEmployeed(Employeed employeed)
        {
            try
            {
                var command = CreateCommand("PA_REGISTER_EMPLOYEED_POST_EMPLOYEED");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_surnames", employeed?.Person?.Surnames);
                command.Parameters.AddWithValue("@v_names", employeed?.Person?.Names);
                command.Parameters.AddWithValue("@v_birthdate", employeed?.Person?.BirthDate.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_type_document_id", employeed?.Person?.PersonDocument?.Document?.value);
                command.Parameters.AddWithValue("@v_nro_document", employeed?.Person?.PersonDocument?.NroDocument);
                command.Parameters.AddWithValue("@v_gender", employeed?.Person?.Gender);
                command.Parameters.AddWithValue("@v_cellphone", employeed?.Person?.PersonCellphone?.CellPhoneNumber);
                command.Parameters.AddWithValue("@v_email", employeed?.Person?.PersonEmail?.EmailDescription);
                command.Parameters.AddWithValue("@v_company", employeed?.Experience?.Company);
                command.Parameters.AddWithValue("@v_still_works", employeed?.Experience?.StillWorks);
                command.Parameters.AddWithValue("@v_start_date", employeed?.Experience?.StartDate?.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_finish_date", employeed?.Experience?.FinishDate?.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_activities", employeed?.Experience?.Activities);
                command.Parameters.AddWithValue("@v_afp_id", employeed?.AfpSure?.Value);
                command.Parameters.AddWithValue("@v_associate_code", employeed?.AssociateCode);
                command.Parameters.AddWithValue("@v_afp_link_date", employeed?.AfpLinkDate?.ToString("yyyy/MM/dd"));
                command.Parameters.AddWithValue("@v_type_contract_id", employeed?.TypeOfContract?.Value);
                command.Parameters.AddWithValue("@v_modality_id", employeed?.ModalityContract?.Value);
                command.Parameters.AddWithValue("@v_role_id", employeed?.Role?.Value);
                command.Parameters.AddWithValue("@v_admision_date", employeed?.AdmisionDate?.ToString("yyyy/MM/dd"));

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutAppproveContractEmployeed(Employeed employeed)
        {
            try
            {
                var command = CreateCommand("PA_APPROVE_CONTRACT_PUT_EMPLOYEED");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_employeed_id", employeed?.EmployeedId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutUpdateProfile(string nameProfile, int id)
        {
            try
            {
                var command = CreateCommand("PA_UPDATE_PROFILE_EMPLOYEED_BY_ID_PUT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_name", nameProfile);
                command.Parameters.AddWithValue("@v_id", id);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
