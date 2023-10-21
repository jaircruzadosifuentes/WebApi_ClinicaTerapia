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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }
        [HttpPost("PostSaveSaleHead")]
        public ActionResult<Sale> PostSaveSaleHead(Sale sale)
        {
            int saleReturn =_saleService.PostSaveSaleHead(sale);
            var correlativo = _saleService.GetCorrelativoSale(saleReturn);
            return Ok(correlativo);
        }
    }
}
