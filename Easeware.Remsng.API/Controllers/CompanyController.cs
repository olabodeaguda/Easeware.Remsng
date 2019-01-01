using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private ICompanyManager _cManager;
        public CompanyController(ICompanyManager companyManager)
        {
            _cManager = companyManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyModel companyModel)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                description = "Company has been created successfully",
                data = await _cManager.Add(companyModel)
            });
        }
        [HttpGet("lcdaCode")]
        public async Task<IActionResult> Get(string lcdaCode,
            [FromQuery]int pageSize = 20,
            [FromQuery] int pageNum = 1)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _cManager.Get(lcdaCode, new PageModel()
                {
                    PageNumber = pageNum,
                    PageSize = pageSize
                })
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetByLCDA([FromQuery]string lcdaCode)
        {
            var result = await _cManager.Get(lcdaCode);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = result
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update(CompanyModel companyModel)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                description = "Update is successful",
                data = await _cManager.Update(companyModel)
            });
        }

        [HttpPost("activate/{id}")]
        public async Task<IActionResult> Activate(long id)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                description = "Activation is successful",
                data = await _cManager.Activate(id)
            });
        }

        [HttpPost("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(long id)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                description = "Deactivation is successful",
                data = await _cManager.Deactivate(id)
            });
        }
    }
}