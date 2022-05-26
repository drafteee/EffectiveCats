using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Account
{
    public class User : IdentityUser<long>
    {
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
