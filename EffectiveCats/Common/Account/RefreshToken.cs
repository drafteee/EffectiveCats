using Domain.Models.Account;
using Infrastructure;
using MediatR.Interfaces;
using Microsoft.Extensions.Logging;

namespace MediatR.Account
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

            public Task<AuthenticateResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    var userId = long.Parse(_accessor.GetCurrentUserId());

                    _logger.LogInformation($"RefreshToken userId={userId} [{DateTime.Now}]");
                    
                    return _service.RefreshToken(userId, _accessor.GetCurrentUserIp());
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
