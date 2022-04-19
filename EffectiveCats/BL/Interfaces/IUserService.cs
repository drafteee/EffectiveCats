using DAL.Models.Account;

namespace BL.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(string userName, string password, string ipAddress);
        Task<AuthenticateResponse> RefreshToken(long id, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
    }
}
