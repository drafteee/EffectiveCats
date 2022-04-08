using System.Text.Json.Serialization;

namespace Domain.Models.Account
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            UserName = user.UserName;
            JwtToken = $"Bearer {jwtToken}";
            RefreshToken = refreshToken;
        }
    }
}
