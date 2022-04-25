using AutoMapper;
using MediatR.Cat.Responses;
using MediatR.Services;
using Microsoft.Extensions.Logging;
using Models = Domain.Models;

namespace MediatR.MCat
{
    public class GetAllCat
    {
        public class Request : IRequest<List<GetAllCatResponse>> { }

        public class Handler: IRequestHandler<Request, List<GetAllCatResponse>>
        {
            private readonly ILogger<GetAllCat> _logger;
            private readonly IMapper _mapper;
            private readonly ICatService _service;

            public Handler(ILogger<GetAllCat> logger, IMapper mapper, ICatService service)
            {
                _logger = logger;
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<GetAllCatResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadAllM Cat [{DateTime.Now}]");
                    var list = await _service.GetAll();
                    return _mapper.Map<List<Models.Cat>, List<GetAllCatResponse>>(list);
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ReadAllM error Cat [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
