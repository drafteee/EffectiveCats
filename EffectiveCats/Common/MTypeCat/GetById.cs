using AutoMapper;
using BL.Exceptions;
using BL.Services;
using Common.MTypeCat.Dto;
using DAL.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetByIdCatType
    {
        public class Request : IRequest<GetByIdTypeCatDto>
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

        public class Handler : IRequestHandler<Request, GetByIdTypeCatDto>
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

            public async Task<GetByIdTypeCatDto> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadM CatType [{DateTime.Now}]");

                    var entity = await _service.Get(request.Id);
                    if(entity == null)
                    {
                        throw new AppException($"Not found CatType id={request.Id}");
                    }
                    return _mapper.Map<CatType, GetByIdTypeCatDto>(entity);
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
