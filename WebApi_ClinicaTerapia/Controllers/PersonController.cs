using Common;
using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IErrorService _errorService;

        public PersonController(IPersonService personService, IErrorService errorService)
        {
            _personService = personService;
            _errorService = errorService;
        }

        [HttpGet(Name = "GetPersonAll")]
        public ActionResult<IEnumerable<Person>> Get()
        {
            try
            {
                var listPersons =  _personService.GetAll();
                return Ok(listPersons);
            }
            catch (Exception ex)
            {
                Error error = new()
                {
                    Description = ex.Message, DescriptionTrace = ex.StackTrace, CodeUser = "SIST", TypeError = (int)EnumTypeError.CONTROLLER, CreatedAt = DateTime.Now
                };

                int codeError = _errorService.InsertErrorRepository(error);
                throw new Exception(string.Concat(Constantes.G_MESSAGE_ERROR_WITH_CODE, codeError.ToString()));
            }
           
        } 
        [HttpGet("GetPersonByNroDocument/{nroDocument}")]
        public ActionResult<Person> GetPersonByNroDocument(string nroDocument)
        {
           
            var person =  _personService.GetPersonByNroDocument(nroDocument);
            return Ok(person);
        }
    }
}
