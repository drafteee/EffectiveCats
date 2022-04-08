using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure
{
    public class UserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId() => _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "id")?.Value;
        public string GetCurrentUserIp() => _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }
}
