using AutoMapper;
using BL.Exceptions;
using BL.Services;
using Common.MCat.Dto;
using DAL.Models;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class GetByIdCat
    {
        public class Request : IRequest<GetByIdCatDto>
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

        public class Handler : IRequestHandler<Request, GetByIdCatDto>
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

            public async Task<GetByIdCatDto> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    _logger.LogInformation($"ReadM Cat [{DateTime.Now}]");

                    var entity = await _service.Get(request.Id);
                    if (entity == null)
                    {
                        throw new AppException($"Not found Cat id={request.Id}");
                    }
                    return _mapper.Map<Cat, GetByIdCatDto>(entity);
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
