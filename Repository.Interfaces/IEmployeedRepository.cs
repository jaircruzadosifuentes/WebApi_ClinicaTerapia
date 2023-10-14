using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEmployeedRepository
    {
        IEnumerable<Employeed> GetAllEmployeed();
        IEnumerable<Employeed> GetAllEmployeedPendingAproval();
        IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId);
        Employeed PostAccessSystem(Employeed employeed);
        Employeed GetByUserNameEmployeed(string userName);
        bool PostRegisterEmployeed(Employeed employeed);
        bool PutAppproveContractEmployeed(Employeed employeed);

    }
}
