using AutoMapper;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Implementations
{
    public class CompanyManager : ICompanyManager
    {
        private IMapper _mapper;
        private RemsDbContext _context;
        public CompanyManager(RemsDbContext context, IMapper mapper)
        {
            context = _context;
            _mapper = mapper;
        }

        public Task<int> AddSync(CompanyModel companyModel)
        {
            Company company = _mapper.Map<Company>(companyModel);
            _context.Companies.Add(company);
            return _context.SaveChangesAsync();
        }

        public async Task<List<CompanyModel>> GetAsync(string lcdaCode)
        {
            var company = await _context.Companies.Where(x => x.LcdaCode == lcdaCode).ToListAsync();
            if (company.Count < 1)
            {
                return new List<CompanyModel>();
            }

            return company.Select(x => _mapper.Map<CompanyModel>(x)).ToList();
        }

        public async Task<PageModel> GetAsync(string lcdaCode, PageModel pageModel)
        {
            pageModel.TotalSize = await _context.Companies.Where(x => x.LcdaCode == lcdaCode).CountAsync();
            if (pageModel.PageSize < 1)
            {
                return pageModel;
            }
            var cyps = await _context.Companies
                .Where(x => x.LcdaCode == lcdaCode)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).Take(pageModel.PageSize)
                .ToArrayAsync();

            pageModel.Data = cyps.Count() > 0 ? cyps : Array.Empty<object>();

            return pageModel;
        }

        public async Task<CompanyModel> GetAsync(long id)
        {
            Company company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return null;
            }

            return _mapper.Map<CompanyModel>(company);
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

        public async Task<int> UpdateAsync(CompanyModel companyModel)
        {
            Company company = await _context.Companies.FindAsync(companyModel.Id);
            company.CompanyName = companyModel.CompanyName;
            company.ModifiedBy = companyModel.ModifiedBy;
            company.ModifiedDate = DateTimeOffset.Now;
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateStatusAsync(CompanyModel companyModel)
        {
            Company company = await _context.Companies.FindAsync(companyModel.Id);
            company.Status = companyModel.CompanyName;
            company.ModifiedBy = companyModel.ModifiedBy;
            company.ModifiedDate = DateTimeOffset.Now;
            return await _context.SaveChangesAsync();
        }
    }
}
