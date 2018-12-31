using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/lcda")]
    public class LcdaController : ControllerBase
    {
        private readonly ILcdaManager _lcdaManager;
        public LcdaController(ILcdaManager lcdaManager)
        {
            _lcdaManager = lcdaManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LcdaModel lcdaModel)
        {
            var lcda = await _lcdaManager.CreateLcda(lcdaModel);
            if (lcda != null)
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    data = lcda,
                    description = $"{lcdaModel.LcdaName} has been created successfully"
                });
            }

            throw new BadRequestException("An error occur while processing your request. Please try again or contact an administrator");
        }
    }
}
