using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInAttentionController : ControllerBase
    {
        private readonly IPatientAttentionService _patientAttentionService;

        public PatientInAttentionController(IPatientAttentionService patientAttentionService)
        {
            _patientAttentionService = patientAttentionService;
        }

        [HttpGet(Name = "GetPatientsInAttention")]
        public ActionResult<IEnumerable<PatientInAttention>> GetAllPatientsInAttention()
        {
            var listPatientsInQueues = _patientAttentionService.GetAllPatientsInAttention();
            return Ok(listPatientsInQueues);
        }
    }
}
