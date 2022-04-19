﻿using AutoMapper;
using BL.Interfaces;
using Common.MBase;
using Common.MCat.Dto;
using DAL.Models;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Common.MCat
{
    public class GetByIdCat
    {
        public class Request : IReadQuery<GetByIdCatDto, long>
        {
            public long Id { get; set; }
        }

        public class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : ReadQueryHandler<Cat, long, Request, ICRUD<Cat, long>, GetByIdCatDto>
        {
            public Handler(ILogger<GetByIdCat> logger, IMapper mapper, ICRUD<Cat, long> service) : base(logger, mapper, service) { }
        }
    }
}
