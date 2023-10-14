using Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Services;

namespace WebApi_ClinicaTerapia.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [EnableCors("_myAllowSpecificOrigins")]
        [HttpGet(Name = "GetAllDocuments")]
        public ActionResult<IEnumerable<Document>> GetAllDocuments()
        {
            var listDocuments = _documentService.GetAllDocuments();
            return Ok(listDocuments);
        }
    }
}
