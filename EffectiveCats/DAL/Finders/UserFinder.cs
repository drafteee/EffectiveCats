using DAL.Interfaces.Finder;
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

        public Task<List<User>> GetAllAsync()
        {
            return _dbSet.ToListAsync();
        }

        public Task<User?> GetAsync(Expression<Func<User, bool>> condition)
        {
            return _dbSet.FirstOrDefaultAsync(condition);
        }

        public bool TokenIsUnique(string jwtToken)
        {
            return !_dbSet.Any(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
        }
    }
}
