using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/ward")]
    [ApiController]
    public class WardController : ControllerBase
    {
        private readonly IWardService _wardService;
        private readonly ICodeGeneratorService _codeGeneratorService;
        public WardController(IWardService wardService, ICodeGeneratorService codeGeneratorService)
        {
            _wardService = wardService;
            _codeGeneratorService = codeGeneratorService;
        }

        [HttpGet("{lcdaCode}/{pageNumber}/{pageSize}")]
        public async Task<IActionResult> Get(string lcdaCode, int pageNumber = 1, int pageSize = 20)
        {
            PageModel pageModel = new PageModel()
            {
                PageNumber = pageNumber < 1 ? 1 : pageNumber,
                PageSize = pageSize < 1 ? 20 : pageSize
            };
            if (string.IsNullOrEmpty(lcdaCode))
            {
                throw new BadRequestException("Lcda is required!!!");
            }
            pageModel = await _wardService.GetAsync(pageModel, lcdaCode);
            return Ok(pageModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WardModel wardModel)
        {
            WardModel wModel = await _wardService.GetAsync(wardModel.WardName);
            if (wModel != null)
            {
                throw new BadRequestException($"{wardModel.WardName} already exist");
            }
            long wardid = await _wardService.LastId();
            wardModel.WardCode = _codeGeneratorService.NewCode(wardid, "WRD");

            ResponseModel responseModel = await _wardService.AddAsync(wardModel);

            if (responseModel.code == ResponseCode.SUCCESSFULL)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(responseModel);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] WardModel wardModel, long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wardService.GetByIdAsync(id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward can not be found");
            }

            if (wModel.Status == WardStatus.NOT_ACTIVE)
            {
                throw new BadRequestException("Ward can not be updated because status is not active");
            }

            wardModel.Id = id;
            ResponseModel responseModel = await _wardService.UpdateAsync(wardModel);
            if (responseModel.code == ResponseCode.SUCCESSFULL)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(responseModel);
            }
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wardService.GetByIdAsync(id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward can not be found");
            }

            if (wModel.Status == WardStatus.NOT_ACTIVE)
            {
                throw new BadRequestException("Ward is already not active");
            }

            wModel.Id = id;
            wModel.Status = WardStatus.NOT_ACTIVE;
            ResponseModel responseModel = await _wardService.UpdateStatusAsync(wModel);
            if (responseModel.code == ResponseCode.SUCCESSFULL)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(responseModel);
            }
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(long id)
        {
            if (id == default(long))
            {
                throw new BadRequestException("Ward is required!!!");
            }

            WardModel wModel = await _wardService.GetByIdAsync(id);
            if (wModel == null)
            {
                throw new NotFoundException("Selected ward can not be found");
            }

            if (wModel.Status == WardStatus.ACTIVE)
            {
                throw new BadRequestException("Ward is already active");
            }

            wModel.Id = id;
            wModel.Status = WardStatus.ACTIVE;
            ResponseModel responseModel = await _wardService.UpdateStatusAsync(wModel);
            if (responseModel.code == ResponseCode.SUCCESSFULL)
            {
                return Ok(responseModel);
            }
            else
            {
                return BadRequest(responseModel);
            }
        }
    }
}