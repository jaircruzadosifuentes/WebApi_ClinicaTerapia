using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IScheduleService
    {
        bool GenerateSchedule(PayDuesDetail payDuesDetail);
        IEnumerable<PayDuesDetail> GetAllSchedulePatient(int patientId);
    }
    public class ScheduleService : IScheduleService
    {
        private IUnitOfWork _unitOfWork;

        public ScheduleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool GenerateSchedule(PayDuesDetail payDuesDetail)
        {
            using var context = _unitOfWork.Create();
            bool generate = context.Repositories.ScheduleRepository.GenerateSchedule(payDuesDetail);
            context.SaveChanges();
            return generate;
        }

        public IEnumerable<PayDuesDetail> GetAllSchedulePatient(int patientId)
        {
            using var context = _unitOfWork.Create();
            var schedules = context.Repositories.ScheduleRepository.GetAllSchedulePatient(patientId);
            return schedules;
        }
    }
}
