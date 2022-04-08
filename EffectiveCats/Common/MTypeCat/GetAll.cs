﻿using AutoMapper;
using Common.MBase;
using Common.MTypeCat.Dto;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetAllCatType
    {
        public class Request : IReadAll<GetAllTypeCatDto> { }

        public class Handler : ReadAllQueryHandler<CatType, Request, ICRUD<CatType>, GetAllTypeCatDto>
        {
            public Handler(ILogger<GetAllCatType> logger, IMapper mapper, ICRUD<CatType> service) : base(logger, mapper, service) { }
        }
    }
}