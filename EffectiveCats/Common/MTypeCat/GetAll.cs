using AutoMapper;
using BL.Services;
using Common.MTypeCat.Dto;
using DAL.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetAllCatType
    {
        public class Request : IRequest<List<GetAllTypeCatDto>> { }

        public class Handler : IRequestHandler<Request, List<GetAllTypeCatDto>>
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

            public async Task<List<GetAllTypeCatDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadAllM CatType [{DateTime.Now}]");
                    var list = await _service.GetAll();
                    return _mapper.Map<List<CatType>, List<GetAllTypeCatDto>>(list);
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
