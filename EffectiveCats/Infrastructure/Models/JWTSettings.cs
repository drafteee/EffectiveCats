namespace Infrastructure.Models
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
    }
}
