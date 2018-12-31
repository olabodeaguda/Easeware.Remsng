using Easeware.Remsng.Common.Models;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface IUserManager
    {
        Task<LoginResponseModel> Authenticate(LoginRequestModel loginRequestModel);
        Task<LoginResponseModel> RefreshToken(string refreshtoken);
        Task<bool> InitiateChangePwd(string username);
        Task<bool> CompleteChangePwd(string verificationCode,
            ChangePasswordModel changePasswordModel);
        Task<UserModel> CreateUser(UserModel userModel);
    }
}
