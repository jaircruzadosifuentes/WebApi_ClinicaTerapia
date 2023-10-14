using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces;

namespace Services
{
    public interface ISolicitudAttentionService
    {
        bool InsertNewSolicitudAttention(Patient patient);
        bool InsertFirstClinicalAnalysis(Patient patient);
        IEnumerable<Patient> GetPacientesConPrimeraAtencionClinica();
        IEnumerable<Patient> GetPatientsSolicitudeInDraft();
    }
    public class SolicitudAttentionService : ISolicitudAttentionService
    {
        private IUnitOfWork _unitOfWork;

        public SolicitudAttentionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Patient> GetPacientesConPrimeraAtencionClinica()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.SolicitudAttentionRepository.GetPacientesConPrimeraAtencionClinica();
        }

        public IEnumerable<Patient> GetPatientsSolicitudeInDraft()
        {
            using var context = _unitOfWork.Create();
            return context.Repositories.SolicitudAttentionRepository.GetPatientsSolicitudeInDraft();
        }

        public bool InsertFirstClinicalAnalysis(Patient patient)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.SolicitudAttentionRepository.InsertFirstClinicalAnalysis(patient);
            context.SaveChanges();
            return insert;
        }

        public bool InsertNewSolicitudAttention(Patient patient)
        {
            using var context = _unitOfWork.Create();
            bool insert = context.Repositories.SolicitudAttentionRepository.InsertNewSolicitudAttention(patient);
            context.SaveChanges();
            return insert;
        }
    }
}
