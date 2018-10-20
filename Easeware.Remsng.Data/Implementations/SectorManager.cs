using AutoMapper;
using Easeware.Remsng.Common.Interfaces.Managers;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Common.Utilities;
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
    public class SectorManager : ISectorManager
    {
        private RemsDbContext _context;
        private IMapper _mapper;
        public SectorManager(RemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseModel> AddAsync(SectorModel sectorModel)
        {
            Sector sector = _mapper.Map<Sector>(sectorModel);
            _context.Sectors.Add(sector);
            int count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{sectorModel.SectorName} sector has been added successfully",
                    data = sector.Id
                };
            }

            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = $"An error occur while trying to create sector. " +
                $"Please try again or contact an administrator if error persist"
            };
        }

        public async Task<SectorModel> Get(long Id)
        {
            Sector sector = await _context.Sectors.FindAsync(Id);
            if (sector == null)
            {
                return null;
            }
            return _mapper.Map<SectorModel>(sector);
        }

        public async Task<List<SectorModel>> GetByLcdaAsync(string lcdaCode)
        {
            List<Sector> sectors = await _context.Sectors.OrderBy(x => x.SectorName).ToListAsync();

            if (sectors.Count < 1)
            {
                return new List<SectorModel>();
            }

            return sectors.Select(x => _mapper.Map<SectorModel>(x)).ToList();
        }

        public async Task<ResponseModel> UpdateAsync(SectorModel sectorModel)
        {
            Sector sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Id == sectorModel.Id);
            if (sector == null)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.NOTFOUND,
                    description = $"{sectorModel.SectorName} does not exist"
                };
            }

            sector.SectorCode = sectorModel.SectorCode;
            sector.SectorName = sectorModel.SectorName;

            int count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{sectorModel.SectorName} has been updated successfully"
                };
            }
            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = $"An error occur while trying to update sector. " +
                $"Please try again or contact an administrator if error persist"
            };
        }

       
    }
}
