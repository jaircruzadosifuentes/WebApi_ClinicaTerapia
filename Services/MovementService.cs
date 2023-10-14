using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IMovementService
    {
        IEnumerable<SaleBuyOut> GetAllMovementsSaleBuyOut();
    }
    public class MovementService : IMovementService
    {
        private IUnitOfWork _unitOfWork;

        public MovementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SaleBuyOut> GetAllMovementsSaleBuyOut()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.MovementsRepository.GetAllMovementsSaleBuyOut();
        }
    }
}
