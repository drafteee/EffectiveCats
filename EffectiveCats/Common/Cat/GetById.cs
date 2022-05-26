using AutoMapper;
using FluentValidation;
using MediatR.Cat.Responses;
using MediatR.Services;
using StackExchange.Redis;
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
            private readonly IDatabase _cache;

            public Handler(ILogger<GetByIdCat> logger, IMapper mapper, ICatService service, IDatabase cache)
            {
                _logger = logger;
                _service = service;
                _mapper = mapper;
                _cache = cache;
            }

            public async Task<GetByIdCatResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadM Cat [{DateTime.Now}]");

                    byte[] cache = _cache.StringGet($"CatGet{request.Id}");

                    if (cache != null)
                    {
                        var cachedDataString = Encoding.UTF8.GetString(cache);
                        return JsonSerializer.Deserialize<GetByIdCatResponse>(cachedDataString);
                    }

                    var entity = await _service.Get(request.Id);
                    if (entity == null)
                    {
                        throw new ApplicationException($"Not found Cat id={request.Id}");
                    }

                    var mappedEntity = _mapper.Map<Domain.Entities.Cat, GetByIdCatResponse>(entity);
                    string cachedData = JsonSerializer.Serialize(new WeakReference(mappedEntity));
                    var dataToCache =  Encoding.UTF8.GetBytes(cachedData);

                    _cache.StringSet($"CatGet{request.Id}", dataToCache);

                    return mappedEntity;
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
