using Domain.Interfaces.Finders;
using Domain.Interfaces.Repositories;
using Domain.Models.Account;

namespace Domain.Repositories
{
    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public UserRepository(MainContext context, IFinder<User, long> finder) : base(context, finder) { }

        public bool TokenIsUnique(string jwtToken)
        {
            return !DbContext.Any(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
        }
    }
}