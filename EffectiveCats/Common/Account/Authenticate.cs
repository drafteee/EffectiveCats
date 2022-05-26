using FluentValidation;
using Infrastructure;
using MediatR.Interfaces;
using MediatR.Models;
using Microsoft.Extensions.Logging;

namespace MediatR.Account
{
    public class Authenticate
    {
        public class Command : IRequest<AuthenticateResponse>
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, AuthenticateResponse>
        {
            private readonly IUserService _service;
            private readonly ILogger<Authenticate> _logger;
            private readonly UserAccessor _accessor;

            public Handler(ILogger<Authenticate> logger, IUserService service, UserAccessor accessor)
            {
                _service = service;
                _logger = logger;
                _accessor = accessor;
            }

            public Task<AuthenticateResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"Authentication {command.UserName} [{DateTime.Now}]");
                    return _service.Authenticate(command.UserName, command.Password, _accessor.GetCurrentUserIp());
                }
                catch (Exception e)
                {
                    _logger.LogError($"Authentication error {command.UserName} [{DateTime.Now}]");
                    throw e;
                }
            }
        }
    }
}
