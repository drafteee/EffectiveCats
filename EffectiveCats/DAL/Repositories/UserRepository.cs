using DAL.Interfaces.Finders;
using DAL.Interfaces.Repositories;
using DAL.Models.Account;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User, long>, IUserRepository
    {
        public UserRepository(MainContext context) : base(context) { }


    }
}