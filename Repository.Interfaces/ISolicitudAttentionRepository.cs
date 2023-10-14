using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ISolicitudAttentionRepository
    {
        bool InsertNewSolicitudAttention(Patient patient);
        bool InsertFirstClinicalAnalysis(Patient patient);
        IEnumerable<Patient> GetPacientesConPrimeraAtencionClinica();
        IEnumerable<Patient> GetPatientsSolicitudeInDraft();
    }
}
