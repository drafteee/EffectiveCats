using AutoMapper;
using MediatR.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models = Domain.Models;

namespace MediatR.MCat
{
    public class UpdateCat
    {
        public class Command : IRequest<bool>
        {
            public long Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public IFormFile? Image { get; set; }
            public int? TypeId { get; set; }
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly ILogger<UpdateCat> _logger;
            private readonly IMapper _mapper;
            private readonly ICatService _service;

            public Handler(ILogger<UpdateCat> logger, IMapper mapper, ICatService service)
            {
                _logger = logger;
                _service = service;
                _mapper = mapper;
            }

            public async Task<bool> Handle(Command command, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"UpdateM Cat [{DateTime.Now}]");

                    var entity = _mapper.Map<Command, Models.Cat>(command);

                    return (await _service.Update(entity)) > 0;
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"UpdateM error Cat [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
