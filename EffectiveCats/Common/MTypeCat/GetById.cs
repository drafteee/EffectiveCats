using AutoMapper;
using BL.Interfaces;
using Common.MBase;
using Common.MTypeCat.Dto;
using DAL.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetByIdCatType
    {
        public class Request : IReadQuery<GetByIdTypeCatDto, long>
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

        public class Handler : ReadQueryHandler<CatType, long, Request, ICRUD<CatType, long>, GetByIdTypeCatDto>
        {
            public Handler(ILogger<GetByIdCatType> logger, IMapper mapper, ICRUD<CatType, long> service) : base(logger, mapper, service) { }
        }
    }
}
