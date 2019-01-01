using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Easeware.Remsng.API.Controllers
{
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/address")]
    public class AddressController : Controller
    {
        private IAddressManager _addressManager;

        public AddressController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AddressModel value)
        {
            var result = await _addressManager.CreateAddress(value);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = result,
                description = "Address has been created successfully"
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]AddressModel value)
        {
            var result = await _addressManager.UpdateAddress(value);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = result,
                description = "Address has been updated successfully"
            });
        }

        // DELETE api/<controller>/5
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _addressManager.DeActivate(id);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                data = result,
                description = "Address has been deactivated successfully"
            });
        }
    }
}
