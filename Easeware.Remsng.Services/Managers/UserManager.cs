using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IVerificationRepository _vRepo;
        private ICodeGeneratorService _cgService;
        private IHttpContextAccessor _httpAccessor;
        private IJwtService _jwtService;
        private IUserRepository _uRepo;
        private IAuthService _authService;
        private IEmailService _emailService;
        private IConfiguration _configuration;
        private ITemplateService _templateService;
        private IEncryptionService _encryptionService;
        public UserManager(IUserRepository userRepository,
            IEncryptionService encryptionService,
            IJwtService jwtService,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            ICodeGeneratorService cgService,
            IEmailService emailService,
            IConfiguration configuration,
            ITemplateService templateService,
            IVerificationRepository verificationRepository)
        {
            _uRepo = userRepository;
            _jwtService = jwtService;
            _authService = authService;
            _httpAccessor = httpContextAccessor;
            _cgService = cgService;
            _emailService = emailService;
            _configuration = configuration;
            _templateService = templateService;
            _vRepo = verificationRepository;
            _encryptionService = encryptionService;
        }

        public async Task<LoginResponseModel> Authenticate(LoginRequestModel loginRequestModel)
        {
            UserModel usermodel = await _uRepo.Get(loginRequestModel.UserName);
            if (usermodel == null)
            {
                throw new NotFoundException($"{loginRequestModel.UserName} does not exist");
            }
            if (usermodel.userStatus != UserStatus.ACTIVE)
            {
                throw new BadRequestException($"{loginRequestModel.UserName} is not active");
            }
            if (loginRequestModel.Password != _encryptionService.Decrypt(usermodel.passwordHash))
            {
                throw new BadRequestException("Password is incoreect");
            }
            LoginResponseModel loginResponseModel = new LoginResponseModel()
            {
                accessToken = _jwtService.Get(usermodel),
                fullName = $"{usermodel.lastname} {usermodel.otherNames}",
                refreshToken = Guid.NewGuid().ToString(),
                accessId = usermodel.id
            };
            await _authService.LogAccess(loginResponseModel);
            loginResponseModel.accessId = -1;
            return loginResponseModel;
        }

        public async Task<bool> CompleteChangePwd(string verifyCode, ChangePasswordModel changePasswordModel)
        {
            if (string.IsNullOrEmpty(verifyCode))
            {
                throw new BadRequestException("Verification link is invalid");
            }

            VerificationDetailModel verificationDetailModel = await _vRepo.Get(verifyCode);
            if (verificationDetailModel == null)
            {
                throw new BadRequestException("Verification link is invalid");
            }
            if (verificationDetailModel.IsVerified)
            {
                throw new BadRequestException("Verification link has expired");
            }
            if (!long.TryParse(verificationDetailModel.OwnerId, out long id))
            {
                throw new BadRequestException("Verification link has expired");
            }

            UserModel userModel = await _uRepo.Get(id);
            if (userModel == null)
            {
                throw new BadRequestException("Verification link has expired");
            }

            userModel.passwordHash = _encryptionService.Encrypt(changePasswordModel.NewPassword);

            return await _uRepo.ChangePassword(userModel);
        }

        public async Task<UserModel> CreateUser(UserModel userModel)
        {
            userModel.passwordHash = _encryptionService.Encrypt(userModel.Password);
            UserModel um = await _uRepo.Get(userModel.email);
            if (um != null)
            {
                throw new BadRequestException($"{um.email} already exist");
            }

            return await _uRepo.Add(userModel);
        }

        public async Task<bool> InitiateChangePwd(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new BadRequestException($"{username} is required");
            }
            UserModel userModel = await _uRepo.Get(username);
            if (userModel == null)
            {
                throw new NotFoundException($"{username} does not exist");
            }
            VerificationDetailModel vDetails = new VerificationDetailModel()
            {
                CreatedBy = userModel.email,
                VerificationCode = _cgService.VerificationCode(),
                OwnerId = userModel.id.ToString()
            };

            bool vResult = await _vRepo.Add(vDetails);
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
            return vResult;
        }

        public async Task<LoginResponseModel> RefreshToken(string refreshtoken)
        {
            if (string.IsNullOrEmpty(refreshtoken))
            {
                throw new BadRequestException("Refresh tokenis required");
            }
            LoginResponseModel loginResponseModel = await _authService.GetSession(refreshtoken);

            if (loginResponseModel == null)
            {
                throw new SessionExpiredException("Session has expired");
            }

            UserModel userModel = await _uRepo.Get(loginResponseModel.accessId);
            if (userModel == null)
            {
                throw new NotFoundException($"{loginResponseModel.fullName} does not exist");
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

            await _authService.LogAccess(lRModel);
            await _authService.Remove(loginResponseModel);
            lRModel.accessId = -1;
            return lRModel;
        }
    }
}
