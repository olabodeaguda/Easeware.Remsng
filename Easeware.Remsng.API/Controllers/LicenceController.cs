using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [Route("api/v2.0/license")]
    [ApiController]
    public class LicenceController : ControllerBase
    {
        private ILicenceService _licenceService;
        public LicenceController(ILicenceService licenceService)
        {
            _licenceService = licenceService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] LicenceModel licenceModel)
        {
            if (string.IsNullOrEmpty(licenceModel.DateString))
            {
                throw new BadRequestException("Date is required");
            }
            else if (string.IsNullOrEmpty(licenceModel.LcdaCode))
            {
                throw new BadRequestException("LCDA Code is required");
            }
            DateTime? dateTime = licenceModel.DateString.ToDate();
            if (dateTime == null)
            {
                throw new BadRequestException("Date Format in invalid. 'dd-mm-yyyy'");
            }
            //validate existence of the lcda
            //call license table for existence of valid license key for lcda



            licenceModel.ValidTimeSpan = dateTime.Value.Ticks;
            string lc = _licenceService.Encrypt(licenceModel);
            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFULL,
                description = "Code has successfully been created",
                data = lc
            });
        }

        [HttpGet("verify")]
        public IActionResult VerifyLicense([FromQuery] string lckey)
        {
            return Ok(lckey);
        }
    }
}