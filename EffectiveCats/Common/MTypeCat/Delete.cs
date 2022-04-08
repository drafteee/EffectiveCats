using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class DeleteCatType
    {
        public class Command : IDeleteCommand
        {
            public long Id { get ; set ; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }
        public class Handler : DeleteCommandHandler<CatType, Command, ICRUD<CatType>>
        {
            public Handler(ILogger<DeleteCatType> logger, ICRUD<CatType> service) : base(logger, service) { }
        }
    }
}
