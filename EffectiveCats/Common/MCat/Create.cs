using AutoMapper;
using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class CreateCat 
    {
        public class Command : ICreateCommand<long>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public IFormFile Image { get; set; }
            public int TypeId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).Length(5, 10);
            }
        }

        public class Handler : CreateCommandHandler<Cat, long, Command, ICRUD<Cat, long>>
        {
            public Handler(ILogger<CreateCat> logger, IMapper mapper, ICRUD<Cat, long> service) : base(logger, mapper, service) { }
        }
    }
}
