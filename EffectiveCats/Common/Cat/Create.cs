using AutoMapper;
using FluentValidation;
using MediatR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediatR.MCat
{
    public class CreateCat 
    {
        public class Command : IRequest<long>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public IFormFile Image { get; set; }
            public int TypeId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).Length(5, 10);
            }
        }

        public class Handler : IRequestHandler<Command, long>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<CreateCat> _logger;
            private readonly ICatService _service;

            public Handler(ILogger<CreateCat> logger, IMapper mapper, ICatService service)
            {
                _logger = logger;
                _mapper = mapper;
                _service = service;
            }

            public async Task<long> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"CreateM Cat [{DateTime.Now}]");

                    var entity = _mapper.Map<Command, Domain.Entities.Cat>(command);
                    await _service.Create(entity);
                    return entity.Id;
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"CreateM error Cat [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
