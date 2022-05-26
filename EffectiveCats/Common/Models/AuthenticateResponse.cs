using System.Text.Json.Serialization;

namespace MediatR.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        public AuthenticateResponse(long id, string userName, string jwtToken, string refreshToken)
        {
            Id = id;
            UserName = userName;
            JwtToken = $"Bearer {jwtToken}";
            RefreshToken = refreshToken;
        }
    }
}
