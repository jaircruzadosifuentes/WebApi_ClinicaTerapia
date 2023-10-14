using Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SqlServer.DocumentRepository
{
    public class DocumentRepository : Repository, IDocumentRepository
    {
        public DocumentRepository(SqlConnection context, SqlTransaction transaction)
        {
            _context = context;
            _transaction = transaction;
        }
        public IEnumerable<PersonDocument> GetAllByDocumentForPersonId(int personId)
        {
            try
            {
                var documents = new List<PersonDocument>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETBYID_DOCUMENT_FOR_PERSON);
                command.Parameters.AddWithValue("@v_person_id", personId);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        documents.Add(new PersonDocument
                        {
                            PersonDocumentId = Convert.ToInt32(reader["documentId"]),
                            NroDocument = reader["nroDocumento"].ToString()
                        });
                    }
                }

                return documents;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Document> GetAllDocuments()
        {
            try
            {
                var documents = new List<Document>();
                var command = CreateCommand(StoreProcedure.G_STORE_GETALL_DOCUMENT);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        documents.Add(new Document
                        {
                            value = Convert.ToInt32(reader["value"]),
                            label = reader["label"].ToString(),
                            Longitud = Convert.ToInt32(reader["size"])
                        });
                    }
                }

                return documents;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
