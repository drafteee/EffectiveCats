using Microsoft.AspNetCore.Identity;

namespace Domain.Models.Account
{
    public class User : IdentityUser<long>, Interfaces.IId
    {
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
