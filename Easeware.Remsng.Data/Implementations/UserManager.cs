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
    public class UserManager : IUserManager
    {
        private readonly RemsDbContext _remsDbContext;
        private readonly IMapper _mapper;
        public UserManager(RemsDbContext remsDbContext, IMapper mapper)
        {
            _remsDbContext = remsDbContext;
            _mapper = mapper;
        }

        public async Task<bool> Add(UserModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            _remsDbContext.Users.Add(user);
            int count = await _remsDbContext.SaveChangesAsync();
            return count > 0;
        }

        public async Task<UserModel> Get(string email)
        {
            var um = await _remsDbContext.Users.FirstOrDefaultAsync(x => x.email == email);
            if (um == null)
            {
                return null;
            }
            return _mapper.Map<UserModel>(um, opts =>
            {
                //opts.BeforeMap((src,dest)=> src.v)
            });
        }

        public async Task<UserModel> Get(long id)
        {
            var um = await _remsDbContext.Users.FindAsync(id);
            if (um == null)
            {
                return null;
            }
            return _mapper.Map<UserModel>(um, opts =>
            {
                //opts.BeforeMap((src,dest)=> src.v)
            });
        }

        public async Task<PageModel> Get(PageModel pageModel, long Lcdaid)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _remsDbContext.UserLcdas.CountAsync(x => x.LcdaId == Lcdaid);
            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }

            var result = await _remsDbContext.UserLcdas.Include(x => x.User)
                .Where(x => x.LcdaId == Lcdaid)
                .Select(x => x.User)
                .Distinct().ToListAsync();

            var r = result.OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).Select(x => _mapper
                .Map<UserModel>(x)).ToArray();

            pageModel.Data = r.Count() > 0 ? r : new UserModel[0];
            return pageModel;
        }

        public async Task<PageModel> Get(PageModel pageModel, string lcdaCode)
        {
            pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
            pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
            pageModel.TotalSize = await _remsDbContext.UserLcdas.Include(x => x.Lcda).Include(x => x.User)
                .Where(x => x.Lcda.LcdaCode == lcdaCode)
                .Select(x => x.User).Distinct().CountAsync();

            if (pageModel.TotalSize < 1)
            {
                return pageModel;
            }

            var result = await _remsDbContext.UserLcdas
                .Include(x => x.User)
                .Include(x => x.Lcda)
                .Where(x => x.Lcda.LcdaCode == lcdaCode)
                .Select(x => x.User)
                .Distinct().ToListAsync();

            var r = result.OrderByDescending(x => x.CreatedDate)
                .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
                Take(pageModel.PageSize).Select(x => _mapper
                .Map<UserModel>(x)).ToArray();

            pageModel.Data = r.Count() > 0 ? r : new UserModel[0];
            return pageModel;
        }

        public async Task<bool> UpdateStatus(UserModel userModel)
        {
            User um = await _remsDbContext.Users.FindAsync(userModel.id);
            if (um == null)
            {
                return false;
            }
            um.userStatus = userModel.userStatus.ToString();
            int count = await _remsDbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}
