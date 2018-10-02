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
    public class LcdaManager : ILcdaManager
    {
        RemsDbContext _context;
        private readonly IMapper _mapper;
        public LcdaManager(IMapper mapper, RemsDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<bool> Add(LcdaModel lcdaModel)
        {
            Lcda lcda = _mapper.Map<Lcda>(lcdaModel);
            _context.Lcdas.Add(lcda);
            int count = await _context.SaveChangesAsync();
            return count > 0;
        }

        public async Task<LcdaModel> Get(long id)
        {
            var r = await _context.Lcdas.FindAsync(id);
            if (r == null)
            {
                return null;
            }

            LcdaModel lcdaModel = _mapper.Map<LcdaModel>(r);
            return lcdaModel;
        }

        public async Task<LcdaModel> Get(string lcdaCode)
        {
            var r = await _context.Lcdas.FirstOrDefaultAsync(x => x.LcdaCode == lcdaCode);
            if (r == null)
            {
                return null;
            }

            LcdaModel lcdaModel = _mapper.Map<LcdaModel>(r);
            return lcdaModel;
        }

        public async Task<PageModel> Get(PageModel pageModel)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _context.Lcdas.CountAsync();
            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }
            var result = await _context.Lcdas.OrderByDescending(x => x.CreatedBy)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).ToListAsync();

            pageModel.Data = result.Count() > 0 ? _mapper.Map<List<LcdaModel>>(result).ToArray() : new LcdaModel[0];
            return pageModel;
        }

        public async Task<bool> Update(LcdaModel lcdaModel)
        {
            var r = await _context.Lcdas.FindAsync(lcdaModel.Id);
            if (r == null)
            {
                return false;
            }
            r.LcdaCode = lcdaModel.LcdaCode;
            r.LcdaName = lcdaModel.LcdaName;

            int count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<LcdaModel> GetByName(string lcdaName)
        {
            var r = await _context.Lcdas.FirstOrDefaultAsync(x => x.LcdaName == lcdaName);
            if (r == null)
            {
                return null;
            }

            LcdaModel lcdaModel = _mapper.Map<LcdaModel>(r);
            return lcdaModel;
        }

        public async Task<long> LastId()
        {
            var lastLcda = await _context.Lcdas.LastOrDefaultAsync();
            if (lastLcda == null)
            {
                return 0;
            }
            return lastLcda.Id;
        }
    }
}
