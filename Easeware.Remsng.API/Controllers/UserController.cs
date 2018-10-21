using System.Collections.Generic;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private IEmailService _emailService;
        private IUserService _userService;
        private ITemplateService _templateService;
        private IConfiguration _configuration;
        private IVerificationService _verificationService;
        private ICodeGeneratorService _codeGenerationService;
        public UserController(IUserService userService,
            ITemplateService templateService,
            IConfiguration configuration,
            IVerificationService verificationService,
            ICodeGeneratorService codeGeneratorService,
            IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _verificationService = verificationService;
            _codeGenerationService = codeGeneratorService;
            _templateService = templateService;
            _emailService = emailService;
        }
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            UserModel um = await _userService.Get(userModel.email);
            VerificationDetailModel vDetails = new VerificationDetailModel()
            {
                CreatedBy = User.Identity.Name,
                VerificationCode = _codeGenerationService.VerificationCode()
            };

            if (um != null)
            {
                vDetails.OwnerId = um.id.ToString();
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
                            template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/api/v2.0/user/verify/{um.id}/{vDetails.VerificationCode}");
                            NotificationModel notificationModel = new NotificationModel()
                            {
                                Content = template,
                                ContentType = EmailContentType.HTML,
                                Title = "Email Verification",
                                ToEmails = new List<string>() { um.email },
                                FromEmail = _configuration["EmailConfiguration:VerificationEmail"]
                            };

                            await _emailService.Send(notificationModel);
                        }
                    }

                    responseModel.description = $"An email has been send to your email for verification";
                }
                return Ok(responseModel);
            }

            bool result = await _userService.Add(userModel);
            if (result)
            {
                UserModel umm = await _userService.Get(userModel.email);
                vDetails.OwnerId = umm.id.ToString();
                bool vResult = await _verificationService.Add(vDetails);
                if (vResult)
                {
                    string template = await _templateService.GetTemplate(TemplateType.EMAIL_CONFIRMATION);
                    if (!string.IsNullOrEmpty(template))
                    {
                        template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                        template = template.Replace("$VERIFICATION_URL$", $"{_configuration["domainUrl"]}/api/v2.0/user/verify/{umm.id}/{vDetails.VerificationCode}");
                        NotificationModel notificationModel = new NotificationModel()
                        {
                            Content = template,
                            ContentType = EmailContentType.HTML,
                            Title = "Email Verification",
                            ToEmails = new List<string>() { umm.email },
                            FromEmail = _configuration["EmailConfiguration:VerificationEmail"]
                        };

                        await _emailService.Send(notificationModel);
                    }
                }
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
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
            udetails.userStatus = UserStatus.ACTIVE;

            bool result = await _userService.UpdateStatus(udetails);
            if (result)
            {
                await _verificationService.InValidateCode(vcode, id.ToString());
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
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
