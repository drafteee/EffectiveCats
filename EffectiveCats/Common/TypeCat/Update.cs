using AutoMapper;
using Domain.Models;
using MediatR.Services;
using Microsoft.Extensions.Logging;

namespace MediatR.MTypeCat
{
    public class UpdateCatType
    {
        public class Command : IRequest<bool>
        {
            public long Id { get; set; }
            public string? Name { get; set; }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ILogger<UpdateCatType> _logger;
            private readonly IMapper _mapper;
            private readonly ICatTypeService _service;

            public Handler(ILogger<UpdateCatType> logger, IMapper mapper, ICatTypeService service)
            {
                _logger = logger;
                _service = service;
                _mapper = mapper;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"UpdateM CatType [{DateTime.Now}]");

                    var entity = _mapper.Map<Command, CatType>(command);

                    return (await _service.Update(entity)) > 0;
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"UpdateM error CatType [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
