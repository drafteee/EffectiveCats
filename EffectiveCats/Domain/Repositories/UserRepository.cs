using Domain.Models.Account;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public class UserRepository : GenericIdRepository<User>
    {
        public UserRepository(MainContext context) : base(context) { }

        public bool Any(Expression<Func<User, bool>> predicate)
        {
            return DbContext.Set<User>().Any(predicate);
        }
    }
}