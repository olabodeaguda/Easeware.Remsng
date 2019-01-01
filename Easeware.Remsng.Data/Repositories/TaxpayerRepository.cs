using Easeware.Remsng.Common.Exceptions;
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
    public class TaxpayerRepository : ITaxpayerRepository
    {
        private RemsDbContext _context;

        public TaxpayerRepository(RemsDbContext context)
        {
            _context = context;
        }

        public async Task<TaxpayerModel> CreateTaxpayer(TaxpayerModel model)
        {
            Taxpayer entity = model.Map();
            _context.Taxpayers.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<List<TaxpayerModel>> GetAsync(long streetId)
        {
            var res = await _context.Taxpayers
                .Include(x => x.Address)
                .Where(x => x.Address.StreetId == streetId).ToListAsync();

            return res.Select(x => x.Map()).ToList();
        }

        public async Task<PageModel> Get(long lcdaId, PageModel pageModel)
        {
            pageModel.TotalSize = await _context.Taxpayers
                .Include(c => c.Company)
                .Include(d => d.Company.Lcda).Where(x => x.Company.Lcda.Id == lcdaId).CountAsync();
            if (pageModel.PageSize < 1)
            {
                return pageModel;
            }
            var cyps = await _context.Taxpayers
                 .Include(c => c.Company)
                .Include(d => d.Company.Lcda).Where(x => x.Company.Lcda.Id == lcdaId)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).Take(pageModel.PageSize)
                .ToArrayAsync();

            pageModel.Data = cyps.Count() > 0 ? cyps.Select(x => x.Map()).ToArray() : Array.Empty<object>();

            return pageModel;
        }

        public async Task<List<TaxpayerModel>> GetByLcda(long lcdaId)
        {
            var cyps = await _context.Taxpayers
                .Include(c => c.Company)
               .Include(d => d.Company.Lcda).Where(x => x.Company.Lcda.Id == lcdaId).ToListAsync();

            return cyps.Select(x => x.Map()).ToList();
        }

        public async Task<TaxpayerModel> UpdateStatus(TaxpayerModel tm)
        {
            var result = await _context.Taxpayers.FirstOrDefaultAsync(x => x.Id == tm.Id);
            if (result == null)
            {
                throw new NotFoundException("Taxpayer can not be found");
            }
            result.Status = tm.Status.ToString();
            await _context.SaveChangesAsync();
            return result.Map();
        }

        public async Task<TaxpayerModel> UpdateTaxpayer(TaxpayerModel model)
        {
            var result = await _context.Taxpayers.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (result == null)
            {
                throw new NotFoundException("Taxpayer can not be found");
            }
            result.AddressId = model.AddressId;
            result.CompanyId = model.CompanyId;
            result.LastName = model.LastName;
            result.ModifiedBy = model.ModifiedBy;
            result.ModifiedDate = DateTimeOffset.Now;
            result.OtherNames = model.OtherNames;
            result.TaxCategory = model.TaxCategory.ToString();

            await _context.SaveChangesAsync();
            return result.Map();
        }

    }
}
