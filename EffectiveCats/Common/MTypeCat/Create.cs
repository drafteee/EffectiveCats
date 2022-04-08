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
        public class Command : ICreateCommand
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

        public class Handler : CreateCommandHandler<CatType, Command, ICRUD<CatType>>
        {
            public Handler(ILogger<CreateCatType> logger, IMapper mapper, ICRUD<CatType> service) : base(logger, mapper, service) { }
        }
    }
}
