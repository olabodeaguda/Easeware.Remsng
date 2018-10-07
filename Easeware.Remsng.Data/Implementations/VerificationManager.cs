using AutoMapper;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Implementations
{
    public class VerificationManager : IVerificationManager
    {
        private readonly RemsDbContext _context;
        private readonly IMapper _mapper;
        public VerificationManager(RemsDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> Add(VerificationDetailModel verificationDetailModel)
        {
            VerificationDetail verificationDetail = _mapper.Map<VerificationDetail>(verificationDetailModel);
            _context.VerificationDetails.Add(verificationDetail);
            int count = await _context.SaveChangesAsync();
            return count > 0;
        }

        public async Task<VerificationDetailModel> Get(string verificationCode)
        {
            var vCode = await _context.VerificationDetails.FirstOrDefaultAsync(x => x.VerificationCode == verificationCode);
            if (vCode == null)
            {
                return null;
            }
            return _mapper.Map<VerificationDetailModel>(vCode);
        }

        public async Task<bool> InValidateCode(string verificationCode, string ownerId)
        {
            var vCode = await _context.VerificationDetails
                .FirstOrDefaultAsync(x => x.VerificationCode == verificationCode && x.OwnerId == ownerId);
            if (vCode == null)
            {
                return false;
            }
            vCode.IsVerified = true;
            int count = await _context.SaveChangesAsync();
            return count > 0;
        }
    }
}
