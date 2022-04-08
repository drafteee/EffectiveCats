using AutoMapper;
using Common.MBase;
using Common.MCat.Dto;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class GetByIdCat
    {
        public class Request : IReadQuery<GetByIdCatDto>
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

        public class Handler : ReadQueryHandler<Cat, Request, ICRUD<Cat>, GetByIdCatDto>
        {
            public Handler(ILogger<GetByIdCat> logger, IMapper mapper, ICRUD<Cat> service) : base(logger, mapper, service) { }
        }
    }
}
