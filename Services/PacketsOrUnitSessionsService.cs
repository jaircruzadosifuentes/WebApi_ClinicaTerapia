using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPacketsOrUnitSessionsService
    {
        IEnumerable<PacketsOrUnitSessions> GetAllPacketsOrUnitSessions();
        bool PostRegisterPacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions);
        bool PutUpdatePacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions);
    }
    public class PacketsOrUnitSessionsService: IPacketsOrUnitSessionsService
    {
        private IUnitOfWork _unitOfWork;

        public PacketsOrUnitSessionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PacketsOrUnitSessions> GetAllPacketsOrUnitSessions()
        {
            using var context = _unitOfWork.Create();
            var packetsOrUnitSessions = context.Repositories.PacketsOrUnitSessionsRepository.GetAllPacketsOrUnitSessions();
            return packetsOrUnitSessions;
        }

        public bool PostRegisterPacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.PacketsOrUnitSessionsRepository.PostRegisterPacketsOrUnitSessions(packetsOrUnitSessions);
            context.SaveChanges();
            return inserta;
        }

        public bool PutUpdatePacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            using var context = _unitOfWork.Create();
            var inserta = context.Repositories.PacketsOrUnitSessionsRepository.PutUpdatePacketsOrUnitSessions(packetsOrUnitSessions);
            context.SaveChanges();
            return inserta; 
        }
    }
}
