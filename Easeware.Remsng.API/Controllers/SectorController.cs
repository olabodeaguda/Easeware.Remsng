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
    [Route("api/v2.0/sector")]
    [ModelValidation]
    public class SectorController : ControllerBase
    {
        private ISectorService sectorService;
        public SectorController(ISectorService sectorService)
        {

        }

        [HttpGet("{LcdaCode}")]
        public async Task<IActionResult> Get(string LcdaCode)
        {
            if (string.IsNullOrEmpty(LcdaCode))
            {
                throw new BadRequestException("Lcda is required");
            }

            return Ok();
        }

    }
}