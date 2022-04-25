namespace Domain.Models.Account
{
    public class RefreshToken : BaseEntity<long>
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }
        public string? ReasonRevoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsRevoked => Revoked != null;
        public bool IsActive => !IsRevoked && !IsExpired;

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class RevokeTokenRequest
    {
        public string Token { get; set; }
    }
}