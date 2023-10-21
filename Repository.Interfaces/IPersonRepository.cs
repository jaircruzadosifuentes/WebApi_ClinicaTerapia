using Entities;
using Repository.Interfaces.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPersonRepository 
    {
        IEnumerable<Person> GetAllByPersonId(int personId);
        IEnumerable<Person> GetAll();
        Person GetPersonByNroDocument(string nroDocument);
    }
}
