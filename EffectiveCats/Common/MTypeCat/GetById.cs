using AutoMapper;
using Common.MBase;
using Common.MTypeCat.Dto;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetByIdCatType
    {
        public class Request : IReadQuery<GetByIdTypeCatDto>
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

        public class Handler : ReadQueryHandler<CatType, Request, ICRUD<CatType>, GetByIdTypeCatDto>
        {
            public Handler(ILogger<GetByIdCatType> logger, IMapper mapper, ICRUD<CatType> service) : base(logger, mapper, service) { }
        }
    }
}
