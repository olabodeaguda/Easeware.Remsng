using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/street")]
    [ApiController]
    public class StreetController : ControllerBase
    {
        private readonly IStreetManager _streetManager;
        public StreetController(IStreetManager streetManager)
        {
            _streetManager = streetManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StreetModel streetModel)
        {
            StreetModel sModel = await _streetManager.CreateStreet(streetModel);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = sModel,
                description = "Street has been created successfully"
            });
        }

        [HttpGet("ward/{wardId}")]
        public async Task<IActionResult> GetByWardId(long wardId)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _streetManager.Get(wardId)
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] StreetModel model)
        {
            StreetModel sm = await _streetManager.UpdateStreet(model);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _streetManager.UpdateStreet(model)
            });
        }
    }
}