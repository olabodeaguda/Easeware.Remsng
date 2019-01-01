﻿using AutoMapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly RemsDbContext _remsDbContext;
        private readonly IMapper _mapper;
        public UserRepository(RemsDbContext remsDbContext, IMapper mapper)
        {
            _remsDbContext = remsDbContext;
            _mapper = mapper;
        }

        public async Task<UserModel> Add(UserModel userModel)
        {
            User user = _mapper.Map<User>(userModel);
            _remsDbContext.Users.Add(user);
            await _remsDbContext.SaveChangesAsync();
            return user.Map();
        }

        public async Task<UserModel> Get(string email)
        {
            var um = await _remsDbContext.Users.FirstOrDefaultAsync(x => x.email == email);
            if (um == null)
            {
                return null;
            }
            return um.Map();
        }

        public async Task<UserModel> Get(long id)
        {
            var um = await _remsDbContext.Users.FindAsync(id);
            if (um == null)
            {
                return null;
            }
            return um.Map();
        }

        //public async Task<PageModel> Get(PageModel pageModel, string LcdaCode)
        //{
        //    pageModel.PageNumber = pageModel.PageNumber < 1 ? 1 : pageModel.PageNumber;
        //    pageModel.PageSize = pageModel.PageSize < 1 ? 20 : pageModel.PageSize;
        //    pageModel.TotalSize = await _remsDbContext.UserLcdas.CountAsync(x => x.LcdaCode == LcdaCode);
        //    if (pageModel.TotalSize < 1)
        //    {
        //        return pageModel;
        //    }

        //    var result = await _remsDbContext.UserLcdas.Include(x => x.User)
        //        .Where(x => x.LcdaCode == LcdaCode)
        //        .Select(x => x.User)
        //        .Distinct().ToListAsync();

        //    var r = result.OrderByDescending(x => x.CreatedDate)
        //        .Skip((pageModel.PageNumber - 1) * pageModel.PageSize).
        //        Take(pageModel.PageSize).Select(x => _mapper
        //        .Map<UserModel>(x)).ToArray();

        //    pageModel.Data = r.Count() > 0 ? r : new UserModel[0];
        //    return pageModel;
        //}

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
                Take(pageModel.PageSize).Select(x => x.Map()).ToArray();

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

        public async Task<bool> ChangePassword(UserModel uModel)
        {
            User userModel = await _remsDbContext.Users.FindAsync(uModel.id);
            if (userModel == null)
            {
                return false;
            }
            userModel.passwordHash = uModel.passwordHash;
            uModel.ModifiedBy = uModel.ModifiedBy;
            uModel.ModifiedDate = DateTimeOffset.Now;

            int count = await _remsDbContext.SaveChangesAsync();
            return count > 0;
        }
    }
}