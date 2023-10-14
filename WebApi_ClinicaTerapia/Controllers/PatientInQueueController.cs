using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientInQueueController : ControllerBase
    {
        private readonly IPatientQueueService _patientQueueService;

        public PatientInQueueController(IPatientQueueService patientQueueService)
        {
            _patientQueueService = patientQueueService;
        }

        [HttpGet(Name = "GetPatientsInQueue")]
        public ActionResult<IEnumerable<PatientInQueue>> GetAllPatientsInQueues()
        {
            var listPatientsInQueues = _patientQueueService.GetAllPatientsInQueues();
            return Ok(listPatientsInQueues);
        }
    }
}
