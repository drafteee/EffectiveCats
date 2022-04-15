using AutoMapper;
using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class UpdateCatType
    {
        public class Command : IUpdateCommand<CatType, long>
        {
            public long Id { get; set; }
            public string? Name { get; set; }
        }
        public class Handler : UpdateCommandHandler<CatType, long, Command, ICRUD<CatType, long>>
        {
            public Handler(ILogger<UpdateCatType> logger, IMapper mapper, ICRUD<CatType, long> service) : base(logger, mapper, service) { }
        }
    }
}
