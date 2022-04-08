using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class DeleteCat
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

        public class Handler : DeleteCommandHandler<Cat, Command, ICRUD<Cat>>
        {
            public Handler(ILogger<DeleteCat> logger, ICRUD<Cat> service) : base(logger, service) { }
        }
    }
}
