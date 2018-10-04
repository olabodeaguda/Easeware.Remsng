using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Easeware.Remsng.API.Utilities;
using Easeware.Remsng.Common.Enums;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Services;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Easeware.Remsng.API.Controllers
{
    [ModelValidation]
    [Route("api/v2.0/lcda")]
    public class LcdaController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILcdaService _lcdaService;
        private readonly ICodeGeneratorService _codeGenerationService;
        private readonly IUserLcdaService _userLcdaService;
        public LcdaController(ILcdaService lcdaService,
            ICodeGeneratorService codeGeneratorService,
            IUserService userService,
            IUserLcdaService userLcdaService)
        {
            _lcdaService = lcdaService;
            _codeGenerationService = codeGeneratorService;
            _userService = userService;
            _userLcdaService = userLcdaService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LcdaModel lcdaModel)
        {
            long lastId = await _lcdaService.LastId();
            lcdaModel.LcdaCode = _codeGenerationService.NewLcdaCode(lastId);
            bool result = await _lcdaService.Add(lcdaModel);
            if (result)
            {
                // add current user to the lcda
                LcdaModel lModel = await _lcdaService.Get(lcdaModel.LcdaCode);
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    data = lModel.Id,
                    description = $"{lcdaModel.LcdaName} has been created successfully"
                });
            }

            throw new BadRequestException("An error occur while processing your request. Please try again or contact an administrator");
        }
        [Route("assignuser")]
        [HttpPost]
        public async Task<IActionResult> AssignLcdaToUser([FromBody]UserLcdaModel userLcdaModel)
        {
            UserModel userModel = await _userService.Get(userLcdaModel.UserId);
            if (userModel == null)
            {
                throw new BadRequestException("User does not exist");
            }
            if (userModel.userStatus != UserStatus.ACTIVE)
            {
                throw new BadRequestException("User is not active");
            }
            LcdaModel lcdaModel = await _lcdaService.Get(userLcdaModel.LcdaId);
            if (lcdaModel == null)
            {
                throw new BadRequestException("Lcda does not exist");
            }
            if (lcdaModel.LcdaStatus != LcdaStatus.ACTIVE)
            {
                throw new BadRequestException("LCDA is not active");
            }

            bool result = await _userLcdaService.Add(userLcdaModel);
            if (result)
            {
                return Ok(new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = "Request is successful"
                });
            }
            throw new UnknownException("An error occured while trying to assign user to lcda");
        }
    }
}