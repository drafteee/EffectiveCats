using Domain.Interfaces;
using Domain.Models.Account;
using Infrastructure;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.Account
{
    public class RefreshToken
    {
        public class Command : IRequest<AuthenticateResponse> { }

        public class Handler : IRequestHandler<Command, AuthenticateResponse>
        {
            private readonly IUserService _service;
            private readonly ILogger<RefreshToken> _logger;
            private readonly UserAccessor _accessor;

            public Handler(ILogger<RefreshToken> logger, IUserService service, UserAccessor accessor)
            {
                _service = service;
                _logger = logger;
                _accessor = accessor;
            }

            public async Task<AuthenticateResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    var userId = long.Parse(_accessor.GetCurrentUserId());

                    _logger.LogInformation($"RefreshToken userId={userId} [{DateTime.Now}]");

                    var user = await _service.GetById(userId);
                    var refreshToken = user.RefreshTokens.FirstOrDefault(x=> x.Id == userId && x.IsActive);
                    if (refreshToken == null)
                    {
                        throw new KeyNotFoundException("Invalid Token");
                    }
                    return await _service.RefreshToken(refreshToken.Token, _accessor.GetCurrentUserIp());
                }
                catch (Exception e)
                {
                    _logger.LogError($"RefreshToken error [{DateTime.Now}]");
                    throw e;
                }
            }
        }
    }
}
