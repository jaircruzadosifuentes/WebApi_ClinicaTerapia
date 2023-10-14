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
    public class PayController : ControllerBase
    {
        private readonly IPayService _payService;

        public PayController(IPayService payService)
        {
            _payService = payService;
        }
        [HttpPost("PostInsertPaySolicitud")]
        public void PostInsertPaySolicitud(Pay pay)
        {
            _payService.PostInsertPaySolicitud(pay);
        }
        [HttpGet("GetPayDueDetailForPatientId/{patientId}")]
        public ActionResult<IEnumerable<PayDuesDetail>> GetPayDueDetailForPatientId(int patientId)
        {
            var payDuesDetails = _payService.GetPayDueDetailForPatientId(patientId);
            return Ok(payDuesDetails);
        }
    }
}
