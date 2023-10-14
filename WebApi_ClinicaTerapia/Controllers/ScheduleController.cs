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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }
        [HttpPost("PostGenerateSchedule")]
        public void PostGenerateSchedule(PayDuesDetail payDuesDetail)
        {
            _scheduleService.GenerateSchedule(payDuesDetail);
        }
        [HttpGet("GetAllSchedulePatient/{patientId}")]
        public ActionResult<IEnumerable<PayDuesDetail>> GetAllSchedulePatient(int patientId)
        {
            var payDuesDetails = _scheduleService.GetAllSchedulePatient(patientId);
            return Ok(payDuesDetails);
        }
    }
}
