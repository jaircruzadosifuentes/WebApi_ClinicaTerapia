using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.EmailRepository
{
    public class EmailRepository: Repository, IEmailRepository
    {
        public EmailRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<Email> GetAllByEmailForPersonId(int personId)
        {
            try
            {
                var emails = new List<Email>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETBYID_EMAIL_FOR_PERSON);
                command.Parameters.AddWithValue("@v_person_id", personId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emails.Add(new Email
                        {
                            EmailId = Convert.ToInt32(reader["emailId"]),
                            EmailDescription = reader["email"].ToString()
                        });
                    }
                }

                return emails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
