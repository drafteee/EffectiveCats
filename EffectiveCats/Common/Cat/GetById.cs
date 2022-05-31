using AutoMapper;
using FluentValidation;
using MediatR.Cat.Responses;
using MediatR.Services;
using Microsoft.Extensions.Logging;
using Models = Domain.Entities;
using System.Text;
using System.Text.Json;

namespace MediatR.MCat
{
    public class GetByIdCat
    {
        public class Request : IRequest<GetByIdCatResponse>
        {
            public long Id { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Request, GetByIdCatResponse>
        {
            private readonly ILogger<GetByIdCat> _logger;
            private readonly ICatService _service;
            private readonly IMapper _mapper;

            public Handler(ILogger<GetByIdCat> logger, IMapper mapper, ICatService service)
            {
                _logger = logger;
                _service = service;
                _mapper = mapper;
            }

            public async Task<GetByIdCatResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadM Cat [{DateTime.Now}]");

                    var entity = await _service.Get(request.Id);
                    if (entity == null)
                    {
                        throw new ApplicationException($"Not found Cat id={request.Id}");
                    }
                    
                    return _mapper.Map<Domain.Entities.Cat, GetByIdCatResponse>(entity);
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ReadM error Cat [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
