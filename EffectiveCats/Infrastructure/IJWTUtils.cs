namespace Infrastructure
{
    public interface IJWTUtils
    {
        public string GenerateJwtToken(string id);
        public long? ValidateJwtToken(string token);
        public string GenerateRefreshToken();
    }
}
