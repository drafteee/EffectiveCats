using AutoMapper;
using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class UpdateCatType
    {
        public class Command : IUpdateCommand<CatType>
        {
            public long Id { get; set; }
            public string? Name { get; set; }
        }
        public class Handler : UpdateCommandHandler<CatType, Command, ICRUD<CatType>>
        {
            public Handler(ILogger<UpdateCatType> logger, IMapper mapper, ICRUD<CatType> service) : base(logger, mapper, service) { }
        }
    }
}
