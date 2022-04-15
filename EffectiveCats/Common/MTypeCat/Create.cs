using AutoMapper;
using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class CreateCatType
    {
        public class Command : ICreateCommand<long>
        {
            public string Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).Length(5, 10);
            }
        }

        public class Handler : CreateCommandHandler<CatType, long, Command, ICRUD<CatType, long>>
        {
            public Handler(ILogger<CreateCatType> logger, IMapper mapper, ICRUD<CatType, long> service) : base(logger, mapper, service) { }
        }
    }
}
