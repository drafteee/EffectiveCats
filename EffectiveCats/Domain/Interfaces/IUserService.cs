using Domain.Models;
using Domain.Models.Account;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(string userName, string password, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
        Task<User> GetById(long id);
    }
}
