using Common;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPersonService
    {
        IEnumerable<Person> GetAll();
        Person GetPersonByNroDocument(string nroDocument);
    }

    public class PersonService : IPersonService
    {
        private IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Person> GetAll()
        {
            using var context = _unitOfWork.Create();
            try
            {
                var peoples = context.Repositories.PersonRepository.GetAll();
                foreach (var person in peoples)
                {
                    person.Emails = (List<Email>)context.Repositories.EmailRepository.GetAllByEmailForPersonId(person.PersonId);
                    person.PersonDocuments = (List<PersonDocument>)context.Repositories.DocumentRepository.GetAllByDocumentForPersonId(person.PersonId);
                    person.CellPhones = (List<CellPhone>)context.Repositories.CellPhoneRepository.GetAllByCellPhoneForPersonId(person.PersonId);
                }
                return peoples;
            }
            catch (Exception ex)
            {
                Error error = new()
                {
                    Description = ex.Message, DescriptionTrace = ex.StackTrace, CodeUser = "SIST", TypeError = (int)EnumTypeError.LAYER_SERVICE,
                    CreatedAt = DateTime.Now
                };

                int codeError = context.Repositories.ErrorRepository.InsertErrorRepository(error);
                throw new Exception(string.Concat(Constantes.G_MESSAGE_ERROR_WITH_CODE, codeError.ToString()));
            }
        }

        public Person GetPersonByNroDocument(string nroDocument)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.PersonRepository.GetPersonByNroDocument(nroDocument);
        }
    }
}
