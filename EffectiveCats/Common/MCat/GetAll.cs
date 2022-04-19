using AutoMapper;
using BL.Services;
using Common.MCat.Dto;
using DAL.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class GetAllCat
    {
        public class Request : IRequest<List<GetAllCatDto>> { }

        public class Handler: IRequestHandler<Request, List<GetAllCatDto>>
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

            public async Task<List<GetAllCatDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadAllM Cat [{DateTime.Now}]");
                    var list = await _service.GetAll();
                    return _mapper.Map<List<Cat>, List<GetAllCatDto>>(list);
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
