using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/sector")]
    [ModelValidation]
    public class SectorController : ControllerBase
    {
        private ISectorService _sectorService;
        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpGet("{LcdaCode}")]
        public async Task<IActionResult> Get(string LcdaCode)
        {
            return Ok(await _sectorService.GetByLcdaAsync(LcdaCode));
        }



    }
}