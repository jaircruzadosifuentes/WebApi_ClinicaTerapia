using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMovementService _movementService;

        public MovementsController(IMovementService movementService)
        {
            _movementService = movementService;
        }
        [HttpGet("GetAllMovementsSaleBuyOut")]
        public ActionResult<IEnumerable<SaleBuyOut>> GetAllMovementsSaleBuyOut()
        {
            var payMethods = _movementService.GetAllMovementsSaleBuyOut();
            return Ok(payMethods);
        }
    }
}
