﻿using DAL.Interfaces.Finder;
using DAL.Models.Account;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.Finders
{
    public class UserFinder : IUserFinder
    {
        private DbSet<User> _dbSet;

        public UserFinder(MainContext context)
        {
            _dbSet = context.Users;
        }

        public Task<List<User>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<User?> GetByName(string userName)
        {
            return _dbSet.FirstOrDefaultAsync(x=> x.UserName == userName);
        }
        public Task<User?> GetById(long id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<User?> GetByRefreshToken(string token)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.RefreshTokens.Any(t => t.Token == token));
        }

        public bool TokenIsUnique(string jwtToken)
        {
            return !_dbSet.Any(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
        }
    }
}
