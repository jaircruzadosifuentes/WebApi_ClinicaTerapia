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
    public class ContabilidadController : ControllerBase
    {
        private readonly IContabilidadService _contabilidadService;

        public ContabilidadController(IContabilidadService contabilidadService)
        {
            _contabilidadService = contabilidadService;
        }
        [HttpGet("GetCajaChicaMontos/{dateOpened}/{employeedCashId}")]
        public ActionResult<IEnumerable<CajaChicaMontos>> GetCajaChicaMontos(DateTime dateOpened, int employeedCashId)
        {
            var cajaChicaMontos = _contabilidadService.GetCajaChicaMontos(dateOpened, employeedCashId);
            return Ok(cajaChicaMontos);
        } 
        [HttpGet("GetHistDetailCajaChicaByIdEmployeed/{employeedId}")]
        public ActionResult<IEnumerable<CajaChica>> GetHistDetailCajaChicaByIdEmployeed(int employeedId)
        {
            var lstHistCajaChica = _contabilidadService.GetHistDetailCajaChicaByIdEmployeed(employeedId);
            return Ok(lstHistCajaChica);
        } 
        [HttpGet("GetDetailMovementsCajaChica/{dateOpened}/{employeedCashId}")]
        public ActionResult<IEnumerable<CajaChica>> GetDetailMovementsCajaChica(DateTime dateOpened, int employeedCashId)
        {
            var cajaChicaMontos = _contabilidadService.GetDetailMovementsCajaChica(dateOpened, employeedCashId);
            return Ok(cajaChicaMontos);
        }
        [HttpGet("VerifyCajaChica/{dateOpened}/{employeedCashId}")]
        public ActionResult<CajaChica> VerifyCajaChica(DateTime dateOpened, int employeedCashId)
        {
            var verifyCajaChica = _contabilidadService.VerifyCajaChica(dateOpened, employeedCashId);
            return Ok(verifyCajaChica);
        } 
        [HttpGet("DetailDataEmployeedCajaChica/{employeedId}/{dateApertu}")]
        public ActionResult<CajaChica> DetailDataEmployeedCajaChica(int employeedId, DateTime dateApertu)
        {
            var verifyCajaChica = _contabilidadService.DetailDataEmployeedCajaChica(employeedId, dateApertu);
            return Ok(verifyCajaChica);
        }
        [HttpPost("PostCloseCajaChica")]
        public void PostInsertPaySolicitud(CajaChica cajaChica)
        {
            _contabilidadService.PostCloseCajaChica(cajaChica);
        } 
        [HttpPut("CloseCashRegisterById")]
        public void CloseCashRegisterById(CashRegisterDetail cashRegisterDetail)
        {
            _contabilidadService.CloseCashRegisterById(cashRegisterDetail);
        } 
        [HttpPost("PostApertuCajaChica")]
        public void PostApertuCajaChica(CajaChica cajaChica)
        {
            _contabilidadService.PostApertuCajaChica(cajaChica);
        }
    }
}
