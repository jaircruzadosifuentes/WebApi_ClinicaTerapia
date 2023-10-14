using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IScheduleRepository
    {
        bool GenerateSchedule(PayDuesDetail payDuesDetail);
        IEnumerable<PayDuesDetail> GetAllSchedulePatient(int patientId);

    }
}
