using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string? Names { get; set; }
        public string? Surnames { get; set; }
        public DateTime BirthDate { get; set; }
        public int? Age { get; set; }
        public List<Email> Emails { get; set; }
        public string? Address { get; set; }
        public string? CivilStatus { get; set; }
        public string? Gender { get; set; }
        public string? ProfilePicture { get; set; }
        public PersonDocument? PersonDocument { get; set; }
        public Email? PersonEmail { get; set; }
        public CellPhone? PersonCellphone { get; set; }
        public List<PersonDocument> PersonDocuments { get; set; }
        public List<CellPhone> CellPhones { get; set; }

        public Person()
        {
            Emails = new List<Email>();
            PersonDocuments = new List<PersonDocument>();
            CellPhones = new List<CellPhone>();
        }
    }
}
