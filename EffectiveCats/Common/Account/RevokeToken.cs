using FluentValidation;
using Infrastructure;
using MediatR.Interfaces;
using Microsoft.Extensions.Logging;

namespace MediatR.Account
{
    public class RevokeToken
    {
        public class Command : IRequest<bool>
        { 
            public string RefreshToken { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.RefreshToken).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IUserService _service;
            private readonly ILogger<RevokeToken> _logger;
            private readonly UserAccessor _accessor;

            public Handler(ILogger<RevokeToken> logger, IUserService service, UserAccessor accessor)
            {
                _logger = logger;
                _service = service;
                _accessor = accessor;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"RevokeToken {command.RefreshToken} [{DateTime.Now}]");

                    return await _service.RevokeToken(command.RefreshToken, _accessor.GetCurrentUserIp());
                }
                catch (Exception e)
                {
                    _logger.LogError($"RevokeToken error {command.RefreshToken} [{DateTime.Now}]");
                    throw e;
                }
            }
        }
    }
}
