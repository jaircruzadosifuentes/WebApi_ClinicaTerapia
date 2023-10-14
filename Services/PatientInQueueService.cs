using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPatientQueueService
    {
        IEnumerable<PatientInQueue> GetAllPatientsInQueues();
    }
    public class PatientInQueueService: IPatientQueueService
    {
        private IUnitOfWork _unitOfWork;

        public PatientInQueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PatientInQueue> GetAllPatientsInQueues()
        {
            using var context = _unitOfWork.Create();
            var patientInQueues = context.Repositories.PatientInQueueRepository.GetAllPatientInQueues();
            return patientInQueues;
        }
    }
}
