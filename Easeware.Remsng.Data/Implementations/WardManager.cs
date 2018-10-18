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
    public class WardManager : IWardManager
    {
        private IMapper _mapper;
        private RemsDbContext _context;
        public WardManager(RemsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseModel> AddAsync(WardModel wardModel)
        {
            Ward ward = _mapper.Map<Ward>(wardModel);
            _context.Wards.Add(ward);
            int count = await _context.SaveChangesAsync();

            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{wardModel.WardName} has been updated successfully"
                };
            }
            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = "Update was not successful. Please try again or contact an administrator, if error persist"
            };
        }

        public async Task<PageModel> GetAsync(PageModel pageModel, string lcdaCode)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _context.Wards.Where(x => x.LcdaCode == lcdaCode).CountAsync();
            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }
            var result = await _context.Wards.Where(x => x.LcdaCode == lcdaCode).OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).ToListAsync();

            var r = result.Select(x => _mapper.Map<WardModel>(x)).ToArray();

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

            return _mapper.Map<WardModel>(ward);
        }

        public async Task<WardModel> GetByIdAsync(long wardId)
        {
            var ward = await _context.Wards.FindAsync(wardId);
            if (ward == null)
            {
                return null;
            }

            return _mapper.Map<WardModel>(ward);
        }

        public async Task<List<WardModel>> GetByLcdaAsync(string lcdaCode)
        {
            var wards = await _context.Wards.Where(x => x.LcdaCode == lcdaCode).ToListAsync();
            if (wards.Count < 1)
            {
                return new List<WardModel>();
            }

            return wards.Select(x => _mapper.Map<WardModel>(x)).ToList();
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

        public async Task<ResponseModel> UpdateAsync(WardModel wardModel)
        {
            var ward = await _context.Wards.FindAsync(wardModel.Id);
            if (ward == null)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.NOTFOUND,
                    description = "Ward can not be found"
                };
            }
            ward.WardName = wardModel.WardName;
            ward.ModifiedBy = wardModel.ModifiedBy;
            ward.ModifiedDate = DateTimeOffset.Now;

            int count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = "Update is successful"
                };
            }
            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = "No changes occurred"
            };
        }

        public async Task<ResponseModel> UpdateStatusAsync(WardModel wardModel)
        {
            var ward = await _context.Wards.FindAsync(wardModel.Id);
            if (ward == null)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.NOTFOUND,
                    description = "Ward can not be found"
                };
            }

            ward.Status = wardModel.Status.ToString();
            ward.ModifiedBy = wardModel.ModifiedBy;
            ward.ModifiedDate = DateTimeOffset.Now;
            int count = await _context.SaveChangesAsync();
            if (count > 0)
            {
                return new ResponseModel()
                {
                    code = ResponseCode.SUCCESSFULL,
                    description = $"{wardModel.WardName} has been updated successfully"
                };
            }
            return new ResponseModel()
            {
                code = ResponseCode.FAIL,
                description = "Update was not successful. Please try again or contact an administrator, if error persist"
            };
        }
    }
}
