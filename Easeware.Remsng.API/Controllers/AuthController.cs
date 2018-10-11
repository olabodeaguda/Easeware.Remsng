using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [Route("api/v2.0/auth")]
    public class AuthController : ControllerBase
    {
        private IEmailService _emailService;
        private IConfiguration _configuration;
        private IJwtService _jwtService;
        private IEncryptionService _encryptionService;
        private IUserService _userService;
        private IAuthService _authService;
        private ICodeGeneratorService _codeGenerationService;
        private IVerificationService _verificationService;
        private ITemplateService _templateService;
        public AuthController(IUserService userService,
            IEncryptionService encryptionService,
            ITemplateService templateService,
            IJwtService jwtService, IAuthService authService,
            IVerificationService verificationService,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _jwtService = jwtService;
            _authService = authService;
            _templateService = templateService;
            _verificationService = verificationService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginRequestModel loginRequestModel)
        {
            UserModel user = await _userService.Get(loginRequestModel.UserName);

            if (user == null)
            {
                throw new NotFoundException($"{loginRequestModel.UserName} does not exist");
            }
            if (loginRequestModel.Password != _encryptionService.Decrypt(user.passwordHash))
            {
                throw new BadRequestException("Password is incoreect");
            }

            LoginResponseModel loginResponseModel = new LoginResponseModel()
            {
                accessToken = _jwtService.Get(user),
                fullName = $"{user.lastname} {user.otherNames}",
                refreshToken = Guid.NewGuid().ToString(),
                accessId = user.id
            };

            await _authService.LogAccess(loginResponseModel);
            loginResponseModel.accessId = -1;

            return Ok(loginResponseModel);
        }

        [HttpPost]
        [Route("refreshtoken/{refreshtoken}")]
        public async Task<IActionResult> RefreshToken(string refreshtoken)
        {
            if (string.IsNullOrEmpty(refreshtoken))
            {
                throw new BadRequestException("Refresh token is invalid");
            }

            LoginResponseModel loginResponseModel = await _authService.SessionLog(refreshtoken);

            if (loginResponseModel == null)
            {
                throw new SessionExpiredException("");
            }

            UserModel userModel = await _userService.Get(loginResponseModel.accessId);
            if (userModel == null)
            {
                throw new NotFoundException($"{loginResponseModel.fullName} can not be found");
            }
            if (userModel.userStatus != UserStatus.ACTIVE)
            {
                throw new BadRequestException($"{userModel.lastname} {userModel.otherNames} is not active");
            }

            LoginResponseModel lRModel = new LoginResponseModel()
            {
                accessToken = _jwtService.Get(userModel),
                fullName = $"{userModel.lastname} {userModel.otherNames}",
                refreshToken = Guid.NewGuid().ToString(),
                accessId = userModel.id
            };

            await _authService.LogAccess(loginResponseModel);
            lRModel.accessId = -1;
            return Ok(loginResponseModel);
        }

        [HttpGet("chngpwdinitialize/{username}")]
        public async Task<IActionResult> ChangePwdInitialize(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new NotFoundException("Username is required");
            }

            UserModel userModel = await _userService.Get(username);
            if (userModel == null)
            {
                throw new NotFoundException($"{username} does not exist");
            }

            VerificationDetailModel vDetails = new VerificationDetailModel()
            {
                CreatedBy = User.Identity.Name,
                VerificationCode = _codeGenerationService.VerificationCode(),
                OwnerId = userModel.id.ToString()
            };
            bool vResult = await _verificationService.Add(vDetails);
            if (vResult)
            {
                string template = await _templateService.GetTemplate(TemplateType.PASSWORD_RESET);
                if (!string.IsNullOrEmpty(template))
                {
                    template = template.Replace("$FULLNAME$", $"{userModel.lastname} {userModel.otherNames}");
                    template = template.Replace("$PASSWORD_URL$", $"{_configuration["domainUrl"]}/api/v2.0/auth/changepassword/{vDetails.VerificationCode}");
                    NotificationModel notificationModel = new NotificationModel()
                    {
                        Content = template,
                        ContentType = EmailContentType.HTML,
                        Title = "Password Reset",
                        ToEmails = new List<string>() { userModel.email },
                        FromEmail = _configuration["EmailConfiguration:VerificationEmail"]
                    };

                    await _emailService.Send(notificationModel);
                }
            }

            return Ok(new ResponseModel()
            {
                code = ResponseCode.SUCCESSFULL,
                description = $"Please follow the link sent to your mail for password reset"
            });
        }

        [HttpPost]
        [Route("changepassword/{verifyCode}")]
        public async Task<IActionResult> Post(string verifyCode, [FromBody] ChangePasswordModel changePasswordModel)
        {
            if (string.IsNullOrEmpty(verifyCode))
            {
                throw new BadRequestException("Verification link is invalid");
            }

            VerificationDetailModel verificationDetailModel = await _verificationService.Get(verifyCode);
            if (verificationDetailModel == null)
            {
                throw new BadRequestException("Verification link is invalid");
            }
            if (verificationDetailModel.IsVerified)
            {
                throw new BadRequestException("Verification link has expired");
            }
            long id;
            if (!long.TryParse(verificationDetailModel.OwnerId, out id))
            {
                throw new BadRequestException("Verification link has expired");
            }

            UserModel userModel = await _userService.Get(id);
            if (userModel == null)
            {
                throw new BadRequestException("Verification link has expired");
            }

            if (changePasswordModel.OldPassword != _encryptionService.Decrypt(userModel.passwordHash))
            {
                throw new BadRequestException("Old Password is incorrect");
            }

            userModel.passwordHash = _encryptionService.Encrypt(changePasswordModel.NewPassword);

            bool result = await _userService.ChangePassword(userModel);
            if (result)
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = "Password change request was successful"
                });
            }

            throw new UnknownException("Change password request failed");
        }
    }
}
