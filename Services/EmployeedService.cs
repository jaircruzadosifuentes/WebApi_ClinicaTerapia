using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IEmployeedService
    {
        IEnumerable<Employeed> GetAllEmployeed();
        IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId);
        Employeed PostAccessSystem(Employeed employeed);
        bool PostRegisterEmployeed(Employeed employeed);
        IEnumerable<Employeed> GetAllEmployeedPendingAproval();
        bool PutAppproveContractEmployeed(Employeed employeed);
        Employeed GetByUserNameEmployeed(string userName);
    }
    public class EmployeedService : IEmployeedService
    {
        private IUnitOfWork _unitOfWork;

        public EmployeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Employeed> GetAllEmployeed()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetAllEmployeed();
        }

        public IEnumerable<Employeed> GetAllEmployeedPendingAproval()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetAllEmployeedPendingAproval();
        }

        public Employeed GetByUserNameEmployeed(string userName)
        {
            using var context = _unitOfWork.Create();
            var employeedReturn = context.Repositories.EmployeedRepository.GetByUserNameEmployeed(userName);
            return employeedReturn;
        }

        public IEnumerable<EmployeedDisponibilty> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId)
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.EmployeedRepository.GetSchedulesDisponibility(dateToConsult, employeedId);
        }
        public Employeed PostAccessSystem(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            var employeedReturn = context.Repositories.EmployeedRepository.PostAccessSystem(employeed);
            return employeedReturn;
        }

        public bool PostRegisterEmployeed(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.EmployeedRepository.PostRegisterEmployeed(employeed);
            context.SaveChanges();
            return insert;
        }

        public bool PutAppproveContractEmployeed(Employeed employeed)
        {
            using var context = _unitOfWork.Create();
            bool update = context.Repositories.EmployeedRepository.PutAppproveContractEmployeed(employeed);
            context.SaveChanges();
            return update;
        }
    }
}
