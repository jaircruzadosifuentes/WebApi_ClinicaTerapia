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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetProductoByCategoryIdSubCategoryId/{categoryId}/{subCategoryId}")]
        public ActionResult<IEnumerable<Product>> GetProductoByCategoryIdSubCategoryId(int categoryId, int subCategoryId)
        {
            var products = _productService.GetProductoByCategoryIdSubCategoryId(categoryId, subCategoryId);
            return Ok(products);
        }
    }
}
