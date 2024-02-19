using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPatientRepository
    {
        IEnumerable<PatientValidateSchedule> GetAllPatientsWithSchedule(string hourInitial, string hourFinished, DateTime fechaReserved, int employeedId);
        IEnumerable<Patient> GetAllPatientsPendApro();
        bool ApprovePatientNew(int patientId, string type);
        bool PutApproveSolicitude(int patientId, string type);
        IEnumerable<Patient> GetAllPatientsNewAttentionByEmployeedId(int employeedId);
        IEnumerable<PatientProgress> GetSessionForPatientId(int patientId, bool finishedTreatment);
        IEnumerable<Patient> GetAllPatientsPatientWithAppoiment();
        IEnumerable<Patient> GetAllPatientsInTreatment();
        IEnumerable<Patient> GetAllPatientsFinishedTreatment();
        IEnumerable<Patient> GetAllPatientsInPercentajeTreatment(int patientId);
        IEnumerable<PatientProgress> GetAdvanceCliniciForPatientId(int patientId);
        IEnumerable<PatientProgress> GetByIdPatientProgress(int progressPatientId);
        bool PostRegistrProgressSesion(PatientProgress patientProgress);
        bool PutUpdateHourSesion(PatientProgress patientProgress);
        IEnumerable<Patient> GetAllPatientsInWaiting();
        IEnumerable<Patient> GetAllPatientsInAttention();
        PatientProgress GetItemSesionDetailById(int patientDetailSesionId);
        IEnumerable<ClinicalHistory> GetHistoryForPatientId(int id, bool mostrarTodos);
        bool PutUpdateHistorClinicDocument(int clinicHistoryId, string nameFile);
    }
}
