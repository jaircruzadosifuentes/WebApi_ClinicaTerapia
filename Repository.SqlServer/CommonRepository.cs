using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Entities;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.StyledXmlParser.Jsoup.Nodes;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using PdfSharp.Charting;
using Repository.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Document = iTextSharp.text.Document;
using Paragraph = iTextSharp.text.Paragraph;
using iTextSharp.text.pdf;
using Element = iTextSharp.text.Element;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using Microsoft.AspNetCore.Http.Internal;
using PdfReader = iTextSharp.text.pdf.PdfReader;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace Repository.SqlServer
{

    public class CommonRepository : Repository, ICommonRepository
    {
        public CommonRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public Task<APIGatewayProxyResponse> ConvertHtmlToPdf(string bucketName, string fileName)
        {
            try
            {
                string htmlContent = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n\t<title>Reporte General</title>\r\n</head>\r\n<body>\r\n\t<h1>Título principal</h1>\r\n\t<p>Este es un párrafo de ejemplo en HTML5. Aquí podemos escribir información relevante para nuestro sitio web.</p>\r\n\t<blockquote>\r\n\t\t<p>“La creatividad es la inteligencia divirtiéndose.”</p>\r\n\t\t<footer>Albert Einstein</footer>\r\n\t</blockquote>\r\n\t<p>En este otro párrafo podemos seguir añadiendo información para nuestro sitio. También podemos utilizar las etiquetas de enfatizado, como <strong>negrita</strong>, <em>cursiva</em>, <u>subrayado</u>, <s>tachado</s>, y <mark>resaltado</mark>.</p>\r\n</body>\r\n</html>"; // Tu plantilla HTML

                byte[] pdfBytes;

                using (var memoryStream = new MemoryStream())
                {
                    ConverterProperties properties = new();
                    HtmlConverter.ConvertToPdf(htmlContent, memoryStream, properties);

                    pdfBytes = memoryStream.ToArray();
                }
                using (var client = new AmazonS3Client())
                {
                    client.PutObjectAsync(new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileName,
                        InputStream = new MemoryStream(pdfBytes)
                    }).Wait();
                }
                return Task.FromResult(new APIGatewayProxyResponse
                {
                    StatusCode = 200,
                    Body = Convert.ToBase64String(pdfBytes),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/pdf" } },
                    IsBase64Encoded = true
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new APIGatewayProxyResponse
                {
                    StatusCode = 500,
                    Body = $"Error al generar el PDF: {ex.Message}"
                });
            }
        }
        public string GetUrlImageFromS3(string profilePicture, string carpeta, string bucketName)
        {
            var awsKeyId = ConfigurationManager.AppSettings["AWSAccessKey"]; 
            var awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            var awsServiceUrl = ConfigurationManager.AppSettings["AWSServiceUrl"];
            var awsRegionId = ConfigurationManager.AppSettings["AWSRegionId"];
            var awsBucket = bucketName;

            AWSCredentials credentials = new BasicAWSCredentials(awsKeyId, awsSecretKey);
            AmazonS3Config config = new()
            {
                ServiceURL = awsServiceUrl,
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsRegionId)
            };
            var s3ClientGetUrl = new AmazonS3Client(credentials, config);

            string keyPath = getKeyPath(profilePicture, carpeta);

            GetPreSignedUrlRequest request1 = new()
            {
                BucketName = awsBucket,
                Key = keyPath,
                Expires = DateTime.Now.AddMinutes(60)
            };
            return s3ClientGetUrl.GetPreSignedURL(request1);
        }
        public string getKeyPath(string profilePicture, string carpeta)
        {
            return carpeta + "/" + profilePicture;
        }
        public IEnumerable<Config> GetAllConfigs()
        {
            try
            {
                var configs = new List<Config>();
                var command = CreateCommand("PA_CONFIG_GENERAL_GET_CONFIG");
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        configs.Add(new Config
                        {
                            ConfigId = Convert.ToInt32(reader["id"].ToString()),
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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

        public IEnumerable<CategoryEntity> GetCategoriesInSelect()
        {
            try
            {
                var categories = new List<CategoryEntity>();
                var command = CreateCommand("PA_CATEGORY_IN_SELECT_GET_CATEGORY");
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryEntity
                        {
                            Value = Convert.ToInt32(reader["id"].ToString()),
                            Label = reader["name"].ToString(),
                        });
                    }
                }

                return categories;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;

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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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

        public IEnumerable<SubCategory> GetSubCategoriesInSelect(int categoryId)
        {
            try
            {
                var subCategories = new List<SubCategory>();
                var command = CreateCommand("PA_SUB_CATEGORY_IN_SELECT_GET_SUB_CATEGORY");
                command.Parameters.AddWithValue("@v_category_id", categoryId);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subCategories.Add(new SubCategory
                        {
                            Value = Convert.ToInt32(reader["id"].ToString()),
                            Label = reader["name"].ToString(),
                        });
                    }
                }

                return subCategories;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
                command.CommandType = CommandType.StoredProcedure;
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
        public async Task<bool> UploadFileS3Async(IFormFile file, string nameBucket, string name)
        {

            var awsKeyId = ConfigurationManager.AppSettings["AWSAccessKey"];
            var awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            var awsServiceUrl = ConfigurationManager.AppSettings["AWSServiceUrl"];
            var awsRegionId = ConfigurationManager.AppSettings["AWSRegionId"];
            try
            {
                var credentials = new BasicAWSCredentials(awsKeyId, awsSecretKey);
                var config = new AmazonS3Config
                {
                    ServiceURL = awsServiceUrl,
                    RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsRegionId)
                };

                using var s3Client = new AmazonS3Client(credentials, config);

                var transferUtility = new TransferUtility(s3Client);
                var transferRequest = new TransferUtilityUploadRequest
                {
                    InputStream = file.OpenReadStream(),
                    AutoCloseStream = true,
                    BucketName = nameBucket,
                    Key = $"perfil/{name}",
                    StorageClass = S3StorageClass.Standard
                };

                transferRequest.Metadata.Add("Date-UTC-Uploaded", DateTime.UtcNow.ToString());
                await transferUtility.UploadAsync(transferRequest);

                return true;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error al subir archivo a Amazon S3: " + e.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }

        public string EncryptFileNameYId(string originalFileName, int id)
        {
            string combinedString = originalFileName + id.ToString();
            using SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));
            StringBuilder stringBuilder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }
        public static IFormFile ConvertirAMyIFormFile(MemoryStream memoryStream, string nombreArchivo)
        {
            memoryStream.Position = 0;
            Stream stream = memoryStream as Stream;
            IFormFile formFile = new FormFile(stream, 0, stream.Length, null, nombreArchivo);
            return formFile;
        }
        public async Task<bool> GenerarClinicHistoryPDFAsync(ClinicalHistory clinicalHistory, string keyName)
        {
            try
            {
                string ruta = AppContext.BaseDirectory + "spooler\\" + "PLANTILLA_HISTORIA_CLINICA_V1.docx";
                byte[] fileBytes = File.ReadAllBytes(ruta);
                //Keywords and values
                string? keyWordNamePatient = "{nombres}";
                string? valueNamePatient = $"{clinicalHistory?.Patient?.Person?.Surnames}, {clinicalHistory?.Patient?.Person?.Names}";
                string? keyWordSexo = "{sexo}";
                string? valueSexoPatient = $"{clinicalHistory?.Patient?.Person?.Gender}";
                string? keyWordDomicilio = "{domicilio}";
                string? valueDomicilio = $"{clinicalHistory?.Patient?.Person?.Address}";
                string? keyWordEdad = "{edad}";
                string? valueEdad = $"{clinicalHistory?.Patient?.Person?.Age} años";
                string? keyWordTelef = "{celu}";
                string? valueTelef = $"{clinicalHistory?.Patient?.Person?.PersonCellphone?.CellPhoneNumber}";
                string? keyWordECivil = "{ecivil}";
                string? valueECivil = $"{clinicalHistory?.Patient?.Person?.CivilStatus}";
                string? keyWordOcupacion = "{ocupacion}";
                string? valueOcupacion = "-";
                string? keyWordTerapeu = "{terapeuta}";
                string? valueTerapeu = $"{clinicalHistory?.Terapeuta}";
                string? keyWordFecha = "{fecha}";
                string? valueFecha = $"{clinicalHistory?.CreatedAt}";
                string? keyWordNroExpe = "{nroExpediente}";
                string? valueNroExpe = $"{clinicalHistory?.NroClinicHistory}";
                string? keyWordPeso = "{peso}";
                string? valuePeso = $"{clinicalHistory?.Weight} Kg";
                string? keyWordTalla = "{talla}";
                string? valueTalla = $"{clinicalHistory?.HeightOfPerson} cm";
                string? keyWordImc = "{imc}";
                string? valueImc = $"{Convert.ToString(Math.Round((decimal)(clinicalHistory?.Imc), 2))}";

                using MemoryStream memoryStream = new MemoryStream(fileBytes);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordNamePatient, valueNamePatient);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordSexo, valueSexoPatient);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordDomicilio, valueDomicilio);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordEdad, valueEdad);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordTelef, valueTelef);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordECivil, valueECivil);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordOcupacion, valueOcupacion);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordTerapeu, valueTerapeu);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordFecha, valueFecha);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordNroExpe, valueNroExpe);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordPeso, valuePeso);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordTalla, valueTalla);
                ReemplazarPalabrasClaveEnDocumento(memoryStream, keyWordImc, valueImc);

                memoryStream.Position = 0;
                return await SubirArchivoAMemoriaAWS(memoryStream.ToArray(), clinicalHistory, keyName);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine("Error de Amazon S3: " + ex.Message);
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Otro error: " + ex.Message);
                throw;
            }
        }
        private void ReemplazarPalabrasClaveEnDocumento(MemoryStream memoryStream, string keywordToReplace, string replacementText)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
            {
                Body body = doc.MainDocumentPart.Document.Body;

                var textsToReplace = body!.Descendants<Text>()
                    .Where(t => t.Text.Contains(keywordToReplace))
                    .ToList();

                foreach (var textToReplace in textsToReplace)
                {
                    textToReplace.Text = textToReplace.Text.Replace(keywordToReplace, replacementText);
                }
                //doc.Save();
            }
        }
        private static Task<bool> SubirArchivoAMemoriaAWS(byte[] archivo, ClinicalHistory clinicalHistory, string keyName)
        {
            var awsKeyId = ConfigurationManager.AppSettings["AWSAccessKey"];
            var awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            var awsServiceUrl = ConfigurationManager.AppSettings["AWSServiceUrl"];
            var awsRegionId = ConfigurationManager.AppSettings["AWSRegionId"];
            string? bucketName = clinicalHistory?.BucketName; // Reemplazar con el nombre de tu bucket en AWS S3
            var credentials = new BasicAWSCredentials(awsKeyId, awsSecretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = awsServiceUrl,
                RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsRegionId)
            };
            IAmazonS3 s3Client = new AmazonS3Client(credentials, config); // Cambiar la región según corresponda
            TransferUtility fileTransferUtility = new TransferUtility(s3Client);

            using (MemoryStream stream = new MemoryStream(archivo))
            {
                fileTransferUtility.Upload(stream, bucketName, $"{clinicalHistory?.BucketFileName}/{keyName}");
            }
            return Task.FromResult(true);
        }

        public IEnumerable<Pathologies> GetPathologies()
        {
            try
            {
                var pathologies = new List<Pathologies>();
                var command = CreateCommand("PA_PATHOLOGIES_GET_ALL");
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pathologies.Add(new Pathologies
                        {
                            PathologiesId = Convert.ToInt32(reader["id"].ToString()),
                            Description = reader["description"].ToString(),
                        });
                    }
                }

                return pathologies;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
