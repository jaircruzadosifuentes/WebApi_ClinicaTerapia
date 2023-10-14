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
    public class PacketsOrUnitSessionsController : ControllerBase
    {
        private readonly IPacketsOrUnitSessionsService _packetsOrUnitSessionsService;

        public PacketsOrUnitSessionsController(IPacketsOrUnitSessionsService packetsOrUnitSessionsService)
        {
            _packetsOrUnitSessionsService = packetsOrUnitSessionsService;
        }
        [HttpGet("GetAllPacketsOrUnitSession")]
        public ActionResult<IEnumerable<PatientValidateSchedule>> GetAllPacketsOrUnitSession()
        {
            var packetsOrUnitSessions = _packetsOrUnitSessionsService.GetAllPacketsOrUnitSessions();
            return Ok(packetsOrUnitSessions);
        }
        [HttpPost("PostRegisterPacketsOrUnitSessions")]
        public void PostRegisterPacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            _packetsOrUnitSessionsService.PostRegisterPacketsOrUnitSessions(packetsOrUnitSessions);
        }
        [HttpPut("PutUpdatePacketsOrUnitSessions")]
        public void PutUpdatePacketsOrUnitSessions(PacketsOrUnitSessions packetsOrUnitSessions)
        {
            _packetsOrUnitSessionsService.PutUpdatePacketsOrUnitSessions(packetsOrUnitSessions);
        }
    }
}
