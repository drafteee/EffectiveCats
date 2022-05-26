using Domain.Entities.Account;
using Domain.Entitties.Account;

namespace BL.Finders
{
    public interface IUserMongoFinder
    {
        Task<UserMongo> GetByName(string userName);
        Task<UserMongo> GetById(long id);
        Task<UserMongo> GetByRefreshToken(string token);
        bool TokenIsUnique(string jwtToken);
    }
}
