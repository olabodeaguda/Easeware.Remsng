using Easeware.Remsng.Common.Interfaces.Repositories;
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
    public class StreetRepository : IStreetRepository
    {
        private readonly RemsDbContext _context;
        public StreetRepository(RemsDbContext context)
        {
            _context = context;
        }

        public async Task<StreetModel> CreateStreet(StreetModel streetModel)
        {
            Street s = streetModel.Map();
            _context.Streets.Add(s);
            await _context.SaveChangesAsync();
            return s.Map();
        }

        public async Task<List<StreetModel>> Get(long wardId)
        {
            var result = await _context.Streets.Where(x => x.WardId == wardId).ToListAsync();
            return result.Select(x => x.Map()).ToList();
        }

        public async Task<List<StreetModel>> Get(string wardCode)
        {
            var result = await _context.Streets.Include(x => x.Ward).Where(x => x.Ward.WardCode == wardCode).ToListAsync();
            return result.Select(x => x.Map()).ToList();
        }

        public async Task<PageModel> Get(PageModel pageModel, long wardId)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _context.Streets.CountAsync();
            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }
            var result = await _context.Streets.OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).ToListAsync();

            var r = result.Select(x => x.Map()).ToArray();

            pageModel.Data = r.Count() > 0 ? r : new StreetModel[0];
            return pageModel;
        }

        public async Task<StreetModel> UpdateStreet(StreetModel streetModel)
        {
            Street s = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetModel.Id);

            if (s == null)
            {
                throw new Exception($"{streetModel.StreetName} does not exist");
            }
            s.StreetName = streetModel.StreetName;
            s.ModifiedBy = streetModel.ModifiedBy;
            s.ModifiedDate = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
            return s.Map();
        }

        public async Task<StreetModel> UpdateStreetStatus(StreetModel streetModel)
        {
            Street s = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetModel.Id);

            if (s == null)
            {
                throw new Exception($"{streetModel.StreetName} does not exist");
            }
            s.ModifiedBy = streetModel.ModifiedBy;
            s.ModifiedDate = DateTimeOffset.Now;
            s.StreetStatus = streetModel.StreetStatus.ToString();

            await _context.SaveChangesAsync();
            return s.Map();
        }


        public async Task<long> LastId()
        {
            var lastWard = await _context.Streets.LastOrDefaultAsync();
            if (lastWard == null)
            {
                return 0;
            }
            return lastWard.Id;
        }

    }
}
