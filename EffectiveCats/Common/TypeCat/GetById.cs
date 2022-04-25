using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;
using Domain.Models;
using MediatR.Services;
using MediatR.TypeCat.Responses;

namespace MediatR.MTypeCat
{
    public class GetByIdCatType
    {
        public class Request : IRequest<GetByIdTypeCatResponse>
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

        public class Handler : IRequestHandler<Request, GetByIdTypeCatResponse>
        {
            private readonly ILogger<GetByIdCatType> _logger;
            private readonly ICatTypeService _service;
            private readonly IMapper _mapper;

            public Handler(ILogger<GetByIdCatType> logger, IMapper mapper, ICatTypeService service)
            {
                _logger = logger;
                _service = service;
                _mapper = mapper;
            }

            public async Task<GetByIdTypeCatResponse> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadM CatType [{DateTime.Now}]");

                    var entity = await _service.Get(request.Id);
                    if(entity == null)
                    {
                        throw new AppException($"Not found CatType id={request.Id}");
                    }
                    return _mapper.Map<CatType, GetByIdTypeCatResponse>(entity);
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ReadM error CatType [{DateTime.Now}]");

                    throw e;
                }
            }
        }
    }
}
