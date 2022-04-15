using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class DeleteCatType
    {
        public class Command : IDeleteCommand<long>
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
        public class Handler : DeleteCommandHandler<CatType, long, Command, ICRUD<CatType, long>>
        {
            public Handler(ILogger<DeleteCatType> logger, ICRUD<CatType, long> service) : base(logger, service) { }
        }
    }
}
