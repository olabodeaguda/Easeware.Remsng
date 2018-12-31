using AutoMapper;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Repositories
{
    public class WardRepository : IWardRepository
    {
        private IMapper _mapper;
        private RemsDbContext _context;
        public WardRepository(RemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WardModel> AddAsync(WardModel wardModel)
        {
            Ward ward = wardModel.Map();
            _context.Wards.Add(ward);
            await _context.SaveChangesAsync();
            return ward.Map();
        }

        public async Task<PageModel> GetAsync(PageModel pageModel, string lcdaCode)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _context.Wards.Include(x => x.Lcda)
                .Where(x => x.Lcda.LcdaCode == lcdaCode).CountAsync();
            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }
            var result = await _context.Wards
                .Include(x => x.Lcda)
                .Where(x => x.Lcda.LcdaCode == lcdaCode).OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).ToListAsync();

            var r = result.Select(x => x.Map()).ToArray();

            pageModel.Data = r.Count() > 0 ? r : new WardModel[0];
            return pageModel;
        }

        public async Task<WardModel> GetAsync(string wardName)
        {
            Ward ward = await _context.Wards.FirstOrDefaultAsync(x => x.WardName.ToLower() == wardName.ToLower());
            if (ward == null)
            {
                return null;
            }

            return ward.Map();
        }

        public async Task<WardModel> GetAsync(string wardName, long lcdaId)
        {
            Ward ward = await _context.Wards.FirstOrDefaultAsync(x => x.WardName.ToLower() == wardName.ToLower() && x.LcdaId == lcdaId);
            if (ward == null)
            {
                return null;
            }

            return ward.Map();
        }
        public async Task<WardModel> GetByIdAsync(long wardId)
        {
            var ward = await _context.Wards.FindAsync(wardId);
            if (ward == null)
            {
                return null;
            }

            return ward.Map();
        }

        public async Task<List<WardModel>> GetByLcdaAsync(string lcdaCode)
        {
            var wards = await _context.Wards.Include(x => x.Lcda)
                .Where(x => x.Lcda.LcdaCode == lcdaCode).ToListAsync();
            if (wards.Count < 1)
            {
                return new List<WardModel>();
            }

            return wards.Select(x => x.Map()).ToList();
        }

        public async Task<long> LastId()
        {
            var lastWard = await _context.Wards.LastOrDefaultAsync();
            if (lastWard == null)
            {
                return 0;
            }
            return lastWard.Id;
        }

        public async Task<WardModel> UpdateAsync(WardModel wardModel)
        {
            var ward = await _context.Wards.FindAsync(wardModel.Id);
            if (ward == null)
            {
                throw new NotFoundException($"{wardModel.WardName} does not exist");
            }
            ward.WardName = wardModel.WardName;
            ward.ModifiedBy = wardModel.ModifiedBy;
            ward.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
            return ward.Map();
        }

        public async Task<WardModel> UpdateStatusAsync(WardModel wardModel)
        {
            var ward = await _context.Wards.FindAsync(wardModel.Id);
            if (ward == null)
            {
                throw new NotFoundException($"{wardModel.WardName} does not exist");
            }

            ward.Status = wardModel.Status.ToString();
            ward.ModifiedBy = wardModel.ModifiedBy;
            ward.ModifiedDate = DateTimeOffset.Now;
            await _context.SaveChangesAsync();

            return ward.Map();
        }
    }
}
