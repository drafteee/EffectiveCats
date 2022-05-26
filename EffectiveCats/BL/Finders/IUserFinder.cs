using Domain.Entities.Account;

namespace BL.Finders
{
    public interface IUserFinder
    {
        Task<User?> GetByName(string userName);
        Task<User?> GetById(long id);
        Task<User?> GetByRefreshToken(string token);
        bool TokenIsUnique(string jwtToken);
    }
}
