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
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        
        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        [HttpGet("GetAllPayMethods")]
        public ActionResult<IEnumerable<PayMethod>> GetAllPayMethods()
        {
            var payMethods = _commonService.GetAllPayMethods();
            return Ok(payMethods);
        }  
        [HttpGet("GetPathologies")]
        public ActionResult<IEnumerable<Pathologies>> GetPathologies()
        {
            var pathologies = _commonService.GetPathologies();
            return Ok(pathologies);
        }  
        [HttpGet("GetInComboTypeOfContract")]
        public ActionResult<IEnumerable<TypeOfContract>> GetInComboTypeOfContract()
        {
            var typeOfContracts = _commonService.GetInComboTypeOfContract();
            return Ok(typeOfContracts);
        } 
        [HttpGet("GetInComboModalityContract")]
        public ActionResult<IEnumerable<ModalityContract>> GetInComboModalityContract()
        {
            var typeOfContracts = _commonService.GetInComboModalityContract();
            return Ok(typeOfContracts);
        }  
        [HttpGet("GetInComboAfpSure")]
        public ActionResult<IEnumerable<AfpSure>> GetInComboAfpSure()
        {
            var typeOfContracts = _commonService.GetInComboAfpSure();
            return Ok(typeOfContracts);
        } 
        [HttpGet("GetInComboRole")]
        public ActionResult<IEnumerable<Role>> GetInComboRole()
        {
            var typeOfContracts = _commonService.GetInComboRole();
            return Ok(typeOfContracts);
        }  
        [HttpGet("GetReportMensualCategoryTTO")]
        public ActionResult<IEnumerable<object>> GetReportMensualCategoryTTO()
        {
            var typeOfContracts = _commonService.GetReportMensualCategoryTTO();
            return Ok(typeOfContracts);
        } 
        [HttpGet("GetRoles")]
        public ActionResult<IEnumerable<Role>> GetRoles()
        {
            var typeOfContracts = _commonService.GetRoles();
            return Ok(typeOfContracts);
        }  
        [HttpGet("GetAreasInSelect")]
        public ActionResult<IEnumerable<Area>> GetAreasInSelect()
        {
            var typeOfContracts = _commonService.GetAreasInSelect();
            return Ok(typeOfContracts);
        } 
        [HttpGet("GetAllFrecuency")]
        public ActionResult<IEnumerable<Frecuency>> GetAllFrecuency()
        {
            var frecuencies = _commonService.GetAllFrecuency();
            return Ok(frecuencies);
        } 
        [HttpGet("GetCountPatientsType")]
        public ActionResult<IEnumerable<Dashboard>> GetCountPatientsType()
        {
            var dashboards = _commonService.GetCountPatientsType();
            return Ok(dashboards);
        }
        [HttpPost("PostRegisterFrecuencyClinic")]
        public void PostRegisterFrecuencyClinic(Frecuency frecuency)
        {
            _commonService.PostRegisterFrecuencyClinic(frecuency);

        } 
        [HttpPost("PostRegisterRole")]
        public void PostRegisterRole(Role role)
        {
            _commonService.PostRegisterRole(role);

        }
        [HttpPut("PutUpdateFrecuencyClinic")]
        public void PutUpdateFrecuencyClinic(Frecuency frecuency)
        {
            _commonService.PutUpdateFrecuencyClinic(frecuency);

        } 
        [HttpPut("PutDisabledEnabledRole/{roleId}/{type}")]
        public void PutDisabledEnabledRole(int roleId, int type)
        {
            _commonService.PutDisabledEnabledRole(roleId, type);

        } 
        [HttpPut("PutRole")]
        public void PutRole(Role role)
        {
            _commonService.PutRole(role);

        }
        [HttpGet("GetOptions/{employeedId}")]
        public ActionResult<IEnumerable<Option>> GetOptions(int employeedId)
        {
            var options = _commonService.GetOptions(employeedId);
            return Ok(options);
        } 
        [HttpGet("GetOptionsGeneral")]
        public ActionResult<IEnumerable<Option>> GetOptionsGeneral()
        {
            var options = _commonService.GetOptionsGeneral();
            return Ok(options);
        }
        [HttpGet("GetOptionsItemGeneral")]
        public ActionResult<IEnumerable<OptionItems>> GetOptionsItemGeneral()
        {
            var options = _commonService.GetOptionsItemGeneral();
            return Ok(options);
        }
        [HttpGet("GetRoutes/{employeedId}")]
        public ActionResult<IEnumerable<Option>> GetRoutes(int employeedId)
        {
            var options = _commonService.GetRoutes(employeedId);
            return Ok(options);
        }
        [HttpGet("GetOptionsByCodeEmployeed/{code}")]
        public ActionResult<IEnumerable<Option>> GetOptionsByCodeEmployeed(string code)
        {
            var options = _commonService.GetOptionsByCodeEmployeed(code);
            return Ok(options);
        } 
        [HttpGet("GetOptionsItemsByCodeEmployeed/{code}")]
        public ActionResult<IEnumerable<Option>> GetOptionsItemsByCodeEmployeed(string code)
        {
            var options = _commonService.GetOptionsItemsByCodeEmployeed(code);
            return Ok(options);
        }
        [HttpPut("PutRemoveAddOptionEmployeed/{optionItemId}")]
        public void PutRemoveAddOptionEmployeed(int optionItemId)
        {
            _commonService.PutRemoveAddOptionEmployeed(optionItemId);
        }
        [HttpPut("PutConfig")]
        public void PutConfig(Config config)
        {
            _commonService.PutConfig(config);
        }
        [HttpPut("PutAddOptionEmployeed/{optionItemId}/{optionId}/{code}")]
        public void PutAddOptionEmployeed(int optionItemId, int optionId, string code)
        {
            _commonService.PutAddOptionEmployeed(optionItemId, optionId, code);
        }
        [HttpPost("PutAddOptionFather/{codeEmployeed}/{optionId}")]
        public void PutAddOptionFather(string codeEmployeed, int optionId)
        {
            _commonService.PutAddOptionFather(codeEmployeed, optionId);
        }
        [HttpGet("GetInSelectVoucherDocument")]
        public ActionResult<IEnumerable<VoucherDocument>> GetInSelectVoucherDocument()
        {
            var options = _commonService.GetInSelectVoucherDocument();
            return Ok(options);
        }
        [HttpGet("VerifyPatientByFullName/{surnames}/{names}")]
        public ActionResult<IEnumerable<VerifyPatient>> VerifyPatientByFullName(string surnames, string names)
        {
            var verifyPatients = _commonService.VerifyPatientByFullName(surnames.Trim(), names.Trim());
            return Ok(verifyPatients);
        }  
        [HttpGet("GetRoutesSpecial/{userAccess}")]
        public ActionResult<IEnumerable<Routes>> GetRoutesSpecial(string userAccess)
        {
            var routes = _commonService.GetRoutesSpecial(userAccess);
            return Ok(routes);
        } 
        [HttpGet("GetAllConfigs")]
        public ActionResult<IEnumerable<Config>> GetAllConfigs()
        {
            var configs = _commonService.GetAllConfigs();
            return Ok(configs);
        } 
        [HttpGet("GetCategoriesInSelect")]
        public ActionResult<IEnumerable<CategoryEntity>> GetCategoriesInSelect()
        {
            var configs = _commonService.GetCategoriesInSelect();
            return Ok(configs);
        } 
        [HttpGet("GetSubCategoriesInSelect/{categoryId}")]
        public ActionResult<IEnumerable<CategoryEntity>> GetSubCategoriesInSelect(int categoryId)
        {
            var configs = _commonService.GetSubCategoriesInSelect(categoryId);
            return Ok(configs);
        }
    }
}
