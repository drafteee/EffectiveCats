using AutoMapper;
using Microsoft.Extensions.Logging;
using MediatR.TypeCat.Responses;
using MediatR.Services;
using Domain.Entities;

namespace MediatR.MTypeCat
{
    public class GetAllCatType
    {
        public class Request : IRequest<List<GetAllTypeCatResponse>> { }

        public class Handler : IRequestHandler<Request, List<GetAllTypeCatResponse>>
        {
            private readonly ILogger<GetAllCatType> _logger;
            private readonly IMapper _mapper;
            private readonly ICatTypeService _service;

            public Handler(ILogger<GetAllCatType> logger, IMapper mapper, ICatTypeService service)
            {
                _logger = logger;
                _mapper = mapper;
                _service = service;
            }

            public async Task<List<GetAllTypeCatResponse>> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadAllM CatType [{DateTime.Now}]");
                    var list = await _service.GetAll();
                    return _mapper.Map<List<CatType>, List<GetAllTypeCatResponse>>(list);
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ReadAllM error CatType [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
