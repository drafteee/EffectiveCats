﻿using AutoMapper;
using BL.Interfaces;
using Common.MBase;
using Common.MTypeCat.Dto;
using DAL.Models;
using Microsoft.Extensions.Logging;

namespace Common.MTypeCat
{
    public class GetAllCatType
    {
        public class Request : IReadAll<GetAllTypeCatDto> { }

        public class Handler : ReadAllQueryHandler<CatType, long, Request, ICRUD<CatType, long>, GetAllTypeCatDto>
        {
            public Handler(ILogger<GetAllCatType> logger, IMapper mapper, ICRUD<CatType, long> service) : base(logger, mapper, service) { }
        }
    }
}
