using Easeware.Remsng.Common.Exceptions;
using Easeware.Remsng.Common.Interfaces.Repositories;
using Easeware.Remsng.Common.Models;
using Easeware.Remsng.Entities;
using Easeware.Remsng.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Easeware.Remsng.Data.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private RemsDbContext _context;
        public AddressRepository(RemsDbContext context)
        {
            _context = context;
        }

        public async Task<AddressModel> CreateAddress(AddressModel model)
        {
            Address address = model.Map();
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return address.Map();
        }

        public async Task<AddressModel> Get(long id)
        {
            var result = await _context.Addresses.FindAsync(id);
            return result.Map();
        }

        public async Task<AddressModel> UpdateAddress(AddressModel model)
        {
            var entity = await _context.Addresses.FindAsync(model.Id);
            if (entity == null)
            {
                throw new BadRequestException("Address does not exist");
            }
            entity.HouseNumber = model.HouseNumber;
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = model.ModifiedDate;
            entity.StreetId = model.StreetId;

            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<AddressModel> UpdateAddressStatus(AddressModel model)
        {
            var entity = await _context.Addresses.FindAsync(model.Id);
            if (entity == null)
            {
                throw new BadRequestException("Address does not exist");
            }
            entity.ModifiedBy = model.ModifiedBy;
            entity.ModifiedDate = model.ModifiedDate;
            entity.Status = model.Status.ToString();

            await _context.SaveChangesAsync();
            return entity.Map();
        }

        public async Task<List<AddressModel>> GetByLcda(long lcdaId)
        {
            var result = await _context.Taxpayers
                .Include(x => x.Company)
                .Include(x => x.Address)
                .Where(x => x.Company.LcdaId == lcdaId)
                .Select(x => x.Address.Map()).ToListAsync();

            return result;
        }
    }
}
