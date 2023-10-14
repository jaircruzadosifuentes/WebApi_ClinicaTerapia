using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPacketsOrUnitSessionsRepository
    {
        IEnumerable<PacketsOrUnitSessions> GetAllPacketsOrUnitSessions();
        bool PostRegisterPacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions);
        bool PutUpdatePacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions);
    }
}
