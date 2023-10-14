using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.CommonRepository
{
    public class CommonRepository : Repository, ICommonRepository
    {
        public CommonRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<Config> GetAllConfigs()
        {
            try
            {
                var configs = new List<Config>();
                var command = CreateCommand("PA_CONFIG_GENERAL_GET_CONFIG");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        configs.Add(new Config
                        {   ConfigId = Convert.ToInt32(reader["id"].ToString()),
                            Name = reader["key_name_config"].ToString(),
                            Value = reader["value"].ToString(),
                        });
                    }
                }

                return configs;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<Frecuency> GetAllFrecuency()
        {
            try
            {
                var frecuencies = new List<Frecuency>();
                var command = CreateCommand("PA_GET_FRECUENCY_GET_FRECUENCY");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        frecuencies.Add(new Frecuency
                        {
                            FrecuencyId = Convert.ToInt32(reader["frecuencyId"]),
                            Value = Convert.ToInt32(reader["value"]),
                            FrecuencyDescription = reader["description"].ToString(),
                            Abbreviation = reader["abbreviation"].ToString(),
                            State = reader["state"].ToString(),
                        });
                    }
                }

                return frecuencies;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PayMethod> GetAllPayMethods()
        {
            try
            {
                var payMethods = new List<PayMethod>();
                var command = CreateCommand("PA_PAYMETHODS_GET_PAY_METHOD");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payMethods.Add(new PayMethod
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                            Description = reader["description"].ToString(),
                            HaveConcept = Convert.ToBoolean(reader["have_concept"].ToString())
                        });
                    }
                }

                return payMethods;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Area> GetAreasInSelect()
        {
            try
            {
                var areas = new List<Area>();
                var command = CreateCommand("PA_AREA_IN_COMBO_GET_AREA");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        areas.Add(new Area
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                        });
                    }
                }
                return areas;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Dashboard> GetCountPatientsType()
        {
            try
            {
                var dashboards = new List<Dashboard>();
                var command = CreateCommand("PA_GET_COUNT_PATIENTS_GET_PATIENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dashboards.Add(new Dashboard
                        {
                            Size = Convert.ToInt32(reader["size"]),
                            Description = reader["description"].ToString(),
                            Type = reader["type"].ToString(),
                            Url = reader["url"].ToString(),
                        });
                    }
                }

                return dashboards;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AfpSure> GetInComboAfpSure()
        {
            try
            {
                var afpSures = new List<AfpSure>();
                var command = CreateCommand("PA_AFP_SURE_GET_AFP_SURE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        afpSures.Add(new AfpSure
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                        });
                    }
                }

                return afpSures;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ModalityContract> GetInComboModalityContract()
        {
            try
            {
                var modalityContracts = new List<ModalityContract>();
                var command = CreateCommand("PA_MODALITY_IN_COMBO_GET_MODALITY_CONTRACT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        modalityContracts.Add(new ModalityContract
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                        });
                    }
                }

                return modalityContracts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Role> GetInComboRole()
        {
            try
            {
                var roles = new List<Role>();
                var command = CreateCommand("PA_CHARGE_IN_COMBO_GET_ROLE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                            Salary = Convert.ToDecimal(reader["salary_to_pay"].ToString())
                        });
                    }
                }

                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<TypeOfContract> GetInComboTypeOfContract()
        {
            try
            {
                var typeOfContracts = new List<TypeOfContract>();
                var command = CreateCommand("PA_TYPE_CONTRACT_IN_COMBO_GET_TYPE_OF_CONTRACT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typeOfContracts.Add(new TypeOfContract
                        {
                            Value = Convert.ToInt32(reader["value"]),
                            Label = reader["label"].ToString(),
                        });
                    }
                }

                return typeOfContracts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<VoucherDocument> GetInSelectVoucherDocument()
        {
            try
            {
                var vouchers = new List<VoucherDocument>();
                var command = CreateCommand("PA_VO_DOC_GET_VOUCHER_DOCUMENT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vouchers.Add(new VoucherDocument
                        {
                            Value = Convert.ToInt32(reader["id"]),
                            Label = reader["name"].ToString(),
                        });
                    }
                }

                return vouchers;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptions(int employeedId)
        {
            try
            {
                var options = new List<Option>();
                var command = CreateCommand("PA_MENU_GET_OPTION");
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add(new Option
                        {
                            OptionId = Convert.ToInt32(reader["id"]),
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            Icon = reader["icon"].ToString(),
                        });
                    }
                }

                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptionsByCodeEmployeed(string code)
        {
            try
            {
                var options = new List<Option>();
                var command = CreateCommand("PA_MENU_PADRE_GET_OPTION_BY_CODE_EMP");
                command.Parameters.AddWithValue("@v_code_employeed", code);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add(new Option
                        {
                            OptionId = Convert.ToInt32(reader["id"]),
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            Icon = reader["icon"].ToString(),
                        });
                    }
                }

                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Option> GetOptionsGeneral()
        {
            try
            {
                var options = new List<Option>();
                var command = CreateCommand("PA_ME_GET_OPTION");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add(new Option
                        {
                            OptionId = Convert.ToInt32(reader["id"]),
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            Icon = reader["icon"].ToString(),
                            State = Convert.ToBoolean(reader["state"]),
                        });
                    }
                }

                return options;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<OptionItems> GetOptionsItems(int optionId, int employeedId)
        {
            try
            {
                var optionsItems = new List<OptionItems>();
                var command = CreateCommand("PA_MENU_GET_OPTION_ITEMS");
                command.Parameters.AddWithValue("@v_option_id", optionId);
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        optionsItems.Add(new OptionItems
                        {
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            To = reader["to"].ToString(),
                        });
                    }
                }

                return optionsItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<OptionItems> GetOptionsItemsByCodeEmployeed(string code)
        {
            try
            {
                var optionsItems = new List<OptionItems>();
                var command = CreateCommand("PA_MENU_HIJO_GET_OPTION_BY_CODE_EMP");
                command.Parameters.AddWithValue("@v_code_employeed", code);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        optionsItems.Add(new OptionItems
                        {
                            OptionItemId = Convert.ToInt32(reader["id"].ToString()),
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            To = reader["to"].ToString(),
                            State = Convert.ToBoolean(reader["state"].ToString()),
                            Option = new Option()
                            {
                                OptionId = Convert.ToInt32(reader["option_auth_id"].ToString()),
                            }
                        });
                    }
                }

                return optionsItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<OptionItems> GetOptionsItemsGeneral()
        {
            try
            {
                var optionsItems = new List<OptionItems>();
                var command = CreateCommand("PA_ME_GET_OPTION_ITEMS");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        optionsItems.Add(new OptionItems
                        {
                            OptionItemId = Convert.ToInt32(reader["id"].ToString()),
                            Component = reader["component"].ToString(),
                            Name = reader["name"].ToString(),
                            To = reader["to"].ToString(),
                            State = Convert.ToBoolean(reader["state"].ToString()),
                            Option = new Option()
                            {
                                OptionId = Convert.ToInt32(reader["option_id"].ToString()),
                            }
                        });
                    }
                }

                return optionsItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<object> GetReportMensualCategoryTTO()
        {
            try
            {
                var items = new List<object>();
                var command = CreateCommand("PA_GET_REPORT_MENSUAL_CATEGORY_TTO");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new CategoryTTOReport
                        {
                            Enero = Convert.ToInt32(reader["1"]),
                            Febrero = Convert.ToInt32(reader["2"]),
                            Marzo = Convert.ToInt32(reader["3"]),
                            Abril = Convert.ToInt32(reader["4"]),
                            Mayo = Convert.ToInt32(reader["5"]),
                            Junio = Convert.ToInt32(reader["6"]),
                            Julio = Convert.ToInt32(reader["7"]),
                            Agosto = Convert.ToInt32(reader["8"]),
                            Septiembre = Convert.ToInt32(reader["9"]),
                            Octubre = Convert.ToInt32(reader["10"]),
                            Noviembre = Convert.ToInt32(reader["11"]),  
                            Diciembre = Convert.ToInt32(reader["12"]),
                        });
                    }

                }

                return items;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Role> GetRoles()
        {
            try
            {
                var roles = new List<Role>();
                var command = CreateCommand("PA_GET_ROLE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            Value = Convert.ToInt32(reader["id"]),
                            Label = reader["name"].ToString(),
                            Abbreviation = reader["abbreviation"].ToString(),
                            Salary = Convert.ToDecimal(reader["salary_to_pay"].ToString()),
                            StateDescription = reader["state_decription"].ToString(),
                            State = Convert.ToBoolean(reader["state"].ToString()),
                            Area = new Area()
                            {
                                AreaId = Convert.ToInt32(reader["area_id"].ToString()),
                                AreaDescription = reader["area"].ToString(),
                            }
                        });
                    }
                }

                return roles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Routes> GetRoutes(int employeedId)
        {
            try
            {
                var routes = new List<Routes>();
                var command = CreateCommand("PA_ROUTE_GET_ROUTES");
                command.Parameters.AddWithValue("@v_employeed_id", employeedId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        routes.Add(new Routes
                        {
                            RoutesId = Convert.ToInt32(reader["id"]),
                            Path = reader["path"].ToString(),
                            Exact = Convert.ToBoolean(reader["exact"].ToString()),
                            Name = reader["name"].ToString(),
                            Element = reader["element"].ToString(),
                        });
                    }
                }

                return routes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Routes> GetRoutesSpecial(string userCode)
        {
            try
            {
                var routes = new List<Routes>();
                var command = CreateCommand("PA_ROUTES_SPECIAL_GET_ROUTES");
                command.Parameters.AddWithValue("@v_user_access", userCode);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        routes.Add(new Routes
                        {
                            RoutesId = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                        });
                    }
                }

                return routes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostRegisterFrecuencyClinic(Frecuency frecuency)
        {
            try
            {
                var command = CreateCommand("PA_REGISTER_POST_FRECUENCY");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_description", frecuency?.FrecuencyDescription);
                command.Parameters.AddWithValue("@v_abbreviation", frecuency?.Abbreviation);
                command.Parameters.AddWithValue("@_value", frecuency?.Value);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PostRegisterRole(Role role)
        {
            try
            {
                var command = CreateCommand("PA_ROLE_POST");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_name", role?.Name);
                command.Parameters.AddWithValue("@v_abbreviation", role?.Abbreviation);
                command.Parameters.AddWithValue("@v_salary", role?.Salary);
                command.Parameters.AddWithValue("@v_area_id", role?.Area?.AreaId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutAddOptionEmployeed(int optionItemId, int optionId, string code)
        {
            try
            {
                var command = CreateCommand("PA_ADD_MENU_PERMISO_OPTION_PUT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_option_id", optionItemId);
                command.Parameters.AddWithValue("@v_code_trabajador", code);
                command.Parameters.AddWithValue("@v_padre_id", optionId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutAddOptionFather(string codeEmployeed, int optionId)
        {
            try
            {
                var command = CreateCommand("PA_ADD_MENU_AUTH_FATHER_INSERT_PUT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_employeed_code", codeEmployeed);
                command.Parameters.AddWithValue("@v_option_id", optionId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutConfig(Config config)
        {
            try
            {
                var command = CreateCommand("PA_CONFIG_GENERAL_PUT_CONFIG");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_value", config.Value);
                command.Parameters.AddWithValue("@v_key_name", config.Name);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutDisabledEnabledRole(int roleId, int type)
        {
            try
            {
                var command = CreateCommand("PA_PUT_DISABLED_ENABLED_ROLE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_role_id", roleId);
                command.Parameters.AddWithValue("@v_type", type);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutRemoveAddOptionEmployeed(int optionItemId)
        {
            try
            {
                var command = CreateCommand("PA_REMOVE_MENU_PERMISO_OPTION_PUT");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_option_item_id", optionItemId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutRole(Role role)
        {
            try
            {
                var command = CreateCommand("PA_PUT_ROLE");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_name", role?.Name);
                command.Parameters.AddWithValue("@v_abbreviation", role?.Abbreviation);
                command.Parameters.AddWithValue("@v_salary", role?.Salary);
                command.Parameters.AddWithValue("@v_area_id", role?.Area?.AreaId);
                command.Parameters.AddWithValue("@v_role_id", role?.RoleId);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool PutUpdateFrecuencyClinic(Frecuency frecuency)
        {
            try
            {
                var command = CreateCommand("PA_UPDATE_FRECUENCY_PUT_FRECUENCY");
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@v_frecuency_id", frecuency?.FrecuencyId);
                command.Parameters.AddWithValue("@v_description", frecuency?.FrecuencyDescription);
                command.Parameters.AddWithValue("@v_abbreviation", frecuency?.Abbreviation);
                command.Parameters.AddWithValue("@v_value", frecuency?.Value);

                return Convert.ToInt32(command.ExecuteNonQuery()) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<VerifyPatient> VerifyPatientByFullName(string surnames, string names)
        {
            try
            {
                var verifyPatients = new List<VerifyPatient>();
                var command = CreateCommand("PA_VERIFY_IS_PATIENT_ACTIVE_GET_BY_NAMES_PERSON");
                command.Parameters.AddWithValue("@v_surnames", surnames);
                command.Parameters.AddWithValue("@v_names", names);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verifyPatients.Add(new VerifyPatient
                        {
                            NamesFull = reader["nombres_completos"].ToString(),
                            IsExitsPerson = Convert.ToInt32(reader["is_exists_person"].ToString())
                        });
                    }
                }

                return verifyPatients;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
