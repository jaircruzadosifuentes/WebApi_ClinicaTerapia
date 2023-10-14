using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public decimal MountSalary { get; set; }
        public bool State { get; set; }
        public string? UserRegister { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Employeed? Employeed { get; set; }
    }
}
