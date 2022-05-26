using Domain.Entitties.Account;

namespace BL.Repositories
{
    public interface IUserRepository
    {
        void Edit(UserMongo entity);
        void Add(UserMongo entity);
        void AddRange(IEnumerable<UserMongo> entities);
        void Delete(UserMongo entity);
    }
}
