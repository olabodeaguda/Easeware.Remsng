using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [Route("api/v2.0/lcda")]
    //[ApiController]
    public class LcdaController : ControllerBase
    {
        private ILcdaService _lcdaService;
        public LcdaController(ILcdaService lcdaService)
        {
            _lcdaService = lcdaService;
        }

        [HttpPost]
        public IActionResult Add([FromBody] LcdaModel lcdaModel)
        {
            return Ok();
        }
    }
}