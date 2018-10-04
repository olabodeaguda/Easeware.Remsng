using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Services.Implementations
{
    public class VerificationService : IVerificationService
    {
        private readonly IVerificationManager _verificationManager;
        public VerificationService(IVerificationManager verificationManager)
        {
            _verificationManager = verificationManager;
        }
        public async Task<bool> Add(VerificationDetailModel verificationDetailModel)
        {
            return await _verificationManager.Add(verificationDetailModel);
        }

        public async Task<VerificationDetailModel> Get(string verificationCode)
        {
            return await _verificationManager.Get(verificationCode);
        }

        public async Task<bool> InValidateCode(string verificationCode, string ownerId)
        {
            return await _verificationManager.InValidateCode(verificationCode,ownerId);
        }
    }
}
