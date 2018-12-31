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
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private IUserManager _uManager;
        private IEmailService _emailService;
        private IConfiguration _configuration;
        private IJwtService _jwtService;
        private IEncryptionService _encryptionService;
        private IAuthService _authService;
        private ICodeGeneratorService _codeGenerationService;
        private IVerificationService _verificationService;
        private ITemplateService _templateService;
        public AuthController(IUserManager userManager,
            IEncryptionService encryptionService,
            ITemplateService templateService,
            IJwtService jwtService, IAuthService authService,
            IVerificationService verificationService,
            IConfiguration configuration,
            IEmailService emailService, ICodeGeneratorService codeGenerationService)
        {
            _uManager = userManager;
            _encryptionService = encryptionService;
            _jwtService = jwtService;
            _authService = authService;
            _templateService = templateService;
            _verificationService = verificationService;
            _configuration = configuration;
            _emailService = emailService;
            _codeGenerationService = codeGenerationService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequestModel loginRequestModel)
        {
            var result = await _uManager.Authenticate(loginRequestModel);
            return Ok(result);
        }

        [HttpGet]
        [Route("refreshtoken/{refreshtoken}")]
        public async Task<IActionResult> RefreshToken(string refreshtoken)
        {
            var lrModel = await _uManager.RefreshToken(refreshtoken);
            return Ok(lrModel);
        }

        [HttpGet("chngpwdinitialize/{username}")]
        public async Task<IActionResult> ChangePwdInitialize(string username)
        {
            bool result = await _uManager.InitiateChangePwd(username);
            if (!result)
            {
                return BadRequest(new ResponseModel()
                {
                    code = ResponseCode.FAIL,
                    description = "Request failed"
                });
            }

            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFUL,
                description = $"Please follow the link sent to your mail for password reset"
            });
        }

        [HttpPost]
        [Route("chngpwd/{verifyCode}")]
        public async Task<IActionResult> Post(string verifyCode, [FromBody] ChangePasswordModel changePasswordModel)
        {
            bool result = await _uManager.CompleteChangePwd(verifyCode, changePasswordModel);
            if (result)
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    description = "Password change request was successful"
                });
            }

            throw new UnknownException("Change password request failed");
        }
    }
}
