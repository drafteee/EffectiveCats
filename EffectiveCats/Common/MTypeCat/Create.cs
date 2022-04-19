using AutoMapper;
using BL.Services;
using DAL.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class CreateCatType
    {
        public class Command : IRequest<long>
        {
            public string Name { get; set; }
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
            private readonly ILogger<CreateCatType> _logger;
            private readonly ICatTypeService _service;

            public Handler(ILogger<CreateCatType> logger, IMapper mapper, ICatTypeService service)
            {
                _logger = logger;
                _mapper = mapper;
                _service = service;
            }

            public async Task<long> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"CreateM CatType [{DateTime.Now}]");

                    var entity = _mapper.Map<Command, CatType>(command);
                    await _service.Create(entity);
                    return entity.Id;
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"CreateM error CatType [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
