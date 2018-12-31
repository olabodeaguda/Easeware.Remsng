using AutoMapper;
using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Repositories
{
    public class LcdaRepository : ILcdaRepository
    {
        RemsDbContext _context;
        private readonly IMapper _mapper;
        public LcdaRepository(IMapper mapper, RemsDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<LcdaModel> Add(LcdaModel lcdaModel)
        {
            Lcda lcda = lcdaModel.Map();
            _context.Lcdas.Add(lcda);
            int count = await _context.SaveChangesAsync();
            return lcda.Map();
        }

        public async Task<LcdaModel> Get(long id)
        {
            var r = await _context.Lcdas.FindAsync(id);
            if (r == null)
            {
                return null;
            }
            return r.Map();
        }

        public async Task<LcdaModel> Get(string lcdaCode)
        {
            var r = await _context.Lcdas.FirstOrDefaultAsync(x => x.LcdaCode == lcdaCode);
            if (r == null)
            {
                return null;
            }

            return r.Map();
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
            var result = await _context.Lcdas.OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).ToListAsync();

            var r = result.Select(x => x.Map()).ToArray();

            pageModel.Data = r.Count() > 0 ? r : new LcdaModel[0];
            return pageModel;
        }

        public async Task<LcdaModel> Update(LcdaModel lcdaModel)
        {
            var r = await _context.Lcdas.FindAsync(lcdaModel.Id);
            if (r == null)
            {
                throw new NotFoundException("Lcda does not exists");
            }

            r.LcdaName = lcdaModel.LcdaName;

            await _context.SaveChangesAsync();
            return r.Map();
        }

        public async Task<LcdaModel> GetByName(string lcdaName)
        {
            var r = await _context.Lcdas.FirstOrDefaultAsync(x => x.LcdaName == lcdaName);
            if (r == null)
            {
                return null;
            }
            return r.Map();
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
