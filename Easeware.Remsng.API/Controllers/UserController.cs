using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/user")]
    public class UserController : ControllerBase
    {
        private IUserManager _uManager;
        private IEmailService _emailService;
        private ITemplateService _templateService;
        private IConfiguration _configuration;
        private IVerificationService _verificationService;
        private ICodeGeneratorService _codeGenerationService;
        public UserController(
            ITemplateService templateService,
            IConfiguration configuration,
            IVerificationService verificationService,
            ICodeGeneratorService codeGeneratorService,
            IEmailService emailService,
            IUserManager userManager)
        {
            _configuration = configuration;
            _verificationService = verificationService;
            _codeGenerationService = codeGeneratorService;
            _templateService = templateService;
            _emailService = emailService;
            _uManager = userManager;
        }
        public async Task<IActionResult> Post([FromBody] UserModel userModel)
        {
            UserModel uModel = await _uManager.CreateUser(userModel);
            uModel.passwordHash = null;
            if (uModel == null)
            {
                throw new BadRequestException("Registration failed");
            }
            else
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    data = uModel
                });
            }
            throw new UnknownException("An error occurred while trying to create a your login details. Please try again or contact your administrator");
        }

        [HttpGet("verify/{id}/{vcode}")]
        public async Task<IActionResult> Verify(long id, string vcode)
        {
            //if (string.IsNullOrEmpty(vcode))
            //{
            //    throw new BadRequestException("Bad request. PLease try again");
            //}

            //var udetails = await _userService.Get(id);
            //if (udetails == null)
            //{
            //    throw new BadRequestException("Verification details is invalid");
            //}

            //VerificationDetailModel vdetails = await _verificationService.Get(vcode);

            //if (vdetails == null)
            //{
            //    throw new BadRequestException($"Verification link is invalid");
            //}
            //if (vdetails.IsVerified)
            //{
            //    throw new BadRequestException($"Verification link has been used");
            //}
            //udetails.userStatus = UserStatus.ACTIVE;

            bool result = false;// await _userService.UpdateStatus(udetails);
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
