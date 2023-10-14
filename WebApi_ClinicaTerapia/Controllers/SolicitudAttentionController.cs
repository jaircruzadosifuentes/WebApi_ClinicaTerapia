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
    public class SolicitudAttentionController : ControllerBase
    {
        private readonly ISolicitudAttentionService _solicitudAttentionService;

        public SolicitudAttentionController(ISolicitudAttentionService solicitudAttentionService)
        {
            _solicitudAttentionService = solicitudAttentionService;
        }
        [HttpPost("PostNewSolicitudAttention")]
        public void PostNewSolicitudAttention(Patient patient)
        {
            _solicitudAttentionService.InsertNewSolicitudAttention(patient);
        }
        [HttpPost("PostInsertFirstClinicalAnalysis")]
        public void PostInsertFirstClinicalAnalysis(Patient patient)
        {
            _solicitudAttentionService.InsertFirstClinicalAnalysis(patient);
        } 
        [HttpGet("GetPacientesConPrimeraAtencionClinica")]
        public ActionResult<IEnumerable<Patient>> GetPacientesConPrimeraAtencionClinica()
        {
            var result = _solicitudAttentionService.GetPacientesConPrimeraAtencionClinica();
            return Ok(result);
        } 
        [HttpGet("GetPatientsSolicitudeInDraft")]
        public ActionResult<IEnumerable<Patient>> GetPatientsSolicitudeInDraft()
        {
            var result = _solicitudAttentionService.GetPatientsSolicitudeInDraft();
            return Ok(result);
        }
    }
}
