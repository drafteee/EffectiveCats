using FluentValidation;
using MediatR.Services;
using Microsoft.Extensions.Logging;

namespace MediatR.MTypeCat
{
    public class DeleteCatType
    {
        public class Command : IRequest<bool>
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
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ICatTypeService _service;
            private readonly ILogger<DeleteCatType> _logger;

            public Handler(ILogger<DeleteCatType> logger, ICatTypeService service)
            {
                _logger = logger;
                _service = service;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"DeleteM Cat [{DateTime.Now}]");

                    return (await _service.Delete(command.Id)) > 0;
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
