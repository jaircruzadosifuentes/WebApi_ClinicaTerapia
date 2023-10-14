using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet("{hourInitial}/{hourFinished}/{fechaReserved}/{employeedId}")]
        public ActionResult<IEnumerable<PatientValidateSchedule>> GetAllPatientsWithSchedule(string hourInitial, string hourFinished, DateTime fechaReserved, int employeedId   )
        {
            var employeeds = _patientService.GetAllPatientsWithSchedule(hourInitial, hourFinished, fechaReserved, employeedId);
            return Ok(employeeds);
        }
        [HttpGet("GetAllPatientsPendApro")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> GetAllPatientsPendApro()
        {
            var employeeds = _patientService.GetAllPatientsPendApro();
            return Ok(employeeds);
        }  
        [HttpGet("GetAllPatientsPatientWithAppoiment")]
        public ActionResult<IEnumerable<Patient>> GetAllPatientsPatientWithAppoiment()
        {
            var employeeds = _patientService.GetAllPatientsPatientWithAppoiment();
            return Ok(employeeds);
        } 
        [HttpGet("GetAllPatientsNewAttentionByEmployeedId/{employeedId}")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> GetAllPatientsNewAttentionByEmployeedId(int employeedId)
        {
            var employeeds = _patientService.GetAllPatientsNewAttentionByEmployeedId(employeedId);
            return Ok(employeeds);
        } 
        [HttpGet("GetAllPatientsInTreatment")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> GetAllPatientsInTreatment()
        {
            var patients = _patientService.GetAllPatientsInTreatment();
            return Ok(patients);
        }  
        [HttpGet("GetAllPatientsFinishedTreatment")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> GetAllPatientsFinishedTreatment()
        {
            var patients = _patientService.GetAllPatientsFinishedTreatment();
            return Ok(patients);
        }  
        [HttpGet("GetAllPatientsInWaiting")]
        public ActionResult<IEnumerable<Patient>> GetAllPatientsInWaiting()
        {
            var patients = _patientService.GetAllPatientsInWaiting();
            return Ok(patients);
        } 
        [HttpGet("GetAllPatientsInAttention")]
        public ActionResult<IEnumerable<Patient>> GetAllPatientsInAttention()
        {
            var patients = _patientService.GetAllPatientsInAttention();
            return Ok(patients);
        } 
        [HttpGet("GetAllPatientsInPercentajeTreatment/{patientId}")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> GetAllPatientsInPercentajeTreatment(int patientId)
        {
            var patients = _patientService.GetAllPatientsInPercentajeTreatment(patientId);
            return Ok(patients);
        }
        [HttpGet("GetAdvanceCliniciForPatientId/{patientId}")]
        public ActionResult<IEnumerable<PatientProgress>> GetAdvanceCliniciForPatientId(int patientId)
        {
            var patients = _patientService.GetAdvanceCliniciForPatientId(patientId);
            return Ok(patients);
        } 
        [HttpGet("GetByIdPatientProgress/{patientId}")]
        public ActionResult<IEnumerable<PatientProgress>> GetByIdPatientProgress(int patientId)
        {
            var patients = _patientService.GetByIdPatientProgress(patientId);
            return Ok(patients);
        }
        [HttpPut("PutApprovePatientNew/{patientId}/{type}")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> PutApprovePatientNew(int patientId, string type)
        {
            var approve = _patientService.ApprovePatientNew(patientId, type);
            return Ok(approve);
        }
        [HttpPut("PutApprovePatient/{patientId}/{type}")]
        public ActionResult<IEnumerable<PatientInQueueGeneral>> PutApprovePatient(int patientId, string type)
        {
            var approve = _patientService.PutApproveSolicitude(patientId, type);
            return Ok(approve);
        }
        [HttpPost("PostRegistrProgressSesion")]
        public void PostRegistrProgressSesion(PatientProgress patientProgress)
        {
            _patientService.PostRegistrProgressSesion(patientProgress);
        } 
        [HttpPut("PutUpdateHourSesion")]
        public void PutUpdateHourSesion(PatientProgress patientProgress)
        {
            _patientService.PutUpdateHourSesion(patientProgress);
        }
    }
}
