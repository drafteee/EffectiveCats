using Domain.Models.Account;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {
        bool TokenIsUnique(string jwtToken);
    }
}
