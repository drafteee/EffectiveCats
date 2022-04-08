﻿using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    /// <summary>
    /// Need to Add pagination
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadQuery<TEntityDto> : IRequest<TEntityDto> where TEntityDto : class
    {
        long Id { get; set; }
    }

    public class ReadQueryHandler<TEntity, TQuery, TService, TEntityDto> : IRequestHandler<TQuery, TEntityDto>
        where TEntity : class
        where TEntityDto : class
        where TQuery : IReadQuery<TEntityDto>
        where TService : ICRUD<TEntity>
    {
        private readonly ILogger _logger;
        private readonly TService _service;
        private readonly IMapper _mapper;

        public ReadQueryHandler(ILogger logger, IMapper mapper, TService service)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        public virtual async Task<TEntityDto> Handle(TQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"ReadM {typeof(TEntity).Name} [{DateTime.Now}]");

                var entity = await _service.Get(request.Id);

                return _mapper.Map<TEntity, TEntityDto>(entity);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"ReadM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}