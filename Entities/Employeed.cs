using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employeed
    {
        public int EmployeedId { get; set; }
        public Person? Person { get; set; }
        public Role? Role { get; set; }
        public string? Label { get; set; }
        public string? State { get; set; }
        public string? StateAbbreviation { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public DateTime? AdmisionDate { get; set; }
        public Salary? Salary { get; set; }
        public decimal? VacationDays { get; set; }
        public Experience? Experience { get; set; }
        public AfpSure? AfpSure { get; set; }
        public string? AssociateCode { get; set; }
        public DateTime? AfpLinkDate { get; set; }
        public TypeOfContract? TypeOfContract { get; set; }
        public ModalityContract? ModalityContract { get; set; }
        public string? UserName { get; set; }
        public bool? IsStaff { get; set; }
        public string? TypeUser { get; set; }
        public Campus? Campus { get; set; }
    }
}
