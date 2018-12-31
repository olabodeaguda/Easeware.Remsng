using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Managers
{
    public class LcdaManager : ILcdaManager
    {
        private IHttpContextAccessor _httpAccessor;
        private readonly IUserRepository _uRepo;
        private readonly IUserLcdaRepository _ulRepo;
        private readonly ILcdaRepository _lcdaRepo;
        private readonly ICodeGeneratorService _codeGenerationService;
        public LcdaManager(ILcdaRepository lcdaRepository,
            ICodeGeneratorService codeGeneratorService,
            IUserLcdaRepository userLcdaRepository,
            IUserRepository uRepo,
            IHttpContextAccessor httpContextAccessor)
        {
            _lcdaRepo = lcdaRepository;
            _codeGenerationService = codeGeneratorService;
            _ulRepo = userLcdaRepository;
            _uRepo = uRepo;
            _httpAccessor = httpContextAccessor;
        }

        public Task<LcdaModel> ApproveLcda(LcdaModel lcdaModel)
        {
            throw new NotImplementedException();
        }

        public async Task<LcdaModel> CreateLcda(LcdaModel lcdaModel)
        {
            long lastId = await _lcdaRepo.LastId();
            lcdaModel.LcdaCode = _codeGenerationService.NewCode(lastId, "Rems");
            lcdaModel.CreatedBy = _httpAccessor.HttpContext.User.Identity.Name;
            var lcda = await _lcdaRepo.Add(lcdaModel);
            string userId = _httpAccessor.HttpContext.User.Claims.GetUserId();
            await AddUserLcda(lcda, long.Parse(userId));

            return lcda;
        }

        public Task<LcdaModel> DeleteLcda(LcdaModel lcdaModel)
        {
            throw new NotImplementedException();
        }

        public Task<LcdaModel> UpdateLcda(LcdaModel lcdaModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UserLcdaModel> AddUserLcda(LcdaModel lcda, long userId)
        {
            UserModel user = await _uRepo.Get(userId);
            var model = new UserLcdaModel()
            {
                UserId = userId,
                LcdaId = lcda.Id
            };
            bool result = await _ulRepo.Add(model);
            if (result)
            {
                return model;
            }
            return null;
        }
    }
}
