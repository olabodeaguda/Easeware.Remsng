using AutoMapper;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Repositories
{
    public class UserLcdaRepository : IUserLcdaRepository
    {
        private RemsDbContext _remsDbContext;
        private IMapper _mapper;
        public UserLcdaRepository(RemsDbContext remsDbContext, IMapper mapper)
        {
            _remsDbContext = remsDbContext;
            _mapper = mapper;
        }
        public async Task<bool> Add(UserLcdaModel userLcdaModel)
        {
            UserLcda userLcda = _mapper.Map<UserLcda>(userLcdaModel);
            _remsDbContext.UserLcdas.Add(userLcda);
            int count = await _remsDbContext.SaveChangesAsync();
            return count > 0;
        }

        public async Task<UserLcdaModel> Get(UserLcdaModel userLcdaModel)
        {
            UserLcda userLcda = await _remsDbContext.UserLcdas
                .FirstOrDefaultAsync(x => x.LcdaId == userLcdaModel.LcdaId
                && x.UserId == userLcdaModel.UserId);

            if (userLcda == null)
            {
                return null;
            }

            return _mapper.Map<UserLcdaModel>(userLcda);
        }

        public async Task<bool> Remove(UserLcdaModel userLcdaModel)
        {
            UserLcda userLcda = _mapper.Map<UserLcda>(userLcdaModel);
            _remsDbContext.UserLcdas.Remove(userLcda);
            int count = await _remsDbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
