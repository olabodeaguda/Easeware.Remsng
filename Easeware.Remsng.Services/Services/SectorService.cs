using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Infrastructure.Services
{
    public class SectorService : ISectorService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private ISectorRepository _sectorManager;
        public SectorService(ISectorRepository sectorManager, IHttpContextAccessor contextAccessor)
        {
            _sectorManager = sectorManager;
            _contextAccessor = contextAccessor;
        }
        public async Task<ResponseModel> AddAsync(SectorModel sectorModel)
        {
            SectorModel sModel = await _sectorManager.Get(sectorModel.LcdaCode, sectorModel.SectorCode);
            if (sModel != null)
            {
                throw new BadRequestException("Sector already exist");
            }
            sectorModel.SectorStatus = SectorStatus.ACTIVE;
            sectorModel.CreatedBy = _contextAccessor.HttpContext.User.Identity.Name;
            int count = await _sectorManager.AddAsync(sectorModel);
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    description = $"{sectorModel.SectorName} sector has been added successfully"
                };
            }

            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = $"An error occur while trying to create sector. " +
                $"Please try again or contact an administrator if error persist"
            };
        }

        public async Task<ResponseModel> Delete(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Url is in the wrong format");
            }
            SectorModel sectorModel = await _sectorManager.Get(id);
            if (sectorModel == null)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.NOTFOUND,
                    description = "Sector can not be found"
                };
            }

            sectorModel.ModifiedBy = _contextAccessor.HttpContext.User.Identity.Name;
            int count = await _sectorManager.Delete(sectorModel);
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFUL,
                    description = $"{sectorModel.SectorName} has been deleted successfully"
                };
            }
            else
            {
                return new ResponseModel()
                {
                    code = ResponseCode.FAIL,
                    description = $"{sectorModel.SectorName} has been deleted successfully"
                };
            }
        }

        public Task<SectorModel> Get(long Id)
        {
            return _sectorManager.Get(Id);
        }

        public Task<List<SectorModel>> GetByLcdaAsync(string lcdaCode)
        {
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Lcda is required");
            }

            return _sectorManager.Get(lcdaCode);
        }

        public async Task<ResponseModel> UpdateAsync(SectorModel sectorModel)
        {
            ResponseModel responseModel = await _sectorManager.UpdateAsync(sectorModel);
            if (responseModel.code == ResponseCode.NOTFOUND)
            {
                throw new NotFoundException(responseModel.description);
            }
            else if (responseModel.code != ResponseCode.SUCCESSFUL)
            {
                throw new BadRequestException(responseModel.description);
            }

            return responseModel;
        }
    }
}
