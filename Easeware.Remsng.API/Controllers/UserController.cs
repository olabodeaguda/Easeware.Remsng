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
        private readonly IVerificationService _verificationService;
        private readonly ICodeGeneratorService _codeGenerationService;
        public UserController(IUserService userService,
            TemplateService templateService,
            IConfiguration configuration,
            IVerificationService verificationService, ICodeGeneratorService codeGeneratorService)
        {
            _userService = userService;
            _configuration = configuration;
            _templateService = templateService;
            _verificationService = verificationService;
            _codeGenerationService = codeGeneratorService;
        }
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            UserModel um = await _userService.Get(userModel.email);
            VerificationDetailModel vDetails = new VerificationDetailModel()
            {
                CreatedBy = User.Identity.Name,
                OwnerId = um.id.ToString(),
                VerificationCode = _codeGenerationService.VerificationCode()
            };

            if (um != null)
            {
                ResponseModel responseModel = new ResponseModel();
                responseModel.code = ResponseCode.FAIL;

                if (um.userStatus == UserStatus.PENDING)
                {
                    bool vResult = await _verificationService.Add(vDetails);
                    if (vResult)
                    {
                        string template = await _templateService.GetTemplate(TemplateType.EMAIL_CONFIRMATION);
                        if (!string.IsNullOrEmpty(template))
                        {
                            template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                            template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/{um.id}/{vDetails.VerificationCode}");
                        }
                    }

                    responseModel.description = $"An email has been send to the email for verification";
                }
                return Ok(responseModel);
            }

            bool result = await _userService.Add(userModel);
            if (result)
            {
                UserModel umm = await _userService.Get(userModel.email);
                bool vResult = await _verificationService.Add(vDetails);
                if (vResult)
                {
                    string template = await _templateService.GetTemplate(TemplateType.EMAIL_CONFIRMATION);
                    if (!string.IsNullOrEmpty(template))
                    {
                        template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                        template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/{umm.id}/{vDetails.VerificationCode}");
                    }
                }
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{userModel.email} has been created successfully",
                    data = umm.id
                });
            }

            throw new UnknownException("An error occurred while trying to create a your login details. Please try again or contact your administrator");
        }

        [HttpGet("verify/{id}/{vcode}")]
        public async Task<IActionResult> Verify(long id, string vcode)
        {
            if (string.IsNullOrEmpty(vcode))
            {
                throw new BadRequestException("Bad request. PLease try again");
            }

            var udetails = await _userService.Get(id);
            if (udetails == null)
            {
                throw new BadRequestException("Verification details is invalid");
            }

            VerificationDetailModel vdetails = await _verificationService.Get(vcode);

            if (vdetails == null)
            {
                throw new BadRequestException($"Verification link is invalid");
            }
            if (vdetails.IsVerified)
            {
                throw new BadRequestException($"Verification link has been used");
            }

            bool result = await _verificationService.InValidateCode(vcode, id.ToString());
            if (result)
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = "Verification is successful"
                });
            }
            else
            {
                throw new BadRequestException("Verification process failed. Please try again or contact your administrator");
            }
        }
    }
}
