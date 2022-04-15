using AutoMapper;
using Common.MBase;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class UpdateCat
    {
        public class Command : IUpdateCommand<Cat, long>
        {
            public long Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public IFormFile? Image { get; set; }
            public int? TypeId { get; set; }
        }

        public class Handler : UpdateCommandHandler<Cat, long, Command, ICRUD<Cat, long>>
        {
            public Handler(ILogger<UpdateCat> logger, IMapper mapper, ICRUD<Cat, long> service) : base(logger, mapper, service) { }
        }
    }
}
