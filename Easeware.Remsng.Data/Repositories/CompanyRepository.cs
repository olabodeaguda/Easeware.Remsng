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
    public class CompanyRepository : ICompanyRepository
    {
        private RemsDbContext _context;
        public CompanyRepository(RemsDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyModel> AddSync(CompanyModel companyModel)
        {
            Company company = companyModel.Map();
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.Map();
        }

        public async Task<List<CompanyModel>> GetAsync(string lcdaCode)
        {
            var company = await _context.Companies.Include(x => x.Lcda).Where(x => x.Lcda.LcdaCode == lcdaCode).ToListAsync();
            if (company.Count < 1)
            {
                return new List<CompanyModel>();
            }

            return company.Select(x => x.Map()).ToList();
        }

        public async Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel)
        {
            pageModel.TotalSize = await _context.Companies.Where(x => x.Lcda.LcdaCode == lcdaCode).CountAsync();
            if (pageModel.PageSize < 1)
            {
                return pageModel;
            }
            var cyps = await _context.Companies
                .Where(x => x.Lcda.LcdaCode == lcdaCode)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).Take(pageModel.PageSize)
                .ToArrayAsync();

            pageModel.Data = cyps.Count() > 0 ? cyps.Select(x => x.Map()).ToArray() : Array.Empty<object>();

            return pageModel;
        }

        public async Task<CompanyModel> GetAsync(long id)
        {
            Company company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return null;
            }

            return company.Map();
        }

        public async Task<long> LastId()
        {
            var lastCpy = await _context.Companies.LastOrDefaultAsync();
            if (lastCpy == null)
            {
                return 0;
            }
            return lastCpy.Id;
        }

        public async Task<CompanyModel> UpdateAsync(CompanyModel companyModel)
        {
            Company company = await _context.Companies.FindAsync(companyModel.Id);
            if (company == null)
            {
                throw new NotFoundException("Company does not exist");
            }
            company.CompanyName = companyModel.CompanyName;
            company.ModifiedBy = companyModel.ModifiedBy;
            company.ModifiedDate = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
            return company.Map();
        }

        public async Task<CompanyModel> UpdateStatusAsync(CompanyModel companyModel)
        {
            Company company = await _context.Companies.FindAsync(companyModel.Id);
            if (company == null)
            {
                throw new NotFoundException("Company does not exist");
            }
            company.Status = companyModel.Status.ToString();
            company.ModifiedBy = companyModel.ModifiedBy;
            company.ModifiedDate = DateTimeOffset.Now;
            await _context.SaveChangesAsync();
            return company.Map();
        }
    }
}
