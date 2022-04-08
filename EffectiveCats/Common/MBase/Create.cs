using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.MBase
{
    public interface ICreateCommand : IRequest<bool> { }

    public class CreateCommandHandler<TEntity, TCommand, TService> : IRequestHandler<TCommand, bool>
        where TEntity : class
        where TCommand : ICreateCommand
        where TService : ICRUD<TEntity>
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly TService _service;

        public CreateCommandHandler(ILogger logger, IMapper mapper, TService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public virtual async Task<bool> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"CreateM {typeof(TEntity).Name} [{DateTime.Now}]");

                var entity = _mapper.Map<TCommand, TEntity>(command);

                return await _service.Create(entity);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"CreateM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
