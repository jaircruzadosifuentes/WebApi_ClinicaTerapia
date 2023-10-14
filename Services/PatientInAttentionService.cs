using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface IPatientAttentionService
    {
        IEnumerable<PatientInAttention> GetAllPatientsInAttention();
    }
    public class PatientInAttentionService : IPatientAttentionService
    {
        private IUnitOfWork _unitOfWork;

        public PatientInAttentionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<PatientInAttention> GetAllPatientsInAttention()
        {
            using var context = _unitOfWork.Create();
            var patientInAttention = context.Repositories.PatientInAttentionRepository.GetAllPatientInAttention();
            return patientInAttention;
        }
    }
}
