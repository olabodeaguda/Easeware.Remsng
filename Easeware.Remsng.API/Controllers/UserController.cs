using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Easeware.Remsng.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [Route("api/v2.0/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITemplateService _templateService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService,
            TemplateService templateService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
            _templateService = templateService;
        }
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            UserModel um = await _userService.Get(userModel.email);
            string verificationCode = "";
            if (um != null)
            {
                ResponseModel responseModel = new ResponseModel();
                responseModel.code = ResponseCode.FAIL;

                if (um.userStatus == UserStatus.PENDING)
                {
                    // send confirmation email
                    string template = await _templateService.GetTemplate(TemplateType.EMAIL_CONFIRMATION);
                    if (!string.IsNullOrEmpty(template))
                    {
                        template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                        template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/{verificationCode}");
                    }

                    responseModel.description = $"An email has been send to the email for verification";
                }
                return Ok(responseModel);
            }

            bool result = await _userService.Add(userModel);
            if (result)
            {
                //semd mail for confirmation
                string template = await _templateService.GetTemplate(TemplateType.EMAIL_CONFIRMATION);
                if (!string.IsNullOrEmpty(template))
                {
                    template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                    template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/{verificationCode}");
                }
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{userModel.email} has been created successfully"
                });
            }

            throw new UnknownException("An error occurred while trying to create a user. Please try again or contact your administrator");
        }


        [HttpGet("verify/{vcode}")]
        public async Task<IActionResult> Verify(string vcode)
        {
            return Ok();
        }
    }
}