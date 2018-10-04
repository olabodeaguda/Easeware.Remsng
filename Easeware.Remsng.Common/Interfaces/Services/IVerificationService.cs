using Easeware.Remsng.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IVerificationService
    {
        Task<bool> Add(VerificationDetailModel verificationDetailModel);
        Task<VerificationDetailModel> Get(string verificationCode);
        Task<bool> InValidateCode(string verificationCode, string ownerId);
    }
}
