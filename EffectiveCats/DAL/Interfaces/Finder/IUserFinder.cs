using DAL.Models.Account;

namespace DAL.Interfaces.Finder
{
    public interface IUserFinder : IBaseFinder<User>
    {
        bool TokenIsUnique(string jwtToken);
    }
}
