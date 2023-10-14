using Common;
using Entities;
using Newtonsoft.Json;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Repository.SqlServer.PersonRepository
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }

        public IEnumerable<Person> GetAll()
        {
            try
            {
                var persons = new List<Person>();
                string json = string.Empty;
                var command = CreateCommand(StoreProcedure.G_STORE_GETALL_PERSON);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        persons.Add(new Person
                        {
                            PersonId = Convert.ToInt32(reader["personId"]),
                            Names = reader["names"].ToString(),
                            Surnames = reader["surnames"].ToString(),
                            BirthDate = Convert.ToDateTime(reader["birth_date"]),
                            Age = Convert.ToInt32(reader["age"]),
                            CivilStatus = reader["civil_status"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Address = reader["address"].ToString()
                        });
                    }
                }

                return persons;
            } 
            catch (SqlException sql)
            {
                throw new Exception(sql.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Person> GetAllByPersonId(int personId)
        {
            throw new NotImplementedException();
        }

    }
}
