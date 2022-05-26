using FluentValidation;
using MediatR.Services;
using Microsoft.Extensions.Logging;

namespace MediatR.MCat
{
    public class DeleteCat
    {
        public class Command : IRequest
        {
            public long Id { get ; set ; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICatService _service;
            private readonly ILogger<DeleteCat> _logger;

            public Handler(ILogger<DeleteCat> logger, ICatService service)
            {
                _logger = logger;
                _service = service;
            }

            public async Task<Unit> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"DeleteM Cat [{DateTime.Now}]");

                    await _service.Delete(command.Id);

                    return Unit.Value;
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"DeleteM error Cat [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
