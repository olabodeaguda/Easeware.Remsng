using Easeware.Remsng.Common.Models;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Services
{
    public interface IJwtService
    {
        string Get(UserModel userModel);
        object ValidatorParameters();
    }
}
