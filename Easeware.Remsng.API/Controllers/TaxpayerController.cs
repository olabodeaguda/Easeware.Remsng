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
    [Route("api/v{version:apiVersion}/taxpayer")]
    public class TaxpayerController : ControllerBase
    {
        ITaxpayerManager _taxpayerManager;
        public TaxpayerController(ITaxpayerManager taxpayerManager)
        {
            _taxpayerManager = taxpayerManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaxpayerRegistrationModel trModel)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _taxpayerManager.CreateTaxpayer(trModel),
                description = "Taxpayer has been created successfuly"
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaxpayerModel taxpayerModel)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _taxpayerManager.UpdateTaxpayer(taxpayerModel),
                description = "Taxpayer has been updated successfuly"
            });
        }

        [HttpGet("{lcdaId}")]
        public async Task<IActionResult> Get(long lcdaId)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _taxpayerManager.GetByLcda(lcdaId)
            });
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]long lcdaId, [FromQuery]int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _taxpayerManager.Get(lcdaId, new PageModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                })
            });
        }

        [HttpPost("{taxpayerId}/deactivate")]
        public async Task<IActionResult> Deactivate(long taxpayerId)
        {
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = await _taxpayerManager.Deactivate(taxpayerId)
            });
        }
    }
}