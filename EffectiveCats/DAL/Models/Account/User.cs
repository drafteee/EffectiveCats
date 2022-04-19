using Microsoft.AspNetCore.Identity;

namespace DAL.Models.Account
{
    public class User : IdentityUser<long>, Interfaces.IId<long>
    {
        public virtual List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
