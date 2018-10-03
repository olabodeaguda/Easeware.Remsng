using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [Route("api/v2.0/lcda")]
    public class LcdaController : ControllerBase
    {
        private ILcdaService _lcdaService;
        public LcdaController(ILcdaService lcdaService)
        {
            _lcdaService = lcdaService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LcdaModel lcdaModel)
        {
            long lastId = await _lcdaService.LastId();
            lcdaModel.LcdaCode = lastId.GenerateLCDACode();
            bool result = await _lcdaService.Add(lcdaModel);
            if (result)
            {
                // add current user to the lcda
            }

            return Ok();
        }
    }
}