using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.MBase
{
    public interface IUpdateCommand<TEntity> : IRequest<TEntity> where TEntity : class
    {
        long Id { get; set; }
    }

    public class UpdateCommandHandler<TEntity, TCommand, TService> : IRequestHandler<TCommand, TEntity>
        where TEntity : class
        where TCommand : IUpdateCommand<TEntity>
        where TService : ICRUD<TEntity>
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly TService _service;

        public UpdateCommandHandler(ILogger logger, IMapper mapper, TService service)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        public async Task<TEntity> Handle(TCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"UpdateM {typeof(TEntity).Name} [{DateTime.Now}]");

                var entity = _mapper.Map<TCommand, TEntity>(command);

                return await _service.Update(entity);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"UpdateM error {typeof(TEntity).Name} [{DateTime.Now}]");

                throw e;
            }
        }
    }
}
