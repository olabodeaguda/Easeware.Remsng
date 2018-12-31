using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/ward")]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly IWardManager _wardManager;
        public WardController(IWardManager wardManager)
        {
            _wardManager = wardManager;
        }

        [HttpGet("{lcdaCode}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> Get(string lcdaCode, int pageNumber = 1, int pageSize = 20)
        {
            var pageModel = await _wardManager.Get(new PageModel()
            {
                PageNumber = pageNumber < 1 ? 1 : pageNumber,
                PageSize = pageSize < 1 ? 20 : pageSize
            }, lcdaCode);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = pageModel
            });
        }

        [HttpGet("{lcdaCode}")]
        public async Task<IActionResult> GetByLcda(string lcdaCode)
        {
            var pageModel = await _wardManager.GetByLcdaWard(lcdaCode);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = pageModel
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WardModel wardModel)
        {
            var result = await _wardManager.CreateWard(wardModel);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = result
            });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] WardModel wardModel, long id)
        {
            wardModel.Id = id;
            WardModel wm = await _wardManager.UpdateWard(wardModel);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = wm
            });
        }

        [Authorize]
        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(long id)
        {
            WardModel wModel = await _wardManager.DeActivate(id);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = wModel,
                description = "Ward has been deactivated successfully"
            });
        }

        [Authorize]
        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(long id)
        {
            WardModel wModel = await _wardManager.Activate(id);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = wModel,
                description = "Ward has been activated successfully"
            });
        }
    }
}