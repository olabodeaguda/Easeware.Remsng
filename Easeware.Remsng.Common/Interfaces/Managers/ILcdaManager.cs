using Easeware.Remsng.Common.Models;
using System.Threading.Tasks;

namespace Easeware.Remsng.Common.Interfaces.Managers
{
    public interface ILcdaManager
    {
        Task<LcdaModel> CreateLcda(LcdaModel lcdaModel);
        Task<LcdaModel> UpdateLcda(LcdaModel lcdaModel);
        Task<LcdaModel> ApproveLcda(LcdaModel lcdaModel);
        Task<LcdaModel> DeleteLcda(LcdaModel lcdaModel);
    }
}
