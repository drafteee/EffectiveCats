﻿using AutoMapper;
using Common.MBase;
using Common.MCat.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class GetAllCat
    {
        public class Request : IReadAll<GetAllCatDto> { }

        public class Handler : ReadAllQueryHandler<Cat, long, Request, ICRUD<Cat, long>, GetAllCatDto>
        {
            public Handler(ILogger<GetAllCat> logger, IMapper mapper, ICRUD<Cat, long> service) : base(logger, mapper, service) { }
        }
    }
}
