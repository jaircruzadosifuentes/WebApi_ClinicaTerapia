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
    public class EmployeedController : ControllerBase
    {
        private readonly IEmployeedService _employeedService;

        public EmployeedController(IEmployeedService employeedService)
        {
            _employeedService = employeedService;
        }
        [HttpGet(Name = "GetAllEmployeed")]
        public ActionResult<IEnumerable<Employeed>> GetAllEmployeed()
        {
            var employeeds = _employeedService.GetAllEmployeed();
            return Ok(employeeds);
        }  
        [HttpGet("GetAllEmployeedPendingAproval")]
        public ActionResult<IEnumerable<Employeed>> GetAllEmployeedPendingAproval()
        {
            var employeeds = _employeedService.GetAllEmployeedPendingAproval();
            return Ok(employeeds);
        } 
        [HttpGet("{dateToConsult}/{employeedId}")]
        public ActionResult<IEnumerable<EmployeedDisponibilty>> GetSchedulesDisponibility(DateTime dateToConsult, int employeedId)
        {
            var employeeds = _employeedService.GetSchedulesDisponibility(dateToConsult, employeedId);
            return Ok(employeeds);
        } 
        [HttpGet("PostAccessSystem/{user}/{password}")]
        public Employeed PostAccessSystem(string user, string password)
        {
            Employeed employeed = new()
            {
                User = user,
                Password = password
            };

            var employeedReturn = _employeedService.PostAccessSystem(employeed);
            return employeedReturn;
        } 
        [HttpGet("GetByUserNameEmployeed/{username}")]
        public Employeed GetByUserNameEmployeed(string username)
        {
            var employeedReturn = _employeedService.GetByUserNameEmployeed(username);
            return employeedReturn;
        }
        [HttpPost("PostRegisterEmployeed")]
        public void PostRegisterEmployeed(Employeed employeed)
        {
            _employeedService.PostRegisterEmployeed(employeed);
        }
        [HttpPut("PutAppproveContractEmployeed")]
        public void PutAppproveContractEmployeed(Employeed employeed)
        {
            _employeedService.PutAppproveContractEmployeed(employeed);
        }
    }
}
